using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float speed = 1f;

    public bool canFloat = true;

    float startHeight;
    private void Start()
    {
        startHeight = this.transform.position.y;
    }

    void Update()
    {
        if(!canFloat) return;
        this.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        this.transform.position = new Vector3(this.transform.position.x, startHeight+Mathf.Sin(Time.time * speed) * amplitude, this.transform.position.z);
    }

    public void SetCanFloat(bool canFloat)
    {
        this.canFloat = canFloat;
    }
}
