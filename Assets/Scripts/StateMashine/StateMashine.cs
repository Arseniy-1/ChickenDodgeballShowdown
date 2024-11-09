using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Darchi.DodgeballShowdown.StateMashine
{
    public abstract class StateMashine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public StateMashine(List<IState> states)
        {
            foreach (IState state in states)
            {
                state.Initialize(this);
            }

            _states = states;
            EnterStartState();
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();

        protected void EnterStartState()
        {
            _currentState = _states[0];
            _currentState.Enter();
        }

        protected void SetStartStates(params IState[] states)
        {
            foreach (IState state in states)
            {
                _states.Add(state);
            }
        }
    }

    public class EnemyStateMashine : StateMashine
    {
        public EnemyStateMashine(List<IState> states) : base(states)
        {
        }
    }

    public class PlayerStateMashine : StateMashine
    {
        public PlayerStateMashine(List<IState> states) : base(states)
        {
        }
    }

    public class IdleState : IState
    {
        private readonly EntityBehaviour _entity;
        private IStateSwitcher _stateSwitcher;

        public IdleState(EntityBehaviour entity)
        {
            _entity = entity;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Enter()
        {
            Debug.Log(GetType());
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            if (_entity.TargetProvider.Ball != null)
                _stateSwitcher.SwitchState<MoveState>();
        }
    }

    public abstract class MoveState : IState
    {
        public IStateSwitcher StateSwitcher { get; protected set; }
        protected EntityBehaviour Entity { get; private set; }

        public MoveState(EntityBehaviour entity)
        {
            Entity = entity;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            StateSwitcher = stateSwitcher;
        }

        public virtual void Enter()
        {
            Debug.Log(GetType());
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            //unityRX
            if (Entity.GroundChecker.IsGrounded && Entity.TargetProvider.Ball != null) 
            {
                Vector3 direction = Vector3.ProjectOnPlane(Entity.TargetProvider.Ball.transform.position - Entity.transform.position, Vector3.up).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction);
                rotation.x = 0;
                rotation.z = 0;

                Entity.transform.rotation = Quaternion.Lerp(Entity.transform.rotation, rotation, 2 * Time.deltaTime);//Магическое число - скорость поворота
                Entity.transform.position += Entity.transform.forward * Time.deltaTime * 4; //Магическое число - скорость
            }
        }
    }

    public class EnemyMoveState : MoveState
    {
        public EnemyMoveState(EnemyBehaviour enemy) : base(enemy)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Entity.BallHolder.HasBall)
                StateSwitcher.SwitchState<AttackState>();
        }
    }

    public class PlayerMoveState : MoveState
    {
        public PlayerMoveState(PlayerBehaviour player) : base(player)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Entity.BallHolder.HasBall)
                StateSwitcher.SwitchState<AttackState>();

            if (Input.GetKeyDown(KeyCode.F) && Entity.GroundChecker.IsGrounded)
                StateSwitcher.SwitchState<JumpState>();
        }
    }

    public class AttackState : IState
    {
        protected readonly EntityBehaviour _entity;
        protected IStateSwitcher _stateSwitcher;

        public AttackState(EntityBehaviour enemy)
        {
            _entity = enemy;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Enter()
        {
            Debug.Log(GetType());
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            if (_entity.TargetProvider.TargetEntity != null)
            {
                Vector3 direction = Vector3.ProjectOnPlane(_entity.TargetProvider.TargetEntity.transform.position - _entity.transform.position, Vector3.up).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction);
                rotation.x = 0;
                rotation.z = 0;

                _entity.transform.rotation = rotation;//Магическое число - скорость поворота
            }
        }
    }

    public class EnemyAttackState : AttackState
    {
        public EnemyAttackState(EntityBehaviour enemy) : base(enemy)
        {
        }

        public override void Update()
        {
            base.Update();

            if (_entity.BallHolder.TryGetBall(out Ball ball))
            {
                _entity.BallThrower.Throw(ball);
                _stateSwitcher.SwitchState<MoveState>();
            }
        }
    }

    public class PlayerAttackState : AttackState
    {
        public PlayerAttackState(EntityBehaviour enemy) : base(enemy)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.E) && _entity.BallHolder.TryGetBall(out Ball ball))
            {
                _entity.BallThrower.Throw(ball);
                _stateSwitcher.SwitchState<MoveState>();
            }
        }
    }

    public class JumpState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private readonly EntityBehaviour _entity;

        public JumpState(EntityBehaviour entity)
        {
            _entity = entity;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public virtual void Enter()
        {
            _entity.Rigidbody.AddForce(Vector3.up * 350, ForceMode.Force);
            _stateSwitcher.SwitchState<IdleState>();
            Debug.Log(GetType());
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }
    }
}