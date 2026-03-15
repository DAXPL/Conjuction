using UnityEngine;

public class Ingredient : MonoBehaviour {
    [SerializeField] private IngredientData ingredientData;
    private int hitCount = 0;
    [SerializeField, Tooltip("Pieces it splits into when cut")] private int piecesOnCut = 2;
    [SerializeField] private Material[] piecesOnCutMaterial;
    [SerializeField, Tooltip("Hits required for cutting/crushing")] private int requiredHitCount = 3;
    [SerializeField, Tooltip("Multiplier for potion color")] private float colorMultiplier;
    [SerializeField] private IngredientState ingredientState = IngredientState.Whole; 

    // Calculates color multiplier based on state (Whole, Cut, or Crushed).
    public float GetColorMultiplier() {
        switch (ingredientState) {
            case IngredientState.Whole:
                return 1;
            
            case IngredientState.Cut:
                return colorMultiplier;
            
            case IngredientState.Crushed:
                return colorMultiplier * 1.2f;
            
            default:
                return 1;
        }
    }
    public int GetHitCount() => hitCount;
    public int GetPiecesOnCut() => piecesOnCut;
    public int GetRequiredHitCount() => requiredHitCount;
    public Material[] GetPiecesOnCutMaterial() => piecesOnCutMaterial;
    public IngredientState GetIngredientState() => ingredientState;
    public IngredientData GetIngredientData() => ingredientData;
    public void IncreaseHitCount() => hitCount++;
}
