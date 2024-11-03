using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Darchi.DodgeballShowdown.StateMashine
{
    public abstract class StateMashine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public StateMashine(IStateMashineHolder holder, params IState[] states)
        {
            _currentState = _states[0];
            _currentState.Enter();

            foreach (IState state in states)
            {
                _states.Add(state);
            }
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();

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
        public EnemyStateMashine(EnemyBehaviour enemyBehaviour) : base(enemyBehaviour)
        {
            SetStartStates(new IdleState(this, enemyBehaviour), new EnemyMoveState(this, enemyBehaviour));
        }
    }

    public class PlayerStateMashine : StateMashine
    {
        public PlayerStateMashine(PlayerBehaviour playerBehaviour, IdleState idleState) : base(playerBehaviour)
        {
            SetStartStates(new IdleState(this, playerBehaviour), new PlayerMoveState(this, playerBehaviour));
        }
    }

    public class IdleState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly EnemyBehaviour _enemy;

        public IdleState(IStateSwitcher stateSwitcher, EnemyBehaviour enemy)
        {
            _stateSwitcher = stateSwitcher;
            _enemy = enemy;
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
            if (_enemy.BallHolder.HasBall == false)
                _stateSwitcher.SwitchState<MoveState>();
        }
    }

    public abstract class MoveState : IState
    {
        protected IStateSwitcher StateSwitcher { get; private set; }
        protected EnemyBehaviour Enemy {  get; private set; }

        public MoveState(IStateSwitcher stateSwitcher, EnemyBehaviour enemy)
        {
            StateSwitcher = stateSwitcher;
            Enemy = enemy;
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
            if (Enemy.GroundChecker.IsGrounded)
            {
                Vector3 direction = Vector3.ProjectOnPlane(Enemy.TargetScaner.Ball.transform.position - Enemy.transform.position, Vector3.up).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction);
                rotation.x = 0;
                rotation.z = 0;

                Enemy.transform.rotation = Quaternion.Lerp(Enemy.transform.rotation, rotation, 2 * Time.deltaTime);//Магическое число - скорость поворота
                Enemy.transform.position += Enemy.transform.forward * Time.deltaTime * 4; //Магическое число - скорость
            }
        }
    }

    public class EnemyMoveState : MoveState
    {
        public EnemyMoveState(IStateSwitcher stateSwitcher, EnemyBehaviour enemy) : base(stateSwitcher, enemy)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Enemy.BallHolder.HasBall)
                StateSwitcher.SwitchState<AttackState>();
        }
    }

    public class PlayerMoveState : MoveState
    {
        public PlayerMoveState(IStateSwitcher stateSwitcher, EnemyBehaviour enemy) : base(stateSwitcher, enemy)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.E) && Enemy.BallHolder.HasBall)
                StateSwitcher.SwitchState<AttackState>();
        }
    }

    public class AttackState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly EnemyBehaviour _enemy;

        public AttackState(IStateSwitcher stateSwitcher, EnemyBehaviour enemy)
        {
            _stateSwitcher = stateSwitcher;
            _enemy = enemy;
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
            Vector3 direction = Vector3.ProjectOnPlane(_enemy.TargetScaner.Player.transform.position - _enemy.transform.position, Vector3.up).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction);
            rotation.x = 0;
            rotation.z = 0;

            _enemy.transform.rotation = rotation;//Магическое число - скорость поворота

            if (_enemy.BallHolder.HasBall)
                _stateSwitcher.SwitchState<AttackState>();

            if (_enemy.BallHolder.TryGetBall(out Ball ball))
            {
                _enemy.BallThrower.Throw(ball);
                _stateSwitcher.SwitchState<MoveState>();
            }
        }
    }

    public class JumpState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly EnemyBehaviour _enemy;

        public JumpState(IStateSwitcher stateSwitcher, EnemyBehaviour enemy)
        {
            _stateSwitcher = stateSwitcher;
            _enemy = enemy;
        }

        public virtual void Enter()
        {
            _enemy.Rigidbody.AddForce(Vector3.up * 200, ForceMode.Force);
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