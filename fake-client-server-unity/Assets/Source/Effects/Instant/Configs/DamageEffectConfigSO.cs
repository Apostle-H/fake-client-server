using VContainer;
using UnityEngine;
using Assets.Source.Effects.Instant.Runtime;

namespace Assets.Source.Effects.Instant.Configs
{
    [CreateAssetMenu(menuName = "SO/Ability/Effect/Damage", fileName = "NewDamageEffectConfig")]
    internal class DamageEffectConfigSO : EffectConfigSO
    {
        [SerializeField] private uint damage;

        public override IEffect Build(IObjectResolver container)
        {
            var damageEffect = new DamageEffect(damage);
            container.Inject(damageEffect);
            
            return damageEffect;
        }
    }
}
