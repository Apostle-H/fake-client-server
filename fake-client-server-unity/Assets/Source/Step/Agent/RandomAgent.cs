using Assets.Source.Entity;
using Assets.Source.Step.Agent.Data;
using Assets.Source.Utils.IEnumerableExtensions;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Step.Agent
{
    internal class RandomAgent : AAgent
    {
        private readonly IEntity _self;
        private readonly IEntity _enemy;

        public RandomAgent(IEntity self, IEntity enemy)
        {
            _self = self;
            _enemy = enemy;
        }

        public override void Act()
        {
            NewState(AgentState.CHOOSING_ACTION);
            NewState(AgentState.ACTING);

            var randomAbility = _self.AbilityUser.Abilities.Where(ability => ability.CanUse(_self, _enemy)).RandomElement();
            Debug.Log($"Random choice - {randomAbility.Name}");
            randomAbility.Use(_self, _enemy);

            NewState(AgentState.ACTED);
            NewState(AgentState.IDLE);
        }

        public override void Restart() { }
    }
}
