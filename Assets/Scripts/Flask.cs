using Oculus.Interaction;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class Flask : MonoBehaviour {
    [SerializeField] private ParticleSystem steamParticles;
    [SerializeField] private MeshRenderer potionWater;
    [SerializeField] private SnapInteractable bottleNeckSocket;
    [SerializeField] private GameObject cork;
    [Header("Special effects particles")]
    [SerializeField] private ParticleSystem sparkles;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem lighting;
    [SerializeField] private ParticleSystem liquidParticles;
    private IngredientData[] currentPotion;
    private IngredientData[] perfectPotion;
    private LiquidBaseProperties potionInFlask;
    [Space]
    [SerializeField] private float pourAngleThreshold = 90f;

    [SerializeField] private bool corked = false;
    private float maxLiquidLevel = 2.0f;//pouring time in seconds
    [SerializeField] private float liquidLevel = 2.0f;//pouring time in seconds
    private float maxEmissionRate = 100f;
    private Transform currentCorkTransform;

    private void Awake()
    {
        if (cork) cork.transform.parent = null;
    }
    private void OnEnable()
    {
        if (bottleNeckSocket != null)
        {
            bottleNeckSocket.WhenStateChanged += HandleInteractableStateChanged;
            if(liquidParticles) maxEmissionRate = liquidParticles.emission.rateOverTime.constant;
        }
    }

    private void OnDisable()
    {
        if (bottleNeckSocket != null)
        {
            bottleNeckSocket.WhenStateChanged -= HandleInteractableStateChanged;
        }
    }

    private void HandleInteractableStateChanged(InteractableStateChangeArgs args)
    {
        if (args.NewState == InteractableState.Select)
        {
            corked = true;
        }
        else if (args.PreviousState == InteractableState.Select && args.NewState != InteractableState.Select)
        {
            corked = false;
        }
    }

    private void FixedUpdate()
    {
        float tiltAngle = Vector3.Angle(Vector3.up, transform.up);

        bool isPouring = !corked && liquidLevel > 0 && tiltAngle > pourAngleThreshold;

        if (isPouring)
        {
            float pourIntensity = Mathf.InverseLerp(pourAngleThreshold, 180f, tiltAngle);

            if (liquidParticles && !liquidParticles.isPlaying)
            {
                liquidParticles.Play();
            }
            var emission = liquidParticles.emission;
            emission.rateOverTime = maxEmissionRate * pourIntensity; 

            // 4. Ubywanie płynu - użwamy mnożnika intensywności, więc butelka dnem do góry wyleje się szybciej niż ta lekko przechylona.
            liquidLevel -= Time.deltaTime;
        }
        else
        {
            if (liquidParticles && liquidParticles.isPlaying)
            {
                liquidParticles.Stop();
            }
        }
    }

    // Transfers the potion from the cauldron to the flask.
    public void CollectPotion(Potion potion, IngredientData[] currentPotion, IngredientData[] perfectPotion)
    {
        potionInFlask = new(
            potionWater,
            potion,
            PotionUtils.GetActiveEffects(
                potion.potionEffect, sparkles, fire, explosion, lighting
            ),
            steamParticles,
            liquidParticles
        );

        this.perfectPotion = perfectPotion;
        this.currentPotion = currentPotion;
        this.liquidLevel = maxLiquidLevel;
    }

    [ContextMenu("Drink")]
    // Consumes the potion and checks if it matches the perfect recipe.
    public void Drink() {
        if (perfectPotion.SequenceEqual(currentPotion)) {
            Debug.Log("You have perfect potion!");
        }
        
        Color emptyWaterColor = new Color(0, 0, 0, 0);

        potionInFlask.RemovePotionFromWater(potionWater, emptyWaterColor);
        currentPotion = null;
        potionInFlask = null;
    }
}