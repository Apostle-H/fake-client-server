using VContainer;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Assets.Source.Effects.Instant.Runtime;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Source.Effects.Instant.Configs
{
    [CreateAssetMenu(menuName = "SO/Ability/Effect/Clear", fileName = "NewClearEffectConfig")]
    internal class ClearEffectConfigSO : EffectConfigSO
    {
        private IEnumerable<Type> _clearEffectTypes;

        public override IEffect Build(IObjectResolver container)
        {
            var clearEffect = new ClearEffect(_clearEffectTypes);
            container.Inject(clearEffect);
            
            return clearEffect;
        }


#if UNITY_EDITOR
        [SerializeField] private MonoScript[] typeScripts;

        private void OnValidate()
        {
            _clearEffectTypes = typeScripts.Select(typeScript => typeScript.GetClass());
        }
#endif
    }
}
