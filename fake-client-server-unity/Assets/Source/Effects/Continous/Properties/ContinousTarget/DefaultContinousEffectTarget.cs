using Assets.Source.Effects.Continous.Runtime;
using Assets.Source.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Effects.Continous.Properties.ContinousTarget
{
    internal class DefaultContinousEffectTarget : IContinousEffectsCounter
    {
        private Dictionary<IContinousEffect, uint> _effects = new();

        public IReadOnlyDictionary<IContinousEffect, uint> Effects => _effects;

        public IEntity Entity { get; }

        public event Action<IContinousEffect, uint> OnNewEffect;
        public event Action<IContinousEffect, uint> OnCountEffect;
        public event Action<IContinousEffect> OnLostEffect;

        public DefaultContinousEffectTarget(IEntity entity) => Entity = entity;

        public void Restart() => Clear(_effects.Keys.ToArray());

        public void Register(IContinousEffect effect)
        {
            if (_effects.ContainsKey(effect))
            {
                _effects[effect] = effect.LifeTime;

                return;
            }

            _effects.Add(effect, effect.LifeTime);
            effect.OnCount += CountEffect;
            effect.OnFinished += ClearEffect;

            OnNewEffect?.Invoke(effect, _effects[effect]);
        }

        public void Clear(IEnumerable<IContinousEffect> effects)
        {
            foreach (var effect in effects)
            {
                if (!_effects.ContainsKey(effect))
                    continue;

                ClearEffect(Entity, effect);
            }
        }


        private void CountEffect(IEntity target, IContinousEffect effect) 
        {
            if (target != Entity)
                return;

            if (--_effects[effect] > 0)
            {
                OnCountEffect?.Invoke(effect, _effects[effect]);
                return;
            }

            ClearEffect(target, effect);
        }

        private void ClearEffect(IEntity target, IContinousEffect effect)
        {
            if (target != Entity)
                return;

            effect.OnCount -= CountEffect;
            effect.OnFinished -= ClearEffect;
            effect.Remove(target);

            _effects.Remove(effect);

            OnLostEffect?.Invoke(effect);
        }
    }
}
