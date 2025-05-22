using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private List< RecipePart> recipe = new();
    [SerializeField] private UnityEvent onIngredientAdded;
    [SerializeField] private UnityEvent onRecipeComplete;
    private bool boiled = false;

    [SerializeField] private AudioClip[] waterClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        for (int i = 0; i < recipe.Count; i++)
        {
            SetIngriedientAmountText(i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ingredient>(out Ingredient ing))
        {
            onIngredientAdded.Invoke();
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(waterClips[UnityEngine.Random.Range(0, waterClips.Length)]);

            if (recipe[0].ingredientName == ing.ingredientName) {
                recipe[0].amount--;
            }

            Destroy(other.gameObject);
            if(!boiled && IsComplete())
            {
                onRecipeComplete.Invoke();
                boiled = true;
                Debug.Log("Recipe complete!");
            }
            UpdateRecipe();
        }
        
    }
    private void UpdateRecipe()
    {
        SetIngriedientAmountText(0);

        if (recipe[0].amount == 0)
            recipe.RemoveAt(0);
    }

    private void SetIngriedientAmountText(int i) {
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
[System.Serializable]
public class RecipePart
{
    public string ingredientName;
    public int amount;
    public TextMeshPro text;
}
