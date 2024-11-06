using Assets.Source.Step.Agent;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Assets.Source.Restart
{
    internal class GlobalRestarter : IGlobalRestarter
    {
        private TurnManager _turnManager;
        private IRestartable[] _restartables;
        public int RestartPriority => 0;

        [Inject]
        public GlobalRestarter(TurnManager turnManager, IEnumerable<IRestartable> restartables)
        {
            _turnManager = turnManager;
            _restartables = restartables.ToArray();
        }

        public void Restart()
        {
            foreach (var restartable in _restartables)
                restartable.Restart();
        }
    }
}
