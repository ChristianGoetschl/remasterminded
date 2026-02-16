using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FFX
{
    public class CamPriorityController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _zoomInputActionRef;
        [SerializeField] private CinemachineCamera _overviewCam;
        [SerializeField] private CinemachineCamera _closeUpCam;

        private GameBoardController _gameBoardController;

        private const int _LOWPRIO_VALUE = 0;
        private const int _HIGHPRIO_VALUE = 2;

        private void Awake()
        {
            _gameBoardController = FindFirstObjectByType<GameBoardController>();
            if (!_gameBoardController) Debug.LogError($" <<< No {nameof(GameBoardController)} found! >>>");

            _gameBoardController.OnGameWon += SetToOverviewCam;
            _gameBoardController.OnGameLost += SetToOverviewCam;
        }
        private void OnDestroy()
        {
            _gameBoardController.OnGameWon -= SetToOverviewCam;
            _gameBoardController.OnGameLost -= SetToOverviewCam;
        }

        private void Update()
        {
            float zoomInputValue = _zoomInputActionRef.action.ReadValue<float>();

            if (zoomInputValue != 0f)
                _overviewCam.Priority = zoomInputValue > 0 ? _LOWPRIO_VALUE : _HIGHPRIO_VALUE;
        }

        private void SetToOverviewCam() =>
            _overviewCam.Priority = _HIGHPRIO_VALUE;
    }
}
