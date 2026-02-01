using UnityEngine;

namespace FFX
{
    public class GuessRowController : CodeRowController
    {
        [SerializeField] private Material _evalVisPerfectMatchMat;
        [SerializeField] private Material _evalVisColorMatchMat;
        [SerializeField] private Renderer[] _evalVisRenderers;
        [SerializeField] private RowUICanvas _rowUICanvas;

        private void Awake()
        {
            foreach (CodeSlotController codeSlot in CodeSlots)
                codeSlot.OnUpdated += CodeSlotUpdated;
        }
        private void OnDestroy()
        {
            foreach (CodeSlotController codeSlot in CodeSlots)
                codeSlot.OnUpdated -= CodeSlotUpdated;
        }

        private void CodeSlotUpdated() => _rowUICanvas.UpdateUI();

        public void ResetRow()
        {
            foreach (CodeSlotController guessSlot in CodeSlots)
                guessSlot.ResetSlot();

            foreach (Renderer renderer in _evalVisRenderers)
                renderer.enabled = false;
        }

        public void Deactivate() => ToggleState(false);
        public void Activate() => ToggleState(true);
        private void ToggleState(bool isActive) => _rowUICanvas.enabled = isActive;

        public void ShowEvaluation(EvaluationState[] matchValues)
        {
            if (matchValues.Length < _evalVisRenderers.Length)
            { Debug.LogError(" <<< The match values count is less than the evaluation visualizer renderers! >>>"); return; }

            // Visualize the evaluation pins
            for (int i = 0; i < matchValues.Length; i++)
            {
                if (matchValues[i] == EvaluationState.PerfectMatch)
                {
                    _evalVisRenderers[i].enabled = true;
                    _evalVisRenderers[i].sharedMaterial = _evalVisPerfectMatchMat;
                }
                else if (matchValues[i] == EvaluationState.ColorMatch)
                {
                    _evalVisRenderers[i].enabled = true;
                    _evalVisRenderers[i].sharedMaterial = _evalVisColorMatchMat;
                }
                else _evalVisRenderers[i].enabled = false;
            }
        }

        public bool AreAllSlotsFilled()
        {
            bool allSlotsFilled = true;
            foreach (CodeSlotController slotController in CodeSlots)
                allSlotsFilled &= slotController.IsFilled;
            return allSlotsFilled;
        }
    }
}
