using System;

namespace Assets.Source.Properties.Healable
{
    internal interface IHealable
    {
        /// <summary>
        /// uint - health healed
        /// </summary>
        event Action<uint> OnHealed;

        void Heal(uint health);
    }
}
