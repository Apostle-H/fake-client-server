using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Entity.View
{
    internal class EffectView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text turnsLeftText;

        public void Draw(Sprite effectIcon, uint effectTime)
        {
            icon.sprite = effectIcon;
            UpdateTime(effectTime);
        }

        public void UpdateTime(uint effectTime) => turnsLeftText.text = effectTime.ToString();
    }
}
