using Assets.Source.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Effects.Instant.Runtime
{
    internal class ClearEffect : IEffect
    {
        public Type[] ClearEffectTypes { get; private set; }

        private List<Type> _toFindInTargetEffectTypes = new();

        public ClearEffect(IEnumerable<Type> clearEffectType)
        {
            ClearEffectTypes = clearEffectType.ToArray();
        }

        public void Apply(IEntity target)
        {
            _toFindInTargetEffectTypes.Clear();
            _toFindInTargetEffectTypes.AddRange(ClearEffectTypes);

            var targetEffects = target.EffectTarget.Effects.Where(kvp =>
            {
                var foundType = _toFindInTargetEffectTypes.FirstOrDefault(type => kvp.Key.GetType() == type);
                _toFindInTargetEffectTypes.Remove(foundType);

                return foundType != default;
            }).Select(kvp => kvp.Key).ToArray();

            if (!targetEffects.Any())
                return;

            target.EffectTarget.Clear(targetEffects);
        }
    }
}
