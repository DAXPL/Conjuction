using UnityEngine;

public class LiquidBaseProperties
{
    private MeshRenderer liquidMeshRenderer;
    private ParticleSystem[] specialEffectToEnable;
    private ParticleSystem steamParticles;
    private ParticleSystem liquidParticles;

    // Initializes the liquid properties and updates its visual state.
    public LiquidBaseProperties(
        MeshRenderer liquidMeshRenderer,
        Potion potion,
        ParticleSystem[] specialEffectToEnable,
        ParticleSystem steamParticles,
        ParticleSystem liquidParticles
    )
    {
        this.specialEffectToEnable = specialEffectToEnable;
        this.liquidMeshRenderer = liquidMeshRenderer;
        this.steamParticles = steamParticles;
        this.liquidParticles = liquidParticles;
        UpdateWater(potion);
    }

    // Resets the container liquid properties.
    public void RemovePotionFromWater(MeshRenderer liquidMeshRenderer, Color baseColor)
    {
        this.liquidMeshRenderer = liquidMeshRenderer;
        liquidMeshRenderer.material.color = baseColor;

        // Resetowanie koloru cząsteczek przy opróżnianiu
        ApplyColorToParticles(baseColor);

        SetGlowingIntensity(0);
        ToggleIsSteaming(false);
        DisableEffects();
    }

    // Updates liquid color, emission, steam, and effects.
    private void UpdateWater(Potion potion)
    {
        SetColor(potion.potionColor);
        SetGlowingIntensity(potion.glowingIntensity);
        ToggleIsSteaming(potion.isSteaming);
        EnableEffects(potion.potionEffect);
    }

    private void SetColor(Vector3 color)
    {
        Color rgbColor = Color.HSVToRGB(
            color.x / 360f,
            color.y / 100f,
            color.z / 100f
        );

        liquidMeshRenderer.material.color = rgbColor;

        ApplyColorToParticles(rgbColor);
    }

    private void ApplyColorToParticles(Color targetColor)
    {
        if (liquidParticles == null) return;

        ParticleSystem[] allParticles = liquidParticles.GetComponentsInChildren<ParticleSystem>(true);

        foreach (ParticleSystem ps in allParticles)
        {
            ParticleSystem.MainModule main = ps.main;
            main.startColor = targetColor;
            ParticleSystemRenderer renderer = ps.GetComponent<ParticleSystemRenderer>();
            if (renderer != null)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (mat.HasProperty("_BaseColor"))
                    {
                        mat.SetColor("_BaseColor", targetColor);
                    }
                    else if (mat.HasProperty("_Color"))
                    {
                        mat.SetColor("_Color", targetColor);
                    }
                }
            }
        }
    }

    private void SetGlowingIntensity(float emissionStrength)
    {
        liquidMeshRenderer.materials[0].SetColor(
            "_EmissionColor",
            liquidMeshRenderer.materials[0].color * emissionStrength
        );

        liquidMeshRenderer.materials[0].EnableKeyword("_EMISSION");
    }

    private void ToggleIsSteaming(bool isSteaming)
    {
        if (!isSteaming)
        {
            steamParticles.Stop();
            return;
        }

        steamParticles.Play();
    }

    private void EnableEffects(PotionEffect potionEffects)
    {
        foreach (var particle in specialEffectToEnable)
        {
            particle.Play();
        }
    }

    private void DisableEffects()
    {
        foreach (var particle in specialEffectToEnable)
        {
            particle.Stop();
        }
    }
}