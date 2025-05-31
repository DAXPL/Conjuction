using UnityEngine;

public class Flint : MonoBehaviour
{
    [SerializeField] private GameObject sparksPrefab;
    [SerializeField] private float forceThreshold = 5f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Steel"))
        {
            float velocity = collision.relativeVelocity.magnitude;
            Debug.Log($"Flint struck! {velocity}");
            if (velocity >= forceThreshold)
            {
                Instantiate(sparksPrefab, collision.contacts[0].point, Quaternion.identity);

                Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f); // 50 cm promieñ
                foreach (Collider nearbyCollider in colliders)
                {
                    Fireplace fireplace = nearbyCollider.GetComponent<Fireplace>();
                    if (fireplace != null)
                    {
                        fireplace.MakeFire();
                        break;
                    }
                }
            }
        }
    }
}
