using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Darchi.DodgeballShowdown.StateMashine
{
    public class EnemyAttackState : IState
    {
        private readonly EnemyBehaviour _enemy;
        private Ball _ball;

        private IStateSwitcher _stateSwitcher;

        public EnemyAttackState(EnemyBehaviour enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            if (_enemy.BallHolder.TryGetBall(out Ball ball))
            {
                _ball = ball;
                _enemy.BallThrower.StartCharging();
            }

            WaitAndThrow();
        }

        public void Exit()
        {

        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Update()
        {
            if (_enemy.TargetProvider.TargetEntity != null)
            {
                Vector3 direction = Vector3.ProjectOnPlane(_enemy.TargetProvider.TargetEntity.transform.position - _enemy.transform.position, Vector3.up).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction);
                rotation.x = 0;
                rotation.z = 0;

                _enemy.transform.rotation = rotation;//Магическое число - скорость поворота
            }

        }

        private async UniTaskVoid WaitAndThrow()
        {
            var delay = Random.Range(500, 2000);
            await UniTask.Delay(delay);

            if (_ball != null)
            {
                _enemy.BallThrower.StopCharging();
                _enemy.BallThrower.Throw(_ball);
            }

            _ball = null;

            _stateSwitcher.SwitchState<IdleState>();
        }
    }
}