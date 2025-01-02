using System.Collections.Generic;
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
