using Assets.Source.Abilities.Configs;
using Assets.Source.Abilities.User;
using Assets.Source.Effects.Continous.Properties.ContinousTarget;
using Assets.Source.Properties.Hp;
using Assets.Source.Step.Agent;
using UnityEngine;

namespace Assets.Source.Entity
{
    internal abstract class AEntity : MonoBehaviour, IEntity
    {
        [SerializeField] protected uint maxHp;
        [SerializeField] protected AbilityConfigSO[] abilitiesConfigs;

        public IHp Hp { get; private set; }
        public IAbilityUser AbilityUser { get; private set; }
        public IContinousEffectsCounter EffectTarget { get; private set; }
        public IAgent Agent { get; private set; }


        public void Restart()
        {
            Hp.Restart();
            AbilityUser.Restart();
            EffectTarget.Restart();
            //Agent.Restart();
        }

        protected void Build(IHp hp, IAbilityUser abilityUser, IContinousEffectsCounter effectTarget, 
            IAgent agent)
        {
            Hp = hp;
            AbilityUser = abilityUser;
            EffectTarget = effectTarget;
            Agent = agent;
        }
    }
}
