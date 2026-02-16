using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FFX
{
    [RequireComponent(typeof(CinemachineSplineDolly))]
    public class CamSplineMoveController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveInputActionRef;
        [SerializeField] private float _moveSpeed = 1f;

        private CinemachineSplineDolly _splineDolly;

        private void Awake()
        {
            _splineDolly = GetComponent<CinemachineSplineDolly>();
        }

        private void Update()
        {
            float moveInputValue = _moveInputActionRef.action.ReadValue<float>();

            if (moveInputValue != 0f)
                _splineDolly.CameraPosition += Time.deltaTime * _moveSpeed * moveInputValue;
        }
    }
}
