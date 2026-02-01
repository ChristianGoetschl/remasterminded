using UnityEngine;
using UnityEngine.UI;

namespace FFX
{
    public class AddPiecePanel : MonoBehaviour
    {
        [SerializeField] private Button _addPieceBtn;
        [SerializeField] private Image _btnImg;
        [SerializeField] private Image _iconImg;
        [SerializeField] private GameObject _selectPiecePanel;

        [SerializeField] private Button _pieceBtnEmpty;
        [SerializeField] private Button _pieceBtnRed;
        [SerializeField] private Button _pieceBtnBlue;
        [SerializeField] private Button _pieceBtnGreen;
        [SerializeField] private Button _pieceBtnYellow;
        [SerializeField] private Button _pieceBtnPurple;
        [SerializeField] private Button _pieceBtnWhite;

        private CodeSlotController _codeSlot;
        private Color _originBtnColor;
        private Color _originIconColor;

        private void Awake()
        {
            _originBtnColor = _btnImg.color;
            _originIconColor = _iconImg.color;

            _addPieceBtn.onClick.AddListener(AddPieceBtnClicked);
            _pieceBtnEmpty.onClick.AddListener(PieceBtnEmptyClicked);
            _pieceBtnRed.onClick.AddListener(PieceBtnRedClicked);
            _pieceBtnBlue.onClick.AddListener(PieceBtnBlueClicked);
            _pieceBtnGreen.onClick.AddListener(PieceBtnGreenClicked);
            _pieceBtnYellow.onClick.AddListener(PieceBtnYellowClicked);
            _pieceBtnPurple.onClick.AddListener(PieceBtnPurpleClicked);
            _pieceBtnWhite.onClick.AddListener(PieceBtnWhiteClicked);
        }
        private void OnDestroy()
        {
            _codeSlot.OnUpdated -= CodeSlotUpdated;
            _addPieceBtn.onClick.RemoveAllListeners();
            _pieceBtnEmpty.onClick.RemoveListener(PieceBtnEmptyClicked);
            _pieceBtnRed.onClick.RemoveListener(PieceBtnRedClicked);
            _pieceBtnBlue.onClick.RemoveListener(PieceBtnBlueClicked);
            _pieceBtnGreen.onClick.RemoveListener(PieceBtnGreenClicked);
            _pieceBtnYellow.onClick.RemoveListener(PieceBtnYellowClicked);
            _pieceBtnPurple.onClick.RemoveListener(PieceBtnPurpleClicked);
            _pieceBtnWhite.onClick.RemoveListener(PieceBtnWhiteClicked);
        }

        public void InitCodeSlot(CodeSlotController codeSlot)
        {
            _codeSlot = codeSlot;
            _codeSlot.OnUpdated += CodeSlotUpdated;
        }

        private void CodeSlotUpdated()
        {
            // Show/Hide the add-piece-button graphic based on whether the slot is filled or not
            _btnImg.color = _codeSlot.IsFilled ? Color.clear : _originBtnColor;
            _iconImg.color = _codeSlot.IsFilled ? Color.clear : _originIconColor;
        }

        private void AddPieceBtnClicked() => _selectPiecePanel.SetActive(true);

        private void PieceBtnEmptyClicked() => PieceBtnClicked(PieceType.Empty);
        private void PieceBtnRedClicked() => PieceBtnClicked(PieceType.Red);
        private void PieceBtnBlueClicked() => PieceBtnClicked(PieceType.Blue);
        private void PieceBtnGreenClicked() => PieceBtnClicked(PieceType.Green);
        private void PieceBtnYellowClicked() => PieceBtnClicked(PieceType.Yellow);
        private void PieceBtnPurpleClicked() => PieceBtnClicked(PieceType.Purple);
        private void PieceBtnWhiteClicked() => PieceBtnClicked(PieceType.White);
        private void PieceBtnClicked(PieceType type)
        {
            _addPieceBtn.gameObject.SetActive(true);
            _codeSlot.FillPiece(type);
        }
    }
}
