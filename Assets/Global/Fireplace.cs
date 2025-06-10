using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;

    private void Start()
    {
        if(firePrefab) firePrefab.SetActive(false);
    }

    public void MakeFire()
    {
        if (firePrefab) firePrefab.SetActive(true);
    }

    public bool isFireplaceIgnited()
    {
        return firePrefab && firePrefab.activeSelf;
    }
}
