using Assets.Source.Abilities.Runtime;

namespace Assets.Source.Abilities.Pick
{
    internal interface IPickAbilityPicker : IAbilityPicker
    {
        void Pick(IAbility ability);
    }
}
