using Assets.Source.Abilities.Runtime;
using System;

namespace Assets.Source.Abilities.Pick
{
    internal class AbilityPicker : IPickAbilityPicker
    {
        public event Action<IAbility> OnPicked;

        public void Pick(IAbility ability) => OnPicked?.Invoke(ability);
    }
}
