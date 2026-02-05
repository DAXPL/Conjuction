using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;



    public void MakeFire()
    {
        if (firePrefab) firePrefab.SetActive(true);
    }

    public bool isFireplaceIgnited()
    {
        return firePrefab && firePrefab.activeSelf;
    }
}
