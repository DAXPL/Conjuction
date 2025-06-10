using UnityEngine;
using UnityEngine.Audio;

public class Flitt : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private AudioClip[] clips;
    private AudioSource audioSource;
    private float chirpsTime = 0;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        chirpsTime = Time.time + Random.Range(10f, 15f);
    }

    void Update()
    {
        if(target)transform.LookAt(target.position + offset); 
        if(Time.time > chirpsTime)
        {
            audioSource.pitch = Random.Range(0.9f, 1.2f);
            audioSource.PlayOneShot(clips[Random.Range(0,clips.Length)]);
            chirpsTime = Time.time + Random.Range(60f, 240f);
        }
    }
}
