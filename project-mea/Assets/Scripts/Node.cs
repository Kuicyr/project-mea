using UnityEngine;

public class Node : MonoBehaviour
{
    public int type;
    public bool isChanged;
    public GameObject current;
    public GameObject onBlock;
    public GameObject[] gameObjects;

    private void Start()
    {
        isChanged = false;
    }

    public void ChangeType()
    {
        if (isChanged) return;
        isChanged = true;
        ChangeTypeImmediately();
    }

    public void ChangeTypeImmediately()
    {
        Destroy(current);
        Destroy(onBlock);
        if (type == 3)
        {
            current = Instantiate(gameObjects[1], transform.position, Quaternion.identity);
            onBlock = Instantiate(gameObjects[3],
                new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
               // Quaternion.identity);
                Quaternion.Euler(0, Random.Range(0, 360), 0));
            onBlock.GetComponent<MyTree>().Randomize();

            onBlock.transform.SetParent(transform);
        }
        else if (type == 4)
        {
            current = Instantiate(gameObjects[1], transform.position, Quaternion.identity);
            onBlock = Instantiate(gameObjects[4],
                new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                 Quaternion.identity);
                onBlock.GetComponent<MyRock>().Randomize();

            onBlock.transform.SetParent(transform);
        }else
        {
            current = Instantiate(gameObjects[type], transform.position, Quaternion.identity);
            if (type == 2)
            {
                current.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }

        current.transform.SetParent(transform);
    }
}