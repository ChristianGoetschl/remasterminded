using UnityEngine;

namespace FFX
{
    [ExecuteInEditMode]
    public class ToggleGroupObject : MonoBehaviour
    {
        // this script ensures that only one ToggleGroupObject within
        // the same hierarchy level is enabled at the same time
        private void OnEnable()
        {
            foreach (Transform t in transform.parent)
            {
                if (t.TryGetComponent(out ToggleGroupObject menuToggle))
                {
                    if (menuToggle == this) continue;
                    menuToggle.gameObject.SetActive(false);
                }
            }
        }
    }
}
