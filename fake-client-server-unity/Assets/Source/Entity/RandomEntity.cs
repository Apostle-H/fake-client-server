using Assets.Source.Abilities.User;
using Assets.Source.Effects.Continous.Properties.ContinousTarget;
using Assets.Source.Properties.Hp;
using Assets.Source.Step.Agent;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Assets.Source.Entity
{
    internal class RandomEntity : AEntity
    {
        [SerializeField] private AEntity enemy;

        [Inject]
        private void Inject(IObjectResolver container)
        {
            var hp = new DefaultHp(maxHp);
            container.Inject(hp);

            var abilityUser = new DefaultAbilityUser(abilitiesConfigs.Select(abilityConfig => abilityConfig.Build(this, container)));
            container.Inject(abilityUser);

            var effectTarget = new DefaultContinousEffectTarget(this);
            container.Inject(effectTarget);

            var playerAgent = new RandomAgent(this, enemy);
            container.Inject(playerAgent);

            Build(hp, abilityUser, effectTarget, playerAgent);
        }
    }
}
