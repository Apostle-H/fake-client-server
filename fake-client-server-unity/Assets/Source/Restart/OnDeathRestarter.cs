using Assets.Source.Entity;
using Assets.Source.Properties.Hp;
using Assets.Source.Step.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using VContainer;
using VContainer.Unity;

namespace Assets.Source.Restart
{
    internal class OnDeathRestarter : IStartable, IDisposable
    {
        private IGlobalRestarter _restarter;
        private TurnManager _turnManager;

        private IHp[] _hps;

        [Inject]
        public OnDeathRestarter(IGlobalRestarter restarter, IEnumerable<IEntity> entities, TurnManager turnManager)
        {
            _restarter = restarter;
            _turnManager = turnManager;
            _hps = entities.Select(entity => entity.Hp).ToArray();
        }

        public void Start()
        {
            foreach (var hp in _hps)
                hp.OnHealthModified += RestartIfDead;
        }

        public void Dispose()
        {
            foreach (var hp in _hps)
                hp.OnHealthModified -= RestartIfDead;
        }

        private void RestartIfDead(int _delta, uint current, uint _max)
        {
            if (current > 0)
                return;

            _restarter.Restart();
        }
    }
}
