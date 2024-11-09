using System.Collections.Generic;
using System.Linq;
using Darchi.DodgeballShowdown.StateMashine;
using TMPro;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private Dictionary<TriggerZone, Squad> _squads = new();
    [SerializeField] private List<Squad> _squad;
    [SerializeField] private List<TriggerZone> _zones;

    [SerializeField] private Ball _ball;

    public void Initialize(List<Squad> squads)
    {
        _squad = squads;

        for (int i = 0; i < _squad.Count; i++)
        {
            _squads.Add(_zones[i], _squad[i]);
        }

        foreach (TriggerZone triggerZone in _squads.Keys)
        {
            triggerZone.HasBall += NotifySquad;
        }
    }

    public void NotifySquad(TriggerZone areaType)
    {
        foreach (Squad squad in _squads.Values)
            squad.LostBall();

        _squads[areaType].SelectBall(_ball);
    }
}

public class Squad
{
    private List<EntityBehaviour> _entities;
    private Ball _ball;

    public Squad(List<EntityBehaviour> entityBehaviours, SquadStateMashine squadStateMashine)
    {
        _entities = entityBehaviours.ToList();

        foreach (EntityBehaviour entityBehaviour in _entities)
        {
            entityBehaviour.BallTaken += OnBallTaken;
        }
    }

    public void OnBallTaken(EntityBehaviour entityBehaviour)
    {
        foreach (EntityBehaviour entity in _entities)
        {
            if (entity != entityBehaviour)
            {
                entity.TargetProvider.DeselectBall();

            }
        }
    }

    public void SelectBall(Ball ball)
    {
        _ball = ball;

        foreach (EntityBehaviour entityBehaviour in _entities)
        {
            entityBehaviour.TargetProvider.SelectBall(_ball);
        }
    }

    public void LostBall()
    {
        _ball = null;

        foreach (EntityBehaviour entityBehaviour in _entities)
        {
            entityBehaviour.TargetProvider.DeselectBall();
        }
    }
}

public class SquadStateMashine : StateMashine
{
    private List<IState> _states;

    public SquadStateMashine(List<IState> states) : base(states)
    {
        _states = states.ToList();
    }
}

public class IdleSquadState : IState
{
    private readonly List<EntityBehaviour> _entities;
    private IStateSwitcher _stateSwitcher;

    public IdleSquadState(List<EntityBehaviour> entityBehaviours)
    {
        _entities = entityBehaviours.ToList();
    }

    public void Initialize(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}

public class RunSquadState : IState
{
    private readonly List<EntityBehaviour> _entities;
    private IStateSwitcher _stateSwitcher;

    public RunSquadState(List<EntityBehaviour> entities)
    {
        _entities = entities;

        foreach (EntityBehaviour entity in _entities)
        {
            entity.BallTaken -= ChangeToRun;
        }
    }

    public void Initialize(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public void ChangeToRun(EntityBehaviour entity)
    {
        //entity.s
        _stateSwitcher.SwitchState<AnyHasBallSquadState>();
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}

public class AnyHasBallSquadState : IState
{
    private readonly List<EntityBehaviour> _entities;
    private IStateSwitcher _stateSwitcher;

    public AnyHasBallSquadState(List<EntityBehaviour> entities)
    {
        _entities = entities;
    }

    public void Initialize(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
