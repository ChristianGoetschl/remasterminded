using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FFX
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneBtn : MonoBehaviour
    {
        [SerializeField] private int _sceneLoadBuildIndex = 1;

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

        private void BtnClicked() => SceneManager.LoadScene(_sceneLoadBuildIndex);
    }
}
