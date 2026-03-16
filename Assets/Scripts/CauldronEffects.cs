using System;
using System.Collections.Generic;
using UnityEngine;

public class CauldronEffects : MonoBehaviour{
    [SerializeField] private ParticleSystem steamParticles;
    [SerializeField] private MeshRenderer cauldronWater;
    
    [Header("Special effects particles")]
    [SerializeField] private ParticleSystem sparkles;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem lighting;
    private LiquidBaseProperties potionInCauldron;
    private Color startWaterColor;

    private void Start() {
        startWaterColor = cauldronWater.material.color;
    }

    // Updates the cauldron water visual properties
    public void UpdateCauldronWater(Potion potion) {
        potionInCauldron = new(
            cauldronWater, 
            potion, 
            EnableEffects(potion.potionEffect), 
            steamParticles
        );
    }
    
    // Resets the cauldron liquid to its original state
    public void RemoveCauldronWater() {
        potionInCauldron.RemovePotionFromWater(cauldronWater, startWaterColor);
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
