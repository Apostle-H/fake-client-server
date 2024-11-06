using Assets.Source.Abilities.Runtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Abilities.View
{
    internal class AbilityBtn : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button btn;
        [SerializeField] private TMP_Text cooldownText;

        private IAbility _target;

        internal event Action<IAbility> OnClick;

        internal void SetTarget(IAbility target)
        {
            _target = target;
            icon.sprite = target.Icon;
        }

        internal void Bind() => btn.onClick.AddListener(Click);

        internal void Expose() => btn.onClick.RemoveListener(Click);

        public void Draw()
        {
            btn.interactable = !_target.OnCooldown;

            cooldownText.text = _target.OnCooldown 
                ? (_target.CooldownTime - _target.CooldownCounter).ToString()
                : string.Empty;
        }

        private void Click()
        {
            OnClick?.Invoke(_target);
        }
    }
}
