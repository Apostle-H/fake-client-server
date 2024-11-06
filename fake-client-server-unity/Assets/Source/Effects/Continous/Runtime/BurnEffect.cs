using Assets.Source.Entity;
using Assets.Source.Step.Agent.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Effects.Continous.Runtime
{
    internal class BurnEffect : AContinousEffect
    {
        private Dictionary<IEntity, Action<AgentState>> _burnActions = new();

        public uint Damage { get; private set; }

        public BurnEffect(uint damage, Sprite icon, uint lifeTime) : base(icon, lifeTime)
        {
            Damage = damage;
        }

        public override void Apply(IEntity target)
        {
            if (_burnActions.ContainsKey(target))
                return;

            _burnActions.Add(target, (agentState) =>
            {
                if (agentState == AgentState.CHOOSING_ACTION)
                    Burn(target);
            });
            target.Agent.OnNewState += _burnActions[target];
        }

        public override void Remove(IEntity target)
        {
            if (!_burnActions.ContainsKey(target))
                return;

            target.Agent.OnNewState -= _burnActions[target];
            _burnActions.Remove(target);

            Finished(target);
        }

        private void Burn(IEntity target)
        {
            target.Hp.TakeDamage(Damage);

            Count(target);
        }
    }
}
