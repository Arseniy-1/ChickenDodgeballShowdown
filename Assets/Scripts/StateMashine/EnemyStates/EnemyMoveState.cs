namespace Darchi.DodgeballShowdown.StateMashine
{
    public class EnemyMoveState : MoveState
    {
        public EnemyMoveState(EnemyBehaviour enemy) : base(enemy)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Entity.BallHolder.HasBall)
                StateSwitcher.SwitchState<EnemyAttackState>();
        }
    }
}