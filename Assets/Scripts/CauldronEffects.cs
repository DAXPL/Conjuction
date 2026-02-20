using System.Collections;
using UnityEngine;

public class CauldronEffects : MonoBehaviour {
    [SerializeField] private BoxCollider cauldronWater;

    public void WrongIngredientEffect(GameObject water) {
        StartCoroutine(ChangeWaterColor(water));
    }

    public void UpdateCauldronWater(Potion potion) {
        if (!cauldronWater.TryGetComponent(out MeshRenderer mR))
            Debug.LogError("No mesh renderer");

        mR.materials[0].color = Color.HSVToRGB(potion.potionColor.x / 360f, potion.potionColor.y / 100f, potion.potionColor.z / 100f);
    }

    private static IEnumerator ChangeWaterColor(GameObject water) {
        if (!water.TryGetComponent(out MeshRenderer mR))
            yield return null;

        var startingColor = mR.materials[0].color;

        mR.materials[0].color = Color.red;
        yield return new WaitForSeconds(2);
        mR.materials[0].color = startingColor;

    }
}
