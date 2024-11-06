using Assets.Source.Abilities.Pick;
using Assets.Source.Abilities.Runtime;
using Assets.Source.Entity;
using Assets.Source.Step.Agent.Data;
using UnityEngine;
using VContainer;

namespace Assets.Source.Abilities.View
{
    internal class AbilitiesPanel : MonoBehaviour
    {
        [SerializeField] private AEntity targetEntity;
        [SerializeField] private AbilityBtn abilityButtonPrefab;

        private IPickAbilityPicker _abilityPicker;

        private AbilityBtn[] _abilityBtns;

        private bool _bind = false;

        [Inject]
        private void Inject(IPickAbilityPicker picker)
        {
            _abilityPicker = picker;
        }

        private void Awake()
        {
            InitializeBtns();

            targetEntity.Agent.OnNewState += Toggle;
        }

        private void OnDestroy() => targetEntity.Agent.OnNewState -= Toggle;

        private void InitializeBtns()
        {
            _abilityBtns = new AbilityBtn[targetEntity.AbilityUser.Abilities.Count];
            var i = 0;
            foreach (var ability in targetEntity.AbilityUser.Abilities)
            {
                var abilityBtn = Instantiate(abilityButtonPrefab.gameObject, transform).GetComponent<AbilityBtn>();
                abilityBtn.SetTarget(ability);

                _abilityBtns[i++] = abilityBtn;
            }
        }

        private void Toggle(AgentState agentState)
        {
            if (agentState == AgentState.CHOOSING_ACTION)
            {
                Show();
            } else if (agentState == AgentState.ACTING)
            {
                Hide();
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);

            foreach (var ability in _abilityBtns)
                ability.Draw();

            Bind();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            Expose();
        }


        private void Bind()
        {
            if (_bind)
                return;

            foreach (var abilityBtn in _abilityBtns)
            {
                abilityBtn.Bind();
                abilityBtn.OnClick += AbilityPick;
            }

            _bind = true;
        }

        private void Expose()
        {
            if (!_bind)
                return;

            foreach (var abilityBtn in _abilityBtns)
            {
                abilityBtn.Expose();
                abilityBtn.OnClick -= AbilityPick;
            }

            _bind = false;
        }

        private void AbilityPick(IAbility ability) => _abilityPicker.Pick(ability);
    }
}
