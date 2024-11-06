using Assets.Source.Effects.Continous.Runtime;
using UnityEngine;
using VContainer;

namespace Assets.Source.Effects.Continous.Configs
{
    [CreateAssetMenu(menuName = "SO/Ability/Effect/Continous/Regeneration", fileName = "NewRegenerationEffectConfig")]
    internal class RegenerationEffectConfigSO : ContinousEffectConfigSO
    {
        [SerializeField] private uint health;

        public override IContinousEffect Build(IObjectResolver container)
        {
            var temporaryEffect = new RegenerationEffect(health, icon, lifeTime);
            container.Inject(temporaryEffect);
            return temporaryEffect;
        }
    }
}
