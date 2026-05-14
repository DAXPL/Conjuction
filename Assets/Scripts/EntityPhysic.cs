using UnityEngine;

public class EntityPhysic : MonoBehaviour
{
    private const float IMPULSE_THRESHOLD = 0.1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(rb == null) return;
        float impulseMagnitude = collision.impulse.magnitude;
        //Debug.Log($"Hit:{impulseMagnitude}");
        if(impulseMagnitude>= IMPULSE_THRESHOLD)
        {
            rb.isKinematic = false;
        }
    }
}
