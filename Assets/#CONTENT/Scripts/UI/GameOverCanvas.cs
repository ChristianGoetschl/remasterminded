using UnityEngine;

namespace FFX
{
    [RequireComponent(typeof(Canvas))]
    public class GameOverCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _gameWonPanel;
        [SerializeField] private GameObject _gameLostPanel;

        private Canvas _canvas;
        private GameBoardController _gameBoardController;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _gameBoardController = FindFirstObjectByType<GameBoardController>();
            if (!_gameBoardController) Debug.LogError($" <<< No {nameof(GameBoardController)} found! >>>");

            _canvas.enabled = false;

            _gameBoardController.OnGameWon += GameWon;
            _gameBoardController.OnGameLost += GameLost;
            _gameBoardController.OnNewGameStarted += NewGameStarted;
        }
        private void OnDestroy()
        {
            _gameBoardController.OnGameWon -= GameWon;
            _gameBoardController.OnGameLost -= GameLost;
            _gameBoardController.OnNewGameStarted -= NewGameStarted;
        }

        private void ToggleCanvas(bool isActive) => _canvas.enabled = isActive;

        private void GameWon()
        {
            ToggleCanvas(true);
            _gameWonPanel.SetActive(true);
        }
        private void GameLost()
        {
            ToggleCanvas(true);
            _gameLostPanel.SetActive(true);
        }
        private void NewGameStarted() => ToggleCanvas(false);
    }
}
