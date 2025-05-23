using System.Collections.Generic;
using UnityEngine;

public class CauldronEffects : MonoBehaviour {

    public void WrongIngredientEffect(GameObject water) {
        if (water.TryGetComponent(out MeshRenderer mR)) {
            List<Material> materials = new(mR.materials);

            materials[0].color = Color.red;
        }
    }
}
