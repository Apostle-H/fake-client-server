using Assets.Source.Entity;
using Assets.Source.Restart;
using Assets.Source.Step.Agent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using VContainer;
using VContainer.Unity;

namespace Assets.Source.Step.Agent
{
    internal class TurnManager : IStartable, IRestartable
    {
        private IAgent[] _agents;

        private int _currentActiveIndex = 0;

        public IAgent CurrentActive => _agents[_currentActiveIndex];

        public event Action<IAgent> OnTurn;

        [Inject]
        internal TurnManager(IEnumerable<IEntity> entities)
        {
            _agents = entities.Select(entity => entity.Agent).ToArray();
        }

        public void Start() => ActivateCurrent();

        private void ActivateCurrent()
        {
            OnTurn?.Invoke(_agents[_currentActiveIndex]);


            _agents[_currentActiveIndex].OnNewState += Next;
            _agents[_currentActiveIndex].Act();
        }

        private void Next(AgentState agentState)
        {
            if (agentState != AgentState.ACTED)
                return;

            _agents[_currentActiveIndex].OnNewState -= Next;
            _currentActiveIndex = _currentActiveIndex + 1 >= _agents.Length ? 0 : _currentActiveIndex + 1;

            ActivateCurrent();
        }

        public void Restart()
        {
            if (_currentActiveIndex == 0 && CurrentActive.CurrentState == AgentState.CHOOSING_ACTION)
                return;

            CurrentActive.OnNewState -= Next;
            CurrentActive.OnNewState += LauchFirst;
        }

        private void LauchFirst(AgentState agentState)
        {
            if (agentState != AgentState.IDLE)
                return;

            CurrentActive.OnNewState -= LauchFirst;

            _currentActiveIndex = 0;
            Start();
        }
    }
}