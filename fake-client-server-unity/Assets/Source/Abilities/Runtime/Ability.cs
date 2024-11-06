using Assets.Source.Abilities.Data;
using Assets.Source.Effects.Continous.Runtime;
using Assets.Source.Effects.Instant.Runtime;
using Assets.Source.Entity;
using Assets.Source.Step.Agent.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Abilities.Runtime
{
    internal class Ability : IAbility
    {
        private IEntity _owner;

        private readonly IEffect[] _instantEffects;
        private readonly IContinousEffect[] _continousEffects;

        public string Name { get; private set; }
        public Sprite Icon { get; private set; }
        public AbilityTarget Target { get; private set; }
        public bool OnCooldown { get; private set; }
        public uint CooldownTime { get; private set; }
        public uint CooldownCounter { get; private set; }
        
        public Ability(IEntity owner, string name, Sprite icon, AbilityTarget target, IEnumerable<IEffect> instantEffects,
            IEnumerable<IContinousEffect> continousEffects, uint cooldownTime)
        {
            _owner = owner;

            Name = name;
            Icon = icon;
            Target = target;

            _instantEffects = instantEffects.ToArray();
            _continousEffects = continousEffects.ToArray();

            CooldownTime = cooldownTime;
        }

        ~Ability()
        {
            if (!OnCooldown)
                return;

            _owner.Agent.OnNewState -= Cooldown;
        }

        public void Restart()
        {
            if (!OnCooldown)
                return;

            CooldownCounter = CooldownTime;
            Cooldown(AgentState.CHOOSING_ACTION);
        }

        public bool CanUse(IEntity self, IEntity enemy) => !OnCooldown;

        public bool Use(IEntity self, IEntity enemy)
        {
            if (OnCooldown)
                return false;

            if (CooldownTime > 0)
            {
                OnCooldown = true;
                _owner.Agent.OnNewState += Cooldown;
            }

            var target = Target == AbilityTarget.SELF ? self : enemy;
            foreach (var effect in _instantEffects)
                effect.Apply(target);
            foreach (var effect in _continousEffects)
            {
                target.EffectTarget.Register(effect);
                effect.Apply(target);
            }

            return true;
        }

        private void Cooldown(AgentState agentState)
        {
            if (agentState != AgentState.CHOOSING_ACTION)
                return;

            CooldownCounter++;

            if (CooldownCounter < CooldownTime)
                return;

            OnCooldown = false;
            CooldownCounter = 0;
            _owner.Agent.OnNewState -= Cooldown;
        }
    }
}