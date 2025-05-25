using UnityEngine;

public class Cutter : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent(out Ingredient i)) {
            Debug.Log(GetMaterialCount(collision.gameObject));
        }
    }

    private static int GetMaterialCount(GameObject ingredient) {
        var meshRenderer = ingredient.GetComponentInChildren<MeshRenderer>();

        return meshRenderer ? meshRenderer.materials.Length : 1;
    }
}
