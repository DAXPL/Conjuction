using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private List<RecipePart> recipe = new();
    [SerializeField] private UnityEvent<Potion> onIngredientAdded;
    [SerializeField] private UnityEvent onRecipeComplete;
    [SerializeField] private UnityEvent onWrongIngredientAdded;
    [SerializeField] private UnityEvent onGoodIngredientAdded;
    [SerializeField] private Fireplace fireplace;
    [SerializeField] private IngredientData[] perfectPotionIngredients;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Flask flask)) {
            if (potion == null)
                return;
            
            flask.CollectPotion(potion, currentPotionIngredients.ToArray(), perfectPotionIngredients);
            cauldronEffects.RemoveCauldronWater();
            potion = null;
            currentPotionIngredients.Clear();
            return;
        }

        if (!other.TryGetComponent(out Ingredient ing))
            return;
        
        if (fireplace && !fireplace.isFireplaceIgnited())
            return;

        Destroy(other.gameObject);
        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(waterClips[UnityEngine.Random.Range(0, waterClips.Length)]);

        Debug.Log("Ingredient added!");
        currentPotionIngredients.Add(ing.GetIngredientData());
        UpdatePotionStats(ing);
        onGoodIngredientAdded?.Invoke();
    }

    private void UpdatePotionStats(Ingredient ing) {
        Potion potionTemp = ing.GetIngredientData().GetIngredientProperties();
        float colorMultiplier = ing.GetColorMultiplier();

        potion = new ();
        potion.glowingIntensity = potionTemp.glowingIntensity;
        potion.isSteaming = potionTemp.isSteaming;
        ClampColor(potionTemp.potionColor * colorMultiplier);
        potion.potionEffect = potionTemp.potionEffect;
        onIngredientAdded.Invoke(potion);
    }

    
    // TODO: Add comments
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
