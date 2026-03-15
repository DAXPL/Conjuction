using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ingredient Data")]
public class IngredientData : ScriptableObject {
    public string ingredientName;
    [SerializeField] private Potion ingredientProperties;

    public Potion GetIngredientProperties() => ingredientProperties;
}
