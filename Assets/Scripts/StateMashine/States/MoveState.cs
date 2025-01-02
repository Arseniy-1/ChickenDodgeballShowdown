using UnityEngine;

namespace Darchi.DodgeballShowdown.StateMashine
{
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
}