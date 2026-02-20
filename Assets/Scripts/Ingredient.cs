using UnityEngine;

public class Ingredient : MonoBehaviour {
    [Header("For potion properties")]
    [SerializeField] private Vector3 potionColor;
    [SerializeField] private bool isGlowing;
    [SerializeField] private bool isSteaming;
    [SerializeField, Range(0, 4)] private int potionEffect;
    
    [SerializeField] private int piecesOnCut = 2;
    [SerializeField] private Material[] piecesOnCutMaterial;
    [SerializeField] private int requiredHitCount = 3;
    private int hitCount = 0;
    public string ingredientName;
    
    public Vector3 GetPotionColor() => potionColor;
    public bool GetIsGlowing() => isGlowing;
    public bool GetIsSteaming() => isSteaming;
    public int GetPotionEffect() => potionEffect;
    public int GetPiecesOnCut() => piecesOnCut;
    public int GetRequiredHitCount() => requiredHitCount;
    public int GetHitCount() => hitCount;
    public void IncreaseHitCount() => hitCount++;
    public Material[] GetPiecesOnCutMaterial() => piecesOnCutMaterial;
}
