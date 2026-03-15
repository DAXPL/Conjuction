using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
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

[Serializable]
public class Potion {
    public Vector3 potionColor;
    public float glowingIntensity;
    public bool isSteaming;
    public PotionEffect potionEffect;
}

public static class PotionUtils {
    public static ParticleSystem[] GetActiveEffects(
        PotionEffect effects,
        ParticleSystem sparkles,
        ParticleSystem fire,
        ParticleSystem explosion,
        ParticleSystem lighting) {
        
        List<ParticleSystem> activeEffects = new();
        
        if ((effects & PotionEffect.Sparkles) != 0) activeEffects.Add(sparkles);
        if ((effects & PotionEffect.Fire) != 0) activeEffects.Add(fire);
        if ((effects & PotionEffect.Explosion) != 0) activeEffects.Add(explosion);
        if ((effects & PotionEffect.Lighting) != 0) activeEffects.Add(lighting);
        
        return activeEffects.ToArray();
    }
}
