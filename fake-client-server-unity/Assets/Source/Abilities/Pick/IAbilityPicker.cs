using Assets.Source.Abilities.Runtime;
using System;

namespace Assets.Source.Abilities.Pick
{
    internal interface IAbilityPicker
    {
        event Action<IAbility> OnPicked;
    }
}
