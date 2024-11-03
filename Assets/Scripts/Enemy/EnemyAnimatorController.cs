using UnityEngine;

public class EnemyAnimatorController
{
    private Animator _animator;

    public void Initialize(Animator animator) => _animator = animator;

    public void StartIdling() => _animator.SetBool(Constans.Animation.IsIdling, true);
    public void StopIdling() => _animator.SetBool(Constans.Animation.IsIdling, false);

    public void StartRunning() => _animator.SetBool(Constans.Animation.IsRunning, true);
    public void StopRunning() => _animator.SetBool(Constans.Animation.IsRunning, false);

    public void StartGrounded() => _animator.SetBool(Constans.Animation.IsGrounded, true);
    public void StopGrounded() => _animator.SetBool(Constans.Animation.IsGrounded, false);

    public void StartJumping() => _animator.SetBool(Constans.Animation.IsJumping, true);
    public void StopJumping() => _animator.SetBool(Constans.Animation.IsJumping, false);

    public void StartFalling() => _animator.SetBool(Constans.Animation.IsFalling, true);
    public void StopFalling() => _animator.SetBool(Constans.Animation.IsFalling, false);

    public void StartAirborne() => _animator.SetBool(Constans.Animation.IsAirborne, true);
    public void StopAirborne() => _animator.SetBool(Constans.Animation.IsAirborne, false);

    public void StartMovement() => _animator.SetBool(Constans.Animation.IsMovement, true);
    public void StopMovement() => _animator.SetBool(Constans.Animation.IsMovement, false);
}

