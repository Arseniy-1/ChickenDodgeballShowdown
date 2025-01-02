using UnityEngine;
using Darchi.DodgeballShowdown.StateMashine;
using System;

public abstract class EntityBehaviour : MonoBehaviour, IStateMashineHolder, ITarget
{
    [field: SerializeField] public Health Health { get; protected set; }
    [field: SerializeField] public Entitys EntityType { get; protected set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; protected set; }
    [field: SerializeField] public BallHolder BallHolder { get; protected set; }
    [field: SerializeField] public BallThrower BallThrower { get; protected set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; protected set; }
    [field: SerializeField] public EnemyAnimatorController Animator { get; protected set; }
    [field: SerializeField] public TargetProvider TargetProvider { get; protected set; }

    protected EntityStateMashine StateMashine;

    public event Action<EntityBehaviour> BallTaken;

    public Transform Transform => transform;

    private void OnEnable()
    {
        BallHolder.BallTaken += ShowTaken;
    }

    private void OnDisable()
    {
        BallHolder.BallTaken -= ShowTaken;
    }

    public abstract void ChangeToIdleState();

    public virtual void Construct(EntityStateMashine stateMashine)
    {
        StateMashine = stateMashine;
    }

    protected virtual void Update()
    {
        StateMashine.Update();
    }

    private void ShowTaken()
    {
        BallTaken?.Invoke(this);
    }
}
