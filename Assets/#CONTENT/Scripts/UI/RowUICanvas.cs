using UnityEngine;
using UnityEngine.UI;

namespace FFX
{
    [RequireComponent(typeof(Canvas))]
    public class RowUICanvas : MonoBehaviour
    {
        [SerializeField] private GuessRowController _rowController;
        [SerializeField] private AddPiecePanel[] _slotAddPiecePanels;
        [SerializeField] private Button _evaluateBtn;

        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();

            if (_rowController.CodeSlots.Length < _slotAddPiecePanels.Length)
            { Debug.LogError($" <<< Not enough {nameof(CodeSlotController)}s assigned! >>>"); return; }

            for (int i = 0; i < _slotAddPiecePanels.Length; i++)
                _slotAddPiecePanels[i].InitCodeSlot(_rowController.CodeSlots[i]);
        }

        private void OnEnable()
        {
            _canvas.enabled = true;
            UpdateUI();
        }
        private void OnDisable() => _canvas.enabled = false;

        public void UpdateUI()
        {
            // Check whether all slots of the active row have been filled
            _evaluateBtn.interactable = _rowController.AreAllSlotsFilled();
        }
    }
}
