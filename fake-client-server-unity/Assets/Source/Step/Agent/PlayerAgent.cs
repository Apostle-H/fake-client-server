using Assets.Source.Abilities.Pick;
using Assets.Source.Abilities.Runtime;
using Assets.Source.Entity;
using Assets.Source.Step.Agent.Data;

namespace Assets.Source.Step.Agent
{
    internal class PlayerAgent : AAgent
    {
        private readonly IAbilityPicker _abilityPicker;

        private readonly IEntity _self;
        private readonly IEntity _enemy;

        public PlayerAgent(IAbilityPicker abilityPicker, IEntity self, IEntity enemy)
        {
            _abilityPicker = abilityPicker;

            _self = self;
            _enemy = enemy;
        }

        public override void Act()
        {
            NewState(AgentState.CHOOSING_ACTION);

            _abilityPicker.OnPicked += UseAbility;
        }

        private void UseAbility(IAbility ability)
        {
            NewState(AgentState.ACTING);

            if (!ability.CanUse(_self, _enemy))
                return;

            _abilityPicker.OnPicked -= UseAbility;
            ability.Use(_self, _enemy);

            NewState(AgentState.ACTED);
            NewState(AgentState.IDLE);
        }

        public override void Restart() { }
    }
}
