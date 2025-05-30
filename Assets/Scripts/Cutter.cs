using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Cutter : MonoBehaviour {
    [SerializeField] private GameObject cutPiecePrefab;
    [SerializeField] private AudioClip[] piecesPopOutSound;
    [SerializeField] private AudioClip[] cutSound;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.TryGetComponent(out Ingredient i))
            return;

        if (i.GetPiecesOnCut() == 0)
            return;

        i.IncreaseHitCount();
        if (cutSound.Length != 0)
            PlaySound(cutSound);

        if (i.GetHitCount() != i.GetRequiredHitCount())
            return;

        CreateCutPieces(i.GetPiecesOnCut(), i);
        Destroy(collision.gameObject);
    }

    private void CreateCutPieces(int piecesOnCut, Ingredient ingredient) {
        var materials = ingredient.GetPiecesOnCutMaterial();

        for (int i = 0; i < piecesOnCut; i++) {
            if (piecesPopOutSound.Length != 0)
                PlaySound(piecesPopOutSound);

            GameObject piece = Instantiate(cutPiecePrefab, ingredient.transform.localPosition, Quaternion.identity);

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

    private void PlaySound(AudioClip[] clip) {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip[Random.Range(0, clip.Length)]);
    }
}
