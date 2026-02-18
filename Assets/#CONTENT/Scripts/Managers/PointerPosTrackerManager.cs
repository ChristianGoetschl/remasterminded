using UnityEngine;
using UnityEngine.InputSystem;

namespace FFX
{
    public class PointerPosTrackerManager : MonoBehaviour
    {
        public static Vector3? PointerPos;

        private Camera _mainCam;

        private void Awake()
        {
            _mainCam = Camera.main;
        }

        private void Update()
        {
            Ray ray = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitinfo, _mainCam.farClipPlane))
                PointerPos = hitinfo.point;
            else PointerPos = null;
        }
    }
}
