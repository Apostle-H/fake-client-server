using System;
using UnityEngine;

namespace Assets.Source.Properties.Hp
{
    internal class DefaultHp : IHp
    {
        public uint Hp { get; private set; }
        public uint MaxHp { get; private set; }
        public uint Block { get; private set; }

        public event Action<uint> OnDamaged;
        public event Action<uint> OnHealed;
        public event Action<int, uint, uint> OnHealthModified;
        public event Action<int, uint> OnBlockModified;

        public event Action<uint> OnBlocked;

        public DefaultHp(uint maxHp)
        {
            MaxHp = maxHp;
            Hp = maxHp;
        }

        public void Restart() => ModifyHp((int)(MaxHp - Hp));

        public void Heal(uint health)
        {
            ModifyHp((int)health);
            OnHealed?.Invoke(health);
        }

        public void TakeDamage(uint damage)
        {
            var unblockedDamage = (uint)Mathf.Clamp((int)damage - (int)Block, 0, damage);
            var blockedDamage = damage - unblockedDamage;
            if (blockedDamage > 0)
            {
                RemoveBlock(blockedDamage);
                OnBlocked?.Invoke(blockedDamage);

                damage = unblockedDamage;
            }

            ModifyHp(-(int)damage);
            OnDamaged?.Invoke(damage);
        }

        private void ModifyHp(int delta)
        {
            Hp = (uint)Mathf.Clamp((int)Hp + delta, 0, MaxHp);
            OnHealthModified?.Invoke(delta, Hp, MaxHp);
        }

        public void AddBlock(uint block) => ModifyBlock((int)block);

        public void RemoveBlock(uint block) => ModifyBlock(-(int)block);

        private void ModifyBlock(int delta)
        {
            Block = (uint)Mathf.Clamp(Block + delta, 0, uint.MaxValue);
            OnBlockModified?.Invoke(delta, Block);
        }
    }
}
