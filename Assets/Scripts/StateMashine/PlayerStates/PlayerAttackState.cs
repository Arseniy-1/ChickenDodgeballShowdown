using UnityEngine;

namespace Darchi.DodgeballShowdown.StateMashine
{
    public class PlayerAttackState : IState
    {
        private readonly PlayerBehaviour _player;
        private Ball _ball;

        private IStateSwitcher _stateSwitcher;

        public PlayerAttackState(PlayerBehaviour player)
        {
            _player = player;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            _player.PlayerInputController.ActionButtonStarted += OnButtonClicked;
            _player.PlayerInputController.ActionButtonCanceled += OnButtonReleased;
        }

        public void Exit()
        {
            _player.PlayerInputController.ActionButtonStarted -= OnButtonClicked;
            _player.PlayerInputController.ActionButtonCanceled -= OnButtonReleased;
        }

        public void Update()
        {
            if (_player.TargetProvider.TargetEntity != null)
            {
                Vector3 direction = Vector3.ProjectOnPlane(_player.TargetProvider.TargetEntity.transform.position - _player.transform.position, Vector3.up).normalized;

                Quaternion rotation = Quaternion.LookRotation(direction);
                rotation.x = 0;
                rotation.z = 0;

                _player.transform.rotation = rotation;//Магическое число - скорость поворота
            }
        }

        private void OnButtonClicked()
        {
            if (_player.BallHolder.TryGetBall(out Ball ball))
            {
                _ball = ball;
                _player.BallThrower.StartCharging();
            }
        }

        private void OnButtonReleased()
        {
            if (_ball != null)
            {
                _player.BallThrower.StopCharging();
                _player.BallThrower.Throw(_ball);
            }

            _ball = null;

            _stateSwitcher.SwitchState<IdleState>();
        }
    }
}