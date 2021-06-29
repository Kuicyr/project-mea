using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int size;
    public GameObject node;
    public GameObject goNodes;

    private int[,] world;
    private GameObject[,] nodes;

    private void Start()
    {
        Generate();
        Preview();
    }

    private void Generate()
    {
        world = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                world[i, j] = 1;
            }
        }
    }

    private void Preview()
    {
        nodes = new GameObject[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                nodes[i, j] = (GameObject) Instantiate(node, new Vector3(i, 0, j), Quaternion.identity);
                nodes[i, j].transform.SetParent(goNodes.transform);
            }
        }
    }
}