using System.Collections.Generic;

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
