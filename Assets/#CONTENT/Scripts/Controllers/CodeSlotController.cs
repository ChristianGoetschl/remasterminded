using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FFX
{
    public class CodeSlotController : MonoBehaviour
    {
        public event Action OnUpdated;

        public PieceType Type { get; private set; }

        [SerializeField] private PrefabMapping _pieceMapping;
        [SerializeField] private Transform _objectParentTransform;

        public bool IsFilled => Type != PieceType.Empty;

        public void ResetSlot()
        {
            Type = PieceType.Empty;
            UpdateObject();
        }

        public void FillPiece(PieceType type)
        {
            Type = type;
            UpdateObject();
        }
        public void FillRandomPiece()
        {
            int rndIndex = Random.Range(1, Enum.GetNames(typeof(PieceType)).Length);
            FillPiece((PieceType)rndIndex);
        }

        private void UpdateObject()
        {
            // Destroy any filled gameobject slot
            int childCount = _objectParentTransform.childCount;
            for (int i = 0; i < childCount; i++)
                DestroyImmediate(_objectParentTransform.GetChild(0).gameObject);

            // Fill the slot with the corresponding instance
            GameObject go = _pieceMapping.GetPrefabGO(Type);
            if (go != null)
                Instantiate(go, _objectParentTransform, false);

            OnUpdated?.Invoke();
        }
    }
}
