using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform arrowTip;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private AudioClip[] hitsSounds;
    private AudioSource audioSource;
    private float initialPith = 1;
    private float initialVolume = 0;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        initialPith = audioSource.pitch;
        initialVolume = audioSource.volume;
        PullInteraction.PullActionReleased += ReleaseArrow;
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= ReleaseArrow;
    }

    public void ReleaseArrow(float val)
    {
        PullInteraction.PullActionReleased -= ReleaseArrow;
        gameObject.transform.parent = null;
        rb.isKinematic = false;

        Vector3 force = transform.forward * speed * val;
        rb.AddForce(force, ForceMode.Impulse);
        Destroy(gameObject, 60f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb.isKinematic) return;
        if (((1 << collision.gameObject.layer) & hitMask) != 0)
        {
            Debug.Log("Hit: " + collision.gameObject.name);
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.parent = collision.transform;
            if (collision.transform.TryGetComponent(out Rigidbody body))
            {
                body.AddForce(rb.linearVelocity, ForceMode.Impulse);
            }

            audioSource.pitch = initialPith * Random.Range(0.8f, 1.1f);
            audioSource.volume = initialVolume * Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(hitsSounds[0]);
        }
    }
}
