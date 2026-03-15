using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Cauldron : MonoBehaviour
{
    [Header("Recipe Settings")]
    [SerializeField] private List<RecipePart> recipe = new();
    [SerializeField] private UnityEvent<Potion> onIngredientAdded;
    [SerializeField] private UnityEvent onRecipeComplete;
    [SerializeField] private UnityEvent onWrongIngredientAdded;
    [SerializeField] private UnityEvent onGoodIngredientAdded;
    [SerializeField] private Fireplace fireplace;
    [SerializeField] private IngredientData[] perfectPotionIngredients;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] waterClips;
    [SerializeField] private AudioClip[] badIngredientAddedClips;

    private Potion potion;
    private List<IngredientData> currentPotionIngredients = new();
    private AudioSource audioSource;
    private CauldronEffects cauldronEffects;


    private void Awake()
    {
        cauldronEffects = GetComponent<CauldronEffects>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        for (int i = 0; i < recipe.Count; i++)
        {
            SetIngredientAmountText(i);
        }
    }
    
    // Triggers when a Flask or Ingredient enters the cauldron
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is a Flask
        if (other.TryGetComponent(out Flask flask)) {
            // If there's no potion made yet, do nothing
            if (potion == null)
                return;
            
            // Transfer the potion and ingredient data to the flask
            flask.CollectPotion(potion, currentPotionIngredients.ToArray(), perfectPotionIngredients);

            cauldronEffects.RemoveCauldronWater();
            potion = null;
            currentPotionIngredients.Clear();
            return;
        }

        // Check if the object entering the trigger is an Ingredient
        if (!other.TryGetComponent(out Ingredient ing))
            return;
        
        if (fireplace && !fireplace.isFireplaceIgnited())
            return;
        
        Destroy(other.gameObject);
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(waterClips[UnityEngine.Random.Range(0, waterClips.Length)]);

        Debug.Log("Ingredient added!");
        
        // Save the added ingredient
        currentPotionIngredients.Add(ing.GetIngredientData());
        
        // Update the resulting potion's stats
        UpdatePotionStats(ing);
        onGoodIngredientAdded?.Invoke();
    }
    
    // Updates the current potion's stats based on the added ingredient.
    private void UpdatePotionStats(Ingredient ing) {
        // Retrieve properties from the ingredient
        Potion potionTemp = ing.GetIngredientData().GetIngredientProperties();
        float colorMultiplier = ing.GetColorMultiplier();

        // Initialize a new potion and copy visual properties
        potion = new ();
        potion.glowingIntensity = potionTemp.glowingIntensity;
        potion.isSteaming = potionTemp.isSteaming;
        ClampColor(potionTemp.potionColor * colorMultiplier);
        potion.potionEffect = potionTemp.potionEffect;
        onIngredientAdded.Invoke(potion);
    }

    
    // Updates the potion's color and clamps its components.
    private void ClampColor(Vector3 potionColor) {
        potion.potionColor += potionColor;
        
        // Clamp color to Vector3 X to 0-360
        potion.potionColor.x %= 360;
        
        // Clamp color to Vector3 Y,Z to 100
        potion.potionColor.y %= 100;
        potion.potionColor.z %= 100;
        
        potion.potionColor.y = Mathf.Clamp(potion.potionColor.y, 30f, 100f);
        potion.potionColor.z = Mathf.Clamp(potion.potionColor.z, 30f, 100f);
    }
    
    private void SetIngredientAmountText(int i) {
        if (recipe[i].text)
            recipe[i].text.SetText($"{recipe[i].amount}");
    }

    private bool IsComplete()
    {
        for (int i = 0; i < recipe.Count; i++)
        {
            if (recipe[i].amount > 0)
            {
                return false;
            }
        }
        return true;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

[Serializable]
public class RecipePart
{
    public string ingredientName;
    public int amount;
    public TextMeshPro text;
}
