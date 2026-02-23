using System.Collections;
using UnityEngine;

public class CauldronEffects : MonoBehaviour {
    [SerializeField] private MeshRenderer cauldronWater;

    public void UpdateCauldronWater(Potion potion) {
        SetColor(potion.potionColor);
        SetGlowingIntensity(potion.glowingIntensity);
    }

    private void SetColor(Vector3 color) {
        cauldronWater.materials[0].color = 
            Color.HSVToRGB(
                color.x / 360f, 
                color.y / 100f, 
                color.z / 100f
            );
    }

    private void SetGlowingIntensity(float emissionStrength) {
        cauldronWater.materials[0].SetColor("_EmissionColor", cauldronWater.materials[0].color * emissionStrength);
        cauldronWater.materials[0].EnableKeyword("_EMISSION");
    }
}
