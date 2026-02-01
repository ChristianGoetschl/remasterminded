using System;
using System.Collections.Generic;
using UnityEngine;

namespace FFX
{
    public class GameBoardController : MonoBehaviour
    {
        public event Action OnNewGameStarted;
        public event Action OnGameWon;
        public event Action OnGameLost;

        [SerializeField] private CodeRowController _codeRow;
        [SerializeField] private GuessRowController[] _guessRows;

        private int _activeRowIndex;

        public void StartNewGame()
        {
            _activeRowIndex = 0;

            // Create a new random solution code
            _codeRow.CreateRandomCode();
            // Initialize all guess row slots
            InitializeGuessRowSlots();

            OnNewGameStarted?.Invoke();
        }

        private void InitializeGuessRowSlots()
        {
            foreach (GuessRowController row in _guessRows)
                row.ResetRow();

            _guessRows[0].Activate();
            for (int i = 1; i < _guessRows.Length; i++)
                _guessRows[i].Deactivate();
        }

        public void EvaluateActiveRow()
        {
            if (_activeRowIndex >= _guessRows.Length)
            { Debug.Log(" <<< Game is already over >>>"); return; }

            // Check if the active row is filled
            PieceType[] activeValues = _guessRows[_activeRowIndex].EvaluateRow();
            if (activeValues == null)
            { Debug.Log(" <<< Active row is not yet filled! >>>"); return; }

            // Compare and rate the active row entries
            PieceType[] codeValues = _codeRow.EvaluateRow();
            EvaluationState[] matchValues = EvaluateRows(codeValues, activeValues);
            _guessRows[_activeRowIndex].ShowEvaluation(matchValues);
            _guessRows[_activeRowIndex].Deactivate();

            // Check if the game is won
            bool gameWon = true;
            foreach (EvaluationState matchVal in matchValues)
                gameWon &= matchVal == EvaluationState.PerfectMatch;
            if (gameWon)
                OnGameWon?.Invoke();
            else
            {
                // Move on to the next row index
                if (_activeRowIndex < _guessRows.Length - 1)
                {
                    _activeRowIndex++;
                    _guessRows[_activeRowIndex].Activate();
                }
                else OnGameLost?.Invoke();
            }
        }

        public void FillActiveRowWithRandomGuess() =>
            _guessRows[_activeRowIndex].CreateRandomCode();

        public static EvaluationState[] EvaluateRows(PieceType[] codeValues, PieceType[] activeRowValues)
        {
            if (codeValues.Length != activeRowValues.Length) return null;
            int perfectMatchCount = 0;
            int colorMatchCount = 0;

            // Remember which colors of the code have already been processed
            List<PieceType> codeColors = new List<PieceType>();
            for (int i = 0; i < codeValues.Length; i++)
            {
                // Count the perfect matches
                if (codeValues[i] == activeRowValues[i])
                    perfectMatchCount++;

                if (codeColors.Contains(codeValues[i])) continue;
                codeColors.Add(codeValues[i]);

                // Count how many times this color appears in the code
                int codeColorCount = 1;
                for (int j = i + 1; j < codeValues.Length; j++)
                {
                    if (codeValues[j] == codeValues[i])
                        codeColorCount++;
                }

                // Count how many times this color appears in the guess
                int rowColorCount = 0;
                for (int j = 0; j < activeRowValues.Length; j++)
                {
                    if (activeRowValues[j] == codeValues[i])
                        rowColorCount++;
                }

                // Count the color matches
                colorMatchCount += Math.Min(rowColorCount, codeColorCount);
            }
            codeColors.Clear();

            EvaluationState[] matchValues = new EvaluationState[codeValues.Length];
            // The perfect matches are also color matches
            colorMatchCount -= perfectMatchCount;
            for (int i = 0; i < matchValues.Length; i++)
            {
                if (perfectMatchCount > 0)
                {
                    matchValues[i] = EvaluationState.PerfectMatch;
                    perfectMatchCount--;
                }
                else if (colorMatchCount > 0)
                {
                    matchValues[i] = EvaluationState.ColorMatch;
                    colorMatchCount--;
                }
                else matchValues[i] = EvaluationState.NoMatch;
            }
            return matchValues;
        }
    }
}
