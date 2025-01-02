using System.Collections.Generic;

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
