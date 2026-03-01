using UnityEngine;

public class CauldronEffects : MonoBehaviour {
    [SerializeField] private MeshRenderer cauldronWater;
    [SerializeField] private ParticleSystem steamParticles;
    
    [Header("Special effects particles")]
    [SerializeField] private ParticleSystem sparkles;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem lighting;

    public void UpdateCauldronWater(Potion potion) {
        SetColor(potion.potionColor);
        SetGlowingIntensity(potion.glowingIntensity);
        ToggleIsSteaming(potion.isSteaming);
        EnableEffects(potion.potionEffect);
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
    
    private void ToggleIsSteaming(bool isSteaming) {
        if (!isSteaming) {
            steamParticles.Stop();
            return;
        }

        steamParticles.Play();
    }

    private void EnableEffects(PotionEffect potionEffects) {
        if ((potionEffects & PotionEffect.Sparkles) != 0)
            sparkles.Play();

        if ((potionEffects & PotionEffect.Fire) != 0)
            fire.Play();

        if ((potionEffects & PotionEffect.Explosion) != 0)
            explosion.Play();

        if ((potionEffects & PotionEffect.Lighting) != 0)
            lighting.Play();
    }
}
