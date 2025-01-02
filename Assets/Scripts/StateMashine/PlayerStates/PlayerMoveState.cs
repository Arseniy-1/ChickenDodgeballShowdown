using UnityEngine;

namespace Darchi.DodgeballShowdown.StateMashine
{
    public class PlayerMoveState : MoveState
    {
        public PlayerMoveState(PlayerBehaviour player) : base(player)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Entity.BallHolder.HasBall)
                StateSwitcher.SwitchState<PlayerAttackState>();

            //if (Input.GetKeyDown(KeyCode.F) && Entity.GroundChecker.IsGrounded)
            //    StateSwitcher.SwitchState<JumpState>();
        }
    }
}