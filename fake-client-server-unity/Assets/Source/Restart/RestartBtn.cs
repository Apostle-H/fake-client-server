using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Source.Restart
{
    internal class RestartBtn : MonoBehaviour
    {
        [SerializeField] private Button btn;

        private IGlobalRestarter _restarter;

        [Inject]
        private void Inject(IGlobalRestarter restarter) => _restarter = restarter;

        private void Awake() => btn.onClick.AddListener(_restarter.Restart);

        private void OnDestroy() => btn.onClick.RemoveListener(_restarter.Restart);
    }
}
