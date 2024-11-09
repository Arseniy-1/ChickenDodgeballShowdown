using UnityEngine;
using Darchi.DodgeballShowdown.StateMashine;

public class EnemyBehaviour : EntityBehaviour 
{
    protected override void Update()
    {
        base.Update();
    }

    public override void ChangeToIdleState()
    {
        StateMashine.SwitchState<IdleState>();
    }
}
