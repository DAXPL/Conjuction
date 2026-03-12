using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ingredient Data")]
public class IngredientData : ScriptableObject {
    public string ingredientName;
    [SerializeField] private Potion fullIngredientProperties;
    
    [Header("On Ingredient Cut")]
    [SerializeField] private int piecesOnCut = 2;
    [SerializeField] private Material[] piecesOnCutMaterial;
    [SerializeField] private int requiredHitCountCut = 3;
    [SerializeField] private Potion cutIngredientProperties;
    
    [Header("On Ingredient Smashed")]
    [SerializeField] private Material[] piecesOnSmashMaterial;
    [SerializeField] private int requiredHitCountSmash = 3;
    [SerializeField] private Potion smashedIngredientProperties;
    
    // How to set default value??
    private Potion currentIngredientProperties;
    private int hitCount = 0;
    private int currentRequiredHitCount;
    
    public void ResetHitCount() => hitCount = 0;
    public void IncreaseHitCount() => hitCount++;
    public void SetCurrentIngredientProperties(Potion potion) 
        => currentIngredientProperties = potion;
    
    public Potion GetCurrentIngredientProperties() => currentIngredientProperties;

    public int GetPiecesOnCut() => piecesOnCut;
    public int GetRequiredHitCount() => currentRequiredHitCount;
    public int GetHitCount() => hitCount;
    public Material[] GetPiecesOnCutMaterial() => piecesOnCutMaterial;
    
}
