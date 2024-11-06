using Assets.Source.Abilities.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Abilities.User
{
    internal class DefaultAbilityUser : IAbilityUser
    {
        private IAbility[] _abilities;

        public IReadOnlyCollection<IAbility> Abilities => _abilities;

        public DefaultAbilityUser(IEnumerable<IAbility> abilities)
        {
            _abilities = abilities.ToArray();
        }

        public void Restart()
        {
            foreach (var ability in _abilities)
                ability.Restart();
        }
    }
}
