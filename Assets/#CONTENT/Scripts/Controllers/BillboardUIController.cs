using UnityEngine;

namespace FFX
{
    public class BillboardUIController : MonoBehaviour
    {
        private Transform _mainCamTransform;

        private void Awake()
        {
            _mainCamTransform = Camera.main.transform;
        }

        // Point the forward vector away from the main camera
        private void Update() =>
            transform.forward = transform.position - _mainCamTransform.position;
    }
}
