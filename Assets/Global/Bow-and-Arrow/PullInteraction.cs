using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;

    [SerializeField] private Transform start, end;
    [SerializeField] private GameObject notch;

    [SerializeField] private float pullAmount = 0.0f;

    private LineRenderer lineRenderer;
    private IXRSelectInteractor pullingInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactorObject;
    }
    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        pullingInteractor = null;
        pullAmount = 0.0f;
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, 0f);
        UpdateString();
    }

    private void Update()
    {
        if (pullingInteractor!=null)
        {
            Transform interactorTransform = pullingInteractor?.GetAttachTransform(this)?.transform;
            Vector3 pullPosition = interactorTransform.position;
            pullAmount = CalculatePull(pullPosition); 
        }
        UpdateString();
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection)/maxLength;
        return Mathf.Clamp(pullValue, 0.0f, 1.0f);
    }

    private void UpdateString()
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, linePosition.z+0.4f);
        lineRenderer.SetPosition(1, linePosition);
    }
    public float getPull()
    {
        return pullAmount;
    }
}
