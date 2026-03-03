using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour {
    [SerializeField] private MeshRenderer potionWater;
    [SerializeField] private ParticleSystem steamParticles;
    
    [Header("Special effects particles")]
    [SerializeField] private ParticleSystem sparkles;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem lighting;

    public void CollectPotion(Potion potion) {
        LiquidBaseProperties _ = new(
            potionWater, 
            potion, 
            EnableEffects(potion.potionEffect), 
            steamParticles
        );
    }
    
    private ParticleSystem[] EnableEffects(PotionEffect potionEffects) {
        List<ParticleSystem> specialEffects = new();
        
        if ((potionEffects & PotionEffect.Sparkles) != 0)
            specialEffects.Add(sparkles);

        if ((potionEffects & PotionEffect.Fire) != 0)
            specialEffects.Add(fire);

        if ((potionEffects & PotionEffect.Explosion) != 0)
            specialEffects.Add(explosion);

        if ((potionEffects & PotionEffect.Lighting) != 0)
            specialEffects.Add(lighting);
        
        return specialEffects.ToArray();
    }
}