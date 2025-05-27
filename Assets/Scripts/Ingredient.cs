using UnityEngine;

public class Ingredient : MonoBehaviour {
    [SerializeField] private int piecesOnCut = 2;
    public string ingredientName;

    public int GetPiecesOnCut() => piecesOnCut;
}
