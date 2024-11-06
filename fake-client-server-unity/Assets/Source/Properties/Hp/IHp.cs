using Assets.Source.Properties.Damagable;
using Assets.Source.Properties.Defensive;
using Assets.Source.Properties.Healable;
using Assets.Source.Restart;
using System;

namespace Assets.Source.Properties.Hp
{
    internal interface IHp : IDamagable, IHealable, IDefensive, IRestartable
    {
        uint Hp { get; }
        uint MaxHp { get; }

        /// <summary>
        /// int - delta, uint - current, uint - max
        /// </summary>
        event Action<int, uint, uint> OnHealthModified;
    }
}
