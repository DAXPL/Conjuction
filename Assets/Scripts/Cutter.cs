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
        var materials = ingredient.GetPiecesOnCutMaterial();

        for (int i = 0; i < piecesOnCut; i++) {
            GameObject piece = Instantiate(cutPiecePrefab);

            if (piece.TryGetComponent(out Ingredient cutIngredient))
                cutIngredient.ingredientName = ingredient.ingredientName;

            if (!piece.TryGetComponent(out Renderer r))
                return;

            if (materials.Length == 0)
                continue;

            var materialIndex = 0;
            switch (materials.Length) {
                case 1:
                    break;

                default:
                    materialIndex = Mathf.Min(i, materials.Length - 1);

                    if (materials.Length > ingredient.GetPiecesOnCut()) {
                        materialIndex = Random.Range(0, materials.Length - 1);
                    }

                    break;
            }

            r.material = materials[materialIndex];
        }
    }
}
