using UnityEngine;
using UnityEngine.UI;

namespace FFX
{
    [RequireComponent(typeof(Button))]
    public class StartNewGameBtn : MonoBehaviour
    {
        private Button _btn;
        private GameBoardController _gameBoardController;

        private void Awake()
        {
            _gameBoardController = FindFirstObjectByType<GameBoardController>();
            if (!_gameBoardController) Debug.LogError($" <<< No {nameof(GameBoardController)} found! >>>");

            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(BtnClicked);
        }
        private void OnDestroy()
        {
            _btn.onClick.RemoveAllListeners();
        }

        private void BtnClicked() => _gameBoardController.StartNewGame();
    }
}
