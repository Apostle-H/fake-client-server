using Assets.Source.Abilities.User;
using Assets.Source.Effects.Continous.Properties.ContinousTarget;
using Assets.Source.Properties.Hp;
using Assets.Source.Restart;
using Assets.Source.Step.Agent;

namespace Assets.Source.Entity
{
    internal interface IEntity : IRestartable
    {
        IHp Hp { get; }
        IAbilityUser AbilityUser { get; }
        IContinousEffectsCounter EffectTarget { get; }
        IAgent Agent { get; }
    }
}
