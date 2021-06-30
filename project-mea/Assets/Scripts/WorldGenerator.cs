using System.Collections;
using System.Collections.Generic;
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
                world[i, j] = 0;
            }
        }

        GenerateLake(0.12f);
        
    }

    private void GenerateLake(float lakePercent)
    {
        List<Vector2> queue = new List<Vector2>();
        queue.Add(new Vector2(Random.Range(0, size), Random.Range(0, size)));
        var counter = 0;
        while (queue.Count > 0)
        {
            var position = Random.Range(0, queue.Count);
            
            var x = (int) queue[position].x;
            var y = (int) queue[position].y;
            if (world[x, y] == 0)
            {
                world[x, y] = 1;
                counter++;
                if (x != 0 && y != 0 && x != size - 1 && y != size - 1)
                {
                    queue.Add(new Vector2(x + 1, y));
                    queue.Add(new Vector2(x - 1, y));
                    queue.Add(new Vector2(x, y + 1));
                    queue.Add(new Vector2(x, y - 1));
                }
            }

            queue.RemoveAt(position);
            if (counter >= size * size * lakePercent)
            {
                queue.Clear();
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
                nodes[i, j].GetComponent<Node>().type = world[i, j];
                nodes[i, j].GetComponent<Node>().ShowType();
            }
        }
    }
}