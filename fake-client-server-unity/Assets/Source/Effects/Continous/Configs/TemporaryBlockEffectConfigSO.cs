using Assets.Source.Effects.Continous.Runtime;
using UnityEngine;
using VContainer;

namespace Assets.Source.Effects.Continous.Configs
{
    [CreateAssetMenu(menuName = "SO/Ability/Effect/Continous/TemporaryBlock", fileName = "NewTemporaryBlockEffectConfig")]
    internal class TemporaryBlockEffectConfigSO : ContinousEffectConfigSO
    {
        [SerializeField] private uint block;

        public override IContinousEffect Build(IObjectResolver container)
        {
            var temporaryBlock = new TemporaryBlockEffect(block, icon, lifeTime);
            container.Inject(temporaryBlock);
            return temporaryBlock;
        }
    }
}
