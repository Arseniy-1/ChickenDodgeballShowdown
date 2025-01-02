using Darchi.DodgeballShowdown.StateMashine;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    [field: SerializeField] public PlayerInputController PlayerInputController { get; private set; }

    protected override void Update()
    {
        base.Update();
    }

    public override void ChangeToIdleState()
    {
        StateMashine.SwitchState<IdleState>();
    }
}