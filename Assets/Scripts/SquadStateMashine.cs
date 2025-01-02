using System.Collections.Generic;
using System.Linq;
using Darchi.DodgeballShowdown.StateMashine;

public class SquadStateMashine : EntityStateMashine
{
    private List<IState> _states;

    public SquadStateMashine(List<IState> states) : base(states)
    {
        _states = states.ToList();
    }
}
