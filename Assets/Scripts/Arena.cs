using System.Collections.Generic;
using System.Linq;
using Darchi.DodgeballShowdown.StateMashine;

public class Arena
{
    private List<Squad> _squads;
    private Ball ball;

    public Arena(List<Squad> squads)
    {
        _squads = squads.ToList();
    }
}

public class Squad
{
    private List<EntityBehaviour> _entities;
    private SquadStateMashine _squadStateMashine;
    private Ball _ball;

    public Squad(List<EntityBehaviour> entityBehaviours, SquadStateMashine squadStateMashine)
    {
        _entities = entityBehaviours.ToList();
        _squadStateMashine = squadStateMashine;
    }

    public void SelectBall(Ball ball)
    {
        _ball = ball;
    }

    public void LostBall()
    {
        _ball = null;
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
            entity.BallHolder.BallTaken -= ChangeToRun;
        }
    }

    public void Initialize(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public void ChangeToRun()
    {
        _stateSwitcher.SwitchState<IdleSquadState>();
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
