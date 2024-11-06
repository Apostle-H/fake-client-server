using System;

namespace Assets.Source.Properties.Defensive
{
    internal interface IDefensive
    {
        uint Block { get; }

        /// <summary>
        /// int - delta, uint - current
        /// </summary>
        event Action<int, uint> OnBlockModified;
        event Action<uint> OnBlocked;

        void AddBlock(uint block);
        void RemoveBlock(uint block);
    }
}
