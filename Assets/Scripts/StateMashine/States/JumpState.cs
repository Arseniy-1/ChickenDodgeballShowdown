using UnityEngine;

namespace Darchi.DodgeballShowdown.StateMashine
{
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