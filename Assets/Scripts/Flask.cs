using System.Linq;
using UnityEngine;

[SelectionBase]
public class Flask : MonoBehaviour {
    [SerializeField] private ParticleSystem steamParticles;
    [SerializeField] private MeshRenderer potionWater;
    
    [Header("Special effects particles")]
    [SerializeField] private ParticleSystem sparkles;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem lighting;
    private IngredientData[] currentPotion;
    private IngredientData[] perfectPotion;
    private LiquidBaseProperties potionInFlask;
    
    // Transfers the potion from the cauldron to the flask.
    public void CollectPotion(Potion potion, IngredientData[] currentPotion, IngredientData[] perfectPotion) {
        potionInFlask = new(
            potionWater, 
            potion, 
            PotionUtils.GetActiveEffects(
                potion.potionEffect, sparkles, fire, explosion, lighting
            ), 
            steamParticles
        );
        
        this.perfectPotion = perfectPotion; 
        this.currentPotion = currentPotion;
    }
    

    [ContextMenu("Drink")]
    // Consumes the potion and checks if it matches the perfect recipe.
    public void Drink() {
        if (perfectPotion.SequenceEqual(currentPotion)) {
            Debug.Log("You have perfect potion!");
        }
        
        Color emptyWaterColor = new Color(0, 0, 0, 0);

        potionInFlask.RemovePotionFromWater(potionWater, emptyWaterColor);
        currentPotion = null;
        potionInFlask = null;
    }
}