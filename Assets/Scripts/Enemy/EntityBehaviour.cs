using UnityEngine;
using Darchi.DodgeballShowdown.StateMashine;

public abstract class EntityBehaviour : MonoBehaviour, IStateMashineHolder, ITarget
{
    [field: SerializeField] public Health Health { get; protected set; }
    [field: SerializeField] public BallHolder BallHolder { get; protected set; }
    [field: SerializeField] public BallThrower BallThrower { get; protected set; }
    [field: SerializeField] public TargetScaner TargetScaner { get; protected set; }
    [field: SerializeField] public EnemyAnimatorController Animator { get; protected set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; protected set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; protected set; }
    [field: SerializeField] public Entitys EntityType { get; protected set; }

    protected StateMashine _stateMashine;
    protected TargetProvider _targetProvider;

    public bool HasTarget => _targetProvider.Target != null && _targetProvider.Target.Transform.position != null;
    public Transform Transform => transform;

    public virtual void Construct(StateMashine stateMashine)
    {
        _stateMashine = stateMashine;
        _targetProvider = new();
    }

    protected virtual void Update()
    {
        _stateMashine.Update();
    }
}
