using Assets.Source.Effects.Continous.Runtime;
using UnityEngine;
using VContainer;

namespace Assets.Source.Effects.Continous.Configs
{
    internal abstract class ContinousEffectConfigSO : ScriptableObject
    {
        [SerializeField] protected Sprite icon;
        [SerializeField] protected uint lifeTime;

        public abstract IContinousEffect Build(IObjectResolver container);
    }
}
