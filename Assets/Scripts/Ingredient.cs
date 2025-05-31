using UnityEngine;

public class Ingredient : MonoBehaviour {
    [SerializeField] private int piecesOnCut = 2;
    [SerializeField] private Material[] piecesOnCutMaterial;
    [SerializeField] private int requiredHitCount = 3;
    private int hitCount = 0;
    public string ingredientName;

    public int GetPiecesOnCut() => piecesOnCut;
    public int GetRequiredHitCount() => requiredHitCount;
    public int GetHitCount() => hitCount;
    public void IncreaseHitCount() => hitCount++;
    public Material[] GetPiecesOnCutMaterial() => piecesOnCutMaterial;
}
