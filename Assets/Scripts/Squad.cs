using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
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
