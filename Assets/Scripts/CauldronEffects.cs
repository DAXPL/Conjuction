using System.Collections;
using UnityEngine;

public class CauldronEffects : MonoBehaviour {

    public void WrongIngredientEffect(GameObject water) {
        StartCoroutine(ChangeWaterColor(water));
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
