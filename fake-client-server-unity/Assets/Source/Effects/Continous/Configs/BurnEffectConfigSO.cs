using Assets.Source.Effects.Continous.Runtime;
using UnityEngine;
using VContainer;

namespace Assets.Source.Effects.Continous.Configs
{
    [CreateAssetMenu(menuName = "SO/Ability/Effect/Continous/Burn", fileName = "NewBurnEffectConfig")]
    internal class BurnEffectConfigSO : ContinousEffectConfigSO
    {
        [SerializeField] private uint damage;

        public override IContinousEffect Build(IObjectResolver container)
        {
            var temporaryEffect = new BurnEffect(damage, icon, lifeTime);
            container.Inject(temporaryEffect);
            return temporaryEffect;
        }
    }
}
