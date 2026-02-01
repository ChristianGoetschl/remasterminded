using UnityEngine;

namespace FFX
{
    public class CodeCoverController : MonoBehaviour
    {
        [SerializeField] private GameObject _codeCover;

        private GameBoardController _gameBoardController;

        private void Awake()
        {
            _gameBoardController = FindFirstObjectByType<GameBoardController>();
            if (!_gameBoardController) Debug.LogError($" <<< No {nameof(GameBoardController)} found! >>>");

            _gameBoardController.OnNewGameStarted += NewGameStarted;
            _gameBoardController.OnGameWon += GameWon;
            _gameBoardController.OnGameLost += GameLost;
        }
        private void OnDestroy()
        {
            _gameBoardController.OnNewGameStarted -= NewGameStarted;
            _gameBoardController.OnGameWon -= GameWon;
            _gameBoardController.OnGameLost -= GameLost;
        }

        private void NewGameStarted() => ToggleCodeCover(true);
        private void GameWon() => ToggleCodeCover(false);
        private void GameLost() => ToggleCodeCover(false);

        private void ToggleCodeCover(bool codeIsHidden) =>
            _codeCover.SetActive(codeIsHidden);
    }
}
