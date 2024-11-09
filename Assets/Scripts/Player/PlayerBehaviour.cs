using Darchi.DodgeballShowdown.StateMashine;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    public InputHandler InputHandler { get; private set; }

    protected override void Update()
    {
        base.Update();
    }

    public override void ChangeToIdleState()
    {
        StateMashine.SwitchState<IdleState>();
    }
}