using TMPro;
using UnityEngine;

namespace FFX
{
    [RequireComponent(typeof(TMP_Text))]
    public class VersionNumberTxt : MonoBehaviour
    {
        [SerializeField] private string _prefix = "Version: ";

        private void Awake() => GetComponent<TMP_Text>().text = $"{_prefix}{Application.version}";
    }
}
