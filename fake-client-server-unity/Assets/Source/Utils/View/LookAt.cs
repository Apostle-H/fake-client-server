using UnityEngine;

namespace Assets.Source.Utils.View
{
    internal class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform lookAtTarget;

        private void Update() => transform.LookAt(lookAtTarget);
    }
}
