using UnityEngine;
using UnityEngine.InputSystem;

namespace FFX
{
    public class TestInputManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference _testInputActionRef;

        private GameBoardController _gameController;

        private void Awake()
        {
            _gameController = FindFirstObjectByType<GameBoardController>();
            if (!_gameController) Debug.LogError($" <<< No {nameof(GameBoardController)} found! >>>");

            _testInputActionRef.action.performed += TestInput;
        }
        private void OnDestroy()
        {
            _testInputActionRef.action.performed -= TestInput;
        }

        private void TestInput(InputAction.CallbackContext _) =>
            _gameController.FillActiveRowWithRandomGuess();
    }
}
