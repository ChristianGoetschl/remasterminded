using UnityEngine;
using UnityEngine.InputSystem;

namespace FFX
{
    public class ExitAppManager : MonoBehaviour
    {
        [SerializeField] private InputActionReference _exitAppActionRef;

        private void Awake()
        {
            _exitAppActionRef.action.performed += ExitApp;
        }
        private void OnDestroy()
        {
            _exitAppActionRef.action.performed -= ExitApp;
        }

        private void ExitApp(InputAction.CallbackContext _) => ExitApp();
        public static void ExitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}
