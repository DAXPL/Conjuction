using UnityEngine;

public class Cutter : MonoBehaviour {
    [SerializeField] private GameObject cutPiecePrefab;

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.TryGetComponent(out Ingredient i))
            return;

        if (i.GetPiecesOnCut() == 0)
            return;

        i.IncreaseHitCount();
        if (i.GetHitCount() != i.GetRequiredHitCount())
            return;

        CreateCutPieces(i.GetPiecesOnCut(), i);
        Destroy(collision.gameObject);
    }

    private void CreateCutPieces(int piecesOnCut, Ingredient ingredient) {
        var originalIngredient = ingredient.gameObject;
        var materials = GetMaterials(originalIngredient);

        for (int i = 0; i < piecesOnCut; i++) {
            GameObject piece = Instantiate(cutPiecePrefab);

            if (piece.TryGetComponent(out Ingredient cutIngredient))
                cutIngredient.ingredientName = ingredient.ingredientName;


            if (!piece.TryGetComponent(out Renderer r))
                return;

            if (materials.Length == 1) {
                r.material = materials[0];
                return;
            }

            r.material = materials[i];

        }
    }

    private static Material[] GetMaterials(GameObject obj) {
        var meshRenderer = obj.GetComponentInChildren<MeshRenderer>();
        return meshRenderer.materials;
    }
}
