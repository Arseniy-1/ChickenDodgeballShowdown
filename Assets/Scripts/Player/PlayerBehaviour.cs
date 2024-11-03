using Darchi.DodgeballShowdown.StateMashine;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IStateMashineHolder, ITarget
{
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public BallHolder BallHolder { get; private set; }
    [field: SerializeField] public BallThrower BallThrower { get; private set; }
    [field: SerializeField] public TargetScaner TargetScaner { get; private set; }
    [field: SerializeField] public EnemyAnimatorController Animator { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public GroundChecker GroundChecker { get; private set; }

    private EnemyStateMashine _stateMachine;
    private TargetProvider _targetProvider;

    public bool HasTarget => _targetProvider.Target != null && _targetProvider.Target.Transform.position != null;

    public Transform Transform => transform;

    private void Awake()
    {
        _targetProvider = new();
        _stateMachine = new PlayerStateMashine(this);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}

public class Destroyer : MonoBehaviour
{
    private Health _health;

    public void Initialize(Health health)
    {
        _health = health;
    }

    private void OnEnable()
    {
        _health.Died += RaiseDeath;
    }

    private void OnDisable()
    {
        _health.Died -= RaiseDeath;
    }

    private void RaiseDeath()
    {
        Destroy(gameObject);
    }
}
