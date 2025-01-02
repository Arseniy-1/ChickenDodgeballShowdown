using System.Collections.Generic;
using System.Linq;

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
