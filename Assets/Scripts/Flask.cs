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
    private Potion currentPotion;
    private Potion perfectPotion;
    
    public void CollectPotion(Potion potion, Potion perfectPotion) {
        LiquidBaseProperties _ = new(
            potionWater, 
            potion, 
            PotionUtils.GetActiveEffects(potion.potionEffect, sparkles, fire, explosion, lighting), 
            steamParticles
        );
        
        this.perfectPotion = perfectPotion; 
        currentPotion = potion;
    }
    

    [ContextMenu("Drink")]
    public void Drink() {
        // TODO: Add empty the potion flask
    }
}