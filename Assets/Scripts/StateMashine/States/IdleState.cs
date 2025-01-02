using UnityEngine;

namespace Darchi.DodgeballShowdown.StateMashine
{
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
}