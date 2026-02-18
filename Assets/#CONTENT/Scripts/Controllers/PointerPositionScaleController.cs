using UnityEngine;

namespace FFX
{
    public class PointerPositionScaleController : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private Vector3 _originScale;
        private float _minScale;

        private void Awake()
        {
            _originScale = transform.localScale;
            _minScale = _scaleCurve.Evaluate(_scaleCurve.keys[^1].time);
        }

        private void Update()
        {
            if (PointerPosTrackerManager.PointerPos.HasValue)
            {
                float distanceFromPointer = Vector3.Distance(transform.position, PointerPosTrackerManager.PointerPos.Value);
                transform.localScale = _originScale * _scaleCurve.Evaluate(distanceFromPointer);
            }
            else transform.localScale = _originScale * _minScale;
        }
    }
}
