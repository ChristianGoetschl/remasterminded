using UnityEngine;

namespace FFX
{
    [CreateAssetMenu(fileName = "PrefabMapping", menuName = "FFX/PrefabMapping")]
    public class PrefabMapping : ScriptableObject
    {
        public GameObject PiecePrefabRed;
        public GameObject PiecePrefabBlue;
        public GameObject PiecePrefabGreen;
        public GameObject PiecePrefabYellow;
        public GameObject PiecePrefabPurple;
        public GameObject PiecePrefabWhite;

        public GameObject GetPrefabGO(PieceType type)
        {
            if (type == PieceType.Empty)
                return null;
            else if (type == PieceType.Red)
                return PiecePrefabRed;
            else if (type == PieceType.Blue)
                return PiecePrefabBlue;
            else if (type == PieceType.Green)
                return PiecePrefabGreen;
            else if (type == PieceType.Yellow)
                return PiecePrefabYellow;
            else if (type == PieceType.Purple)
                return PiecePrefabPurple;
            else if (type == PieceType.White)
                return PiecePrefabWhite;

            Debug.Log($" <<< Type: {type} is not defined! >>>");
            return null;
        }
    }
}
