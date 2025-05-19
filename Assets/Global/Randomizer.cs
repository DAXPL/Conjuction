using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private bool exclusive = true;
    [SerializeField] private int exclusiveChance;
    void Start()
    {
        if (exclusive)
        {
            int id = Random.Range(0, objects.Length);
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == id);
            }
        }
        else
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(Random.Range(0, 10) <= exclusiveChance);
            }
        }
    }
}
