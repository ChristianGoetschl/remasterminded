using UnityEngine;

namespace FFX
{
    public class CodeRowController : MonoBehaviour
    {
        public CodeSlotController[] CodeSlots;

        public void CreateRandomCode()
        {
            foreach (CodeSlotController codeSlot in CodeSlots)
                codeSlot.FillRandomPiece();
        }

        public PieceType[] EvaluateRow()
        {
            PieceType[] rowValues = new PieceType[CodeSlots.Length];
            for (int i = 0; i < rowValues.Length; i++)
            {
                rowValues[i] = CodeSlots[i].Type;
                // If any of the slots is empty -> return invalid
                if (CodeSlots[i].Type == PieceType.Empty) return null;
            }
            return rowValues;
        }
    }
}
