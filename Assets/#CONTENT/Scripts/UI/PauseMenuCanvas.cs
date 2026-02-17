using UnityEngine;
using UnityEngine.InputSystem;

namespace FFX
{
    [RequireComponent(typeof(Canvas))]
    public class PauseMenuCanvas : MonoBehaviour
    {
        [SerializeField] private InputActionReference _togglePauseMenuActionRef;

        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();

            _togglePauseMenuActionRef.action.performed += ToggleCanvas;
        }
        private void OnDestroy()
        {
            _togglePauseMenuActionRef.action.performed -= ToggleCanvas;
        }

        private void ToggleCanvas(InputAction.CallbackContext _) => ToggleCanvas();
        private void ToggleCanvas() => ToggleCanvas(!_canvas.enabled);
        private void ToggleCanvas(bool isActive) => _canvas.enabled = isActive;
    }
}
