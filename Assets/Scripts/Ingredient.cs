using UnityEngine;

[System.Flags]
public enum PotionEffect {
    Sparkles = 1,
    Fire = 2,
    Explosion = 4,
    Lighting = 8,
}

public class Ingredient : MonoBehaviour {
    [Header("For potion properties")]
    [SerializeField] private Vector3 potionColor;
    [SerializeField] private float glowingIntensity;
    [SerializeField] private bool isSteaming;
    [SerializeField] private PotionEffect potionEffect; 
    [SerializeField] private int piecesOnCut = 2;
    [SerializeField] private Material[] piecesOnCutMaterial;
    [SerializeField] private int requiredHitCount = 3;
    private int hitCount = 0;
    public string ingredientName;
    
    public Vector3 GetPotionColor() => potionColor;
    public float GetGlowingIntensity() => glowingIntensity;
    public bool GetIsSteaming() => isSteaming;
    public PotionEffect GetPotionEffect() => potionEffect;
    public int GetPiecesOnCut() => piecesOnCut;
    public int GetRequiredHitCount() => requiredHitCount;
    public int GetHitCount() => hitCount;
    public void IncreaseHitCount() => hitCount++;
    public Material[] GetPiecesOnCutMaterial() => piecesOnCutMaterial;
}
