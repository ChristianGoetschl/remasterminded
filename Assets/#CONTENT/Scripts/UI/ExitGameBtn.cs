using UnityEngine;
using UnityEngine.UI;

namespace FFX
{
    [RequireComponent(typeof(Button))]
    public class ExitGameBtn : MonoBehaviour
    {
        private Button _btn;

        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(BtnClicked);
        }
        private void OnDestroy()
        {
            _btn.onClick.RemoveAllListeners();
        }

        private void BtnClicked() => ExitAppManager.ExitApp();
    }
}
