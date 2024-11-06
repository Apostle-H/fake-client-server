using System;

namespace Assets.Source.Properties.Damagable
{
    internal interface IDamagable
    {
        /// <summary>
        /// uint - Damage taken
        /// </summary>
        event Action<uint> OnDamaged;
        void TakeDamage(uint damage);
    }
}
