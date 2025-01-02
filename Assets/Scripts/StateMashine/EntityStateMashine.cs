using System.Collections.Generic;
using System.Linq;

namespace Darchi.DodgeballShowdown.StateMashine
{
    public class EntityStateMashine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public EntityStateMashine(List<IState> states)
        {
            foreach (IState state in states)
            {
                state.Initialize(this);
            }

            _states = states;
            EnterStartState();
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();

        protected void EnterStartState()
        {
            _currentState = _states[0];
            _currentState.Enter();
        }

        protected void SetStartStates(params IState[] states)
        {
            foreach (IState state in states)
            {
                _states.Add(state);
            }
        }
    }
}