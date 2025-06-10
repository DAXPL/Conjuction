using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField] private int bursts = 20;
    [SerializeField] private int burst = 1;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private GameObject[] staticObjects;
    private Collider areaCollider;
    [SerializeField] private LayerMask layerMask;

    void Start()
    {
        areaCollider = GetComponent<Collider>();
        StartCoroutine(StartingCorountine());
    }

    private IEnumerator StartingCorountine()
    {
        yield return null;
        for (int i = 0; i < staticObjects.Length; i++)
        {
            staticObjects[i].SetActive(false);
        }
        
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < bursts; i++)
        {
            SpawnInRange(burst);
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
        for (int i=0; i < staticObjects.Length; i++)
        {
            staticObjects[i].SetActive(true);
        }
    }
    public void SpawnInRange(int number)
    {
        if (areaCollider == null || prefabs.Length<=0) return;

        int spawned = 0;
        int maxTries = 100;
        int tries = 0;

        while (spawned < number && tries < maxTries)
        {
            Vector3 losowaPozycja = RandomColiderPoint(areaCollider);

            Vector3 rayStart = new Vector3(losowaPozycja.x, areaCollider.bounds.max.y + 1.5f, losowaPozycja.z);
            Vector3 rayDirection = Vector3.down;

            if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hitInfo, 5f, layerMask))
            {
                Instantiate(prefabs[Random.Range(0,prefabs.Length)], hitInfo.point, Quaternion.identity);
                spawned++;
            }

            tries++;
        }
    }

    private Vector3 RandomColiderPoint(Collider col)
    {
        Vector3 min = col.bounds.min;
        Vector3 max = col.bounds.max;

        return new Vector3(
            Random.Range(min.x, max.x),
            0,
            Random.Range(min.z, max.z)
        );
    }
}
