using UnityEngine;

public class LiquidBaseProperties {
    private MeshRenderer liquidMeshRenderer;
    private ParticleSystem[] specialEffectToEnable;
    private ParticleSystem steamParticles;
        
    public LiquidBaseProperties(
        MeshRenderer liquidMeshRenderer, 
        Potion potion, 
        ParticleSystem[] specialEffectToEnable, 
        ParticleSystem steamParticles
    ) {
        this.specialEffectToEnable = specialEffectToEnable;
        this.liquidMeshRenderer = liquidMeshRenderer;
        this.steamParticles = steamParticles;
        UpdateWater(potion);
    }

    public void RemovePotionFromWater(MeshRenderer liquidMeshRenderer, Color baseColor) {
        this.liquidMeshRenderer = liquidMeshRenderer;
        liquidMeshRenderer.material.color = baseColor;
        SetGlowingIntensity(0);
        ToggleIsSteaming(false);
        DisableEffects();
    }
    
    private void UpdateWater(Potion potion) {
        SetColor(potion.potionColor);
        SetGlowingIntensity(potion.glowingIntensity);
        ToggleIsSteaming(potion.isSteaming);
        EnableEffects(potion.potionEffect);
    }

    private void SetColor(Vector3 color) {
        liquidMeshRenderer.material.color = 
            Color.HSVToRGB(
                color.x / 360f, 
                color.y / 100f, 
                color.z / 100f
            );
    }

    private void SetGlowingIntensity(float emissionStrength) {
        liquidMeshRenderer.materials[0].SetColor(
            "_EmissionColor", 
            liquidMeshRenderer.materials[0].color * emissionStrength
        );
        
        liquidMeshRenderer.materials[0].EnableKeyword("_EMISSION");
    }
    
    private void ToggleIsSteaming(bool isSteaming) {
        if (!isSteaming) {
            steamParticles.Stop();
            return;
        }
    
        steamParticles.Play();
    }

    private void EnableEffects(PotionEffect potionEffects) {
        foreach (var particle in specialEffectToEnable) {
            particle.Play();
        }
    }
    
    private void DisableEffects() {
        foreach (var particle in specialEffectToEnable) {
            particle.Stop();
        }
    }
}