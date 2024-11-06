using Assets.Source.Effects.Continous.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entity.View
{
    internal class ActiveEffectsView : MonoBehaviour
    {
        [SerializeField] private AEntity targetEntity;

        [SerializeField] private EffectView effectViewPrefab;

        private Dictionary<IContinousEffect, EffectView> _activeEffectsViews = new();

        private void Awake()
        {
            targetEntity.EffectTarget.OnNewEffect += NewEffect;
            targetEntity.EffectTarget.OnCountEffect += UpdateEffects;
            targetEntity.EffectTarget.OnLostEffect += LostEffect;
        }

        private void OnDestroy()
        {
            targetEntity.EffectTarget.OnNewEffect -= NewEffect;
            targetEntity.EffectTarget.OnCountEffect -= UpdateEffects;
            targetEntity.EffectTarget.OnLostEffect -= LostEffect;
        }

        private void NewEffect(IContinousEffect effect, uint timeLeft)
        {
            if (!_activeEffectsViews.ContainsKey(effect))
            {
                var effectView = Instantiate(effectViewPrefab.gameObject, transform).GetComponent<EffectView>();
                _activeEffectsViews.Add(effect, effectView);
            }

            _activeEffectsViews[effect].Draw(effect.Icon, timeLeft);
        }

        private void UpdateEffects(IContinousEffect effect, uint timeLeft) => 
            _activeEffectsViews[effect].UpdateTime(timeLeft);

        private void LostEffect(IContinousEffect effect)
        {
            Destroy(_activeEffectsViews[effect].gameObject);
            _activeEffectsViews.Remove(effect);
        }
    }
}
