using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Entity.View
{
    internal class HpBarView : MonoBehaviour
    {
        [SerializeField] private AEntity targetEntity;
        [SerializeField] private Image barFill;
        [SerializeField] private TMP_Text barText;
        [SerializeField] private GameObject shieldIcon;
        [SerializeField] private TMP_Text shieldText;

        private void Awake()
        {
            targetEntity.Hp.OnHealthModified += DrawHp;
            targetEntity.Hp.OnBlockModified += DrawBlock;

            DrawHp(0, targetEntity.Hp.Hp, targetEntity.Hp.MaxHp);
            DrawBlock(0, targetEntity.Hp.Block);
        }

        private void OnDestroy()
        {
            targetEntity.Hp.OnHealthModified -= DrawHp;
            targetEntity.Hp.OnBlockModified -= DrawBlock;
        }

        private void DrawHp(int _, uint current, uint max)
        {
            barFill.fillAmount = (float)current / (float)max;
            barText.text = $"{current}/{max}";
        }

        private void DrawBlock(int _, uint current)
        {
            shieldIcon.SetActive(current > 0);

            shieldText.text = current.ToString();
        }
    }
}
