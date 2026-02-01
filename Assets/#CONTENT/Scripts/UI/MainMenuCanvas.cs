using UnityEngine;
using UnityEngine.InputSystem;

namespace FFX
{
    [RequireComponent(typeof(Canvas))]
    public class MainMenuCanvas : MonoBehaviour
    {
        [SerializeField] private InputActionReference _toggleMainMenuActionRef;

        private Canvas _canvas;
        private GameBoardController _gameBoardController;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _gameBoardController = FindFirstObjectByType<GameBoardController>();
            if (!_gameBoardController) Debug.LogError($" <<< No {nameof(GameBoardController)} found! >>>");

            ToggleCanvas(true);

            _toggleMainMenuActionRef.action.performed += ToggleCanvas;
            _gameBoardController.OnNewGameStarted += NewGameStarted;
        }
        private void OnDestroy()
        {
            _toggleMainMenuActionRef.action.performed -= ToggleCanvas;
            _gameBoardController.OnNewGameStarted -= NewGameStarted;
        }

        private void ToggleCanvas(InputAction.CallbackContext _) => ToggleCanvas();
        private void ToggleCanvas() => ToggleCanvas(!_canvas.enabled);
        private void ToggleCanvas(bool isActive) => _canvas.enabled = isActive;

        private void NewGameStarted() => ToggleCanvas(false);
    }
}
