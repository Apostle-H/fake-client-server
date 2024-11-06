using Assets.Source.Entity;

namespace Assets.Source.Effects.Instant.Runtime
{
    internal class DamageEffect : IEffect
    {
        public uint Damage { get; private set; }

        public DamageEffect(uint damage)
        {
            Damage = damage;
        }

        public void Apply(IEntity target)
        {
            target.Hp.TakeDamage(Damage);
        }
    }
}
