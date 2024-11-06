using Assets.Source.Effects.Instant.Runtime;
using UnityEngine;
using VContainer;

namespace Assets.Source.Effects.Instant.Configs
{
    internal abstract class EffectConfigSO : ScriptableObject
    {
        public abstract IEffect Build(IObjectResolver container);
    }
}
