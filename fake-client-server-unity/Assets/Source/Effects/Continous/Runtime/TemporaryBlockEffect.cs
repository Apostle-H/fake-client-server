using Assets.Source.Effects.Continous.Runtime;
using Assets.Source.Entity;
using Assets.Source.Step.Agent.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Effects.Continous.Runtime
{
    internal class TemporaryBlockEffect : AContinousEffect
    {
        private Dictionary<IEntity, TemporaryBlockContext> _blockContext = new();

        public uint Block { get; private set; }


        public TemporaryBlockEffect(uint block, Sprite icon, uint lifeTime) : base(icon, lifeTime)
        {
            Block = block;
        }

        public override void Apply(IEntity target)
        {
            if (_blockContext.ContainsKey(target))
                return;

            _blockContext.Add(target, new((blocked) => CountBlocked(target, blocked), (agentState) =>
            {
                if (agentState == AgentState.CHOOSING_ACTION)
                    Count(target);
            }));
            target.Hp.AddBlock(Block);

            target.Hp.OnBlocked += _blockContext[target].BlockedCounter;
            target.Agent.OnNewState += _blockContext[target].TimeCounter;
        }

        public override void Remove(IEntity target)
        {
            if (!_blockContext.ContainsKey(target))
                return;

            target.Hp.OnBlocked -= _blockContext[target].BlockedCounter;
            target.Agent.OnNewState -= _blockContext[target].TimeCounter;

            var unusedBlock = Block - _blockContext[target].Blocked;
            if (unusedBlock > 0)
                target.Hp.RemoveBlock(unusedBlock);

            _blockContext.Remove(target);

            Finished(target);
        }

        private void CountBlocked(IEntity target, uint blocked)
        {
            if (blocked == 0)
                return;

            _blockContext[target].Blocked += blocked;
            if (_blockContext[target].Blocked < Block)
                return;

            Remove(target);
        }

        private class TemporaryBlockContext
        {
            public uint Blocked { get; set; }
            public Action<uint> BlockedCounter { get; private set; }
            public Action<AgentState> TimeCounter { get; private set; }

            public TemporaryBlockContext(Action<uint> blockedCounter, Action<AgentState> timeCounter)
            {
                BlockedCounter = blockedCounter;
                TimeCounter = timeCounter;
            }
        }
    }
}
