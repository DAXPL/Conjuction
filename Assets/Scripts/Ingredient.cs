using UnityEngine;

[System.Flags]
public enum PotionEffect {
    Sparkles = 1,
    Fire = 2,
    Explosion = 4,
    Lighting = 8,
}

public enum IngredientState {
    Whole,
    Cut,
    Crushed
}

public class Ingredient : MonoBehaviour {
    [SerializeField] private IngredientData ingredientData;
    [SerializeField] private int piecesOnCut = 2;
    [SerializeField] private Material[] piecesOnCutMaterial;
    [SerializeField] private int requiredHitCount = 3;
    [SerializeField] private float colorMultiplier;
    [SerializeField] private IngredientState ingredientState = IngredientState.Whole;
    private int hitCount = 0;

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
