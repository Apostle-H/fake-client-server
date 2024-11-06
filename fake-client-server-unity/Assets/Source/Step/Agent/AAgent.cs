using Assets.Source.Step.Agent.Data;
using System;

namespace Assets.Source.Step.Agent
{
    internal abstract class AAgent : IAgent
    {
        public AgentState CurrentState { get; protected set; }

        public event Action<AgentState> OnNewState;

        public abstract void Act();

        public abstract void Restart();

        protected void NewState(AgentState state)
        {
            CurrentState = state;
            OnNewState?.Invoke(state);
        }
    }
}
