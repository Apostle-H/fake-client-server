using Assets.Source.Abilities.Data;
using Assets.Source.Abilities.Runtime;
using Assets.Source.Effects.Continous.Configs;
using Assets.Source.Effects.Instant.Configs;
using Assets.Source.Entity;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Assets.Source.Abilities.Configs
{
    [CreateAssetMenu(menuName="SO/Ability/Config", fileName="NewAbilityConfig")]
    internal class AbilityConfigSO : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private Sprite icon;
        [SerializeField] private AbilityTarget target;
        [SerializeField] private EffectConfigSO[] instantEffects;
        [SerializeField] private ContinousEffectConfigSO[] continousEffects;
        [SerializeField] private uint cooldownTime;
        
        public IAbility Build(IEntity owner, IObjectResolver container)
        {
            var instantEffects = this.instantEffects.Select(effect => effect.Build(container));
            var continousEffects = this.continousEffects.Select(effect => effect.Build(container));

            var ability = new Ability(owner, title, icon, target, instantEffects, continousEffects, cooldownTime);
            container.Inject(ability);

            return ability;
        }
    }
}
