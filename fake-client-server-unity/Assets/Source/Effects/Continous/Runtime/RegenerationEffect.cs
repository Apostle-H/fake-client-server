using Assets.Source.Entity;
using Assets.Source.Step.Agent.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Effects.Continous.Runtime
{
    internal class RegenerationEffect : AContinousEffect
    {
        private Dictionary<IEntity, Action<AgentState>> _healActions = new();

        public uint Health { get; private set; }

        public RegenerationEffect(uint health, Sprite icon, uint lifeTime) : base(icon, lifeTime)
        {
            Health = health;
        }

        public override void Apply(IEntity target)
        {
            if (_healActions.ContainsKey(target))
                return;

            _healActions.Add(target, (agentState) => 
            {
                if (agentState == AgentState.CHOOSING_ACTION) 
                    Heal(target); 
            });
            target.Agent.OnNewState += _healActions[target];
        }

        public override void Remove(IEntity target) 
        {
            if (!_healActions.ContainsKey(target))
                return;

            target.Agent.OnNewState -= _healActions[target];
            _healActions.Remove(target);

            Finished(target);
        }

        private void Heal(IEntity target)
        {
            target.Hp.Heal(Health);

            Count(target);
        }
    }
}
