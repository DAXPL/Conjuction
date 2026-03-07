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
            EnableEffects(potion.potionEffect), 
            steamParticles
        );
        
        this.perfectPotion = perfectPotion; 
        currentPotion = potion;
    }
    private int perfectScore = 0;

    [ContextMenu("Drink")]
    public void Drink() {
        if (currentPotion.isSteaming == perfectPotion.isSteaming) 
            perfectScore++;

        if (Mathf.Approximately(
                currentPotion.glowingIntensity, 
                perfectPotion.glowingIntensity)
           ) {
            perfectScore++;
        }

        if (currentPotion.potionEffect == perfectPotion.potionEffect) 
            perfectScore++;

        if (Vector3.Distance(
                currentPotion.potionColor,
                perfectPotion.potionColor
            ) < 20f) {
            perfectScore++;
        }

        Debug.Log("The potion is " + (perfectScore/4) * 100 + "% perfect!");
        currentPotion = null;
        // TODO: Add empty the potion flask
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