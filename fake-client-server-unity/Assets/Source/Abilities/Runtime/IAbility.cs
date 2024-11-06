using Assets.Source.Abilities.Data;
using Assets.Source.Entity;
using Assets.Source.Restart;
using UnityEngine;

namespace Assets.Source.Abilities.Runtime
{
    internal interface IAbility : IRestartable
    {
        string Name { get; }
        Sprite Icon { get; }
        AbilityTarget Target { get; }
        bool OnCooldown { get; }
        uint CooldownTime { get; }
        uint CooldownCounter { get; }

        bool CanUse(IEntity self, IEntity enemy);

        bool Use(IEntity self, IEntity enemy);
    }
}
