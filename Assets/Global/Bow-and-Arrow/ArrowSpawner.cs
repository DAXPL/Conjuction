using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(AudioSource))]
public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject notch;
    [SerializeField] private PullInteraction pullInteraction;
    [SerializeField] private UnityEvent onRelease;
    private XRGrabInteractable bowInteraction;
    private GameObject currentArrow;
    private AudioSource audioSource;

    private float initialPith = 1;
    private float initialVolume = 0;

    private void Start()
    {
        bowInteraction = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();
        initialPith = audioSource.pitch;
        initialVolume = audioSource.volume;
        PullInteraction.PullActionReleased += NotchEmpty;
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchEmpty;
    }

    private void Update()
    {
        if(bowInteraction.isSelected && currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab, notch.transform);
        }
        if(!bowInteraction.isSelected && currentArrow != null)
        {
            currentArrow.GetComponent<Arrow>().ReleaseArrow(1);
            NotchEmpty(0);
        }
    }
    private void NotchEmpty(float value)
    {
        currentArrow = null;
        onRelease.Invoke();
    }

    [ContextMenu("Notch")]
    public void NotchArrow()
    {
        currentArrow = Instantiate(arrowPrefab, notch.transform);
        audioSource.pitch = initialPith*Random.Range(0.8f, 1.2f);
        audioSource.volume = initialVolume*Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}