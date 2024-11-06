using Assets.Source.Restart;
using Assets.Source.Step.Agent.Data;
using System;

namespace Assets.Source.Step.Agent
{
    internal interface IAgent : IRestartable
    {
        AgentState CurrentState { get; }

        event Action<AgentState> OnNewState;

        void Act();
    }
}
