using Assets.Source.Abilities.Runtime;
using Assets.Source.Restart;
using System.Collections.Generic;

namespace Assets.Source.Abilities.User
{
    internal interface IAbilityUser : IRestartable
    {
        IReadOnlyCollection<IAbility> Abilities { get; }
    }
}
