using UnityEngine;

public class MyRock : MonoBehaviour
{
    public GameObject[] rocks;
    public GameObject rock;

    public void Randomize()
    {
        rock = Instantiate(rocks[Random.Range(0, rocks.Length)], transform.position,
            Quaternion.Euler(0, Random.Range(0, 360), 0));
        rock.transform.SetParent(transform);
    }
}