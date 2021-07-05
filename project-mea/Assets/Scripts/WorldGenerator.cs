using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    public int size;
    public GameObject node;
    public GameObject goNodes;

    private int[,] world;
    private GameObject[,] nodes;
    private List<Vector2> toChange;

    private void Start()
    {
        world = new int[size, size];
        nodes = new GameObject[size, size];
        toChange = new List<Vector2>();
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                world[i, j] = 0;
            }
        }

        Preview();

        GenerateTerrain(0.12f, 2, 0, 0);
        GenerateTerrain(0.03f, 1, 0.2f, 3);
        GenerateTerrain(0.03f, 1, 0.2f, 3);
        GenerateTerrain(0.03f, 1, 0.2f, 3);
        GenerateTerrain(0.03f, 1, 0.2f, 3);
        GenerateTerrain(0.03f, 1, 0.2f, 3);
        // GenerateTerrain(0.02f, 2, 0, 0);
       

        var showTime = 5f / (float) toChange.Count;

        for (var index = 0; index < toChange.Count; index++)
        {
            var vector2 = toChange[index];
            StartCoroutine(Change(vector2, showTime * index));
        }

        // Change();
    }

    private void Change()
    {
        if (toChange.Count > 0)
        {
            var x = (int) toChange[0].x;
            var y = (int) toChange[0].y;
            nodes[x, y].GetComponent<Node>().type = world[x, y];
            nodes[x, y].GetComponent<Node>().ChangeType();
            toChange.RemoveAt(0);

            Invoke("Change", 0.0001f);
        }
    }

    IEnumerator Change(Vector2 node, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var x = (int) node.x;
        var y = (int) node.y;
        nodes[x, y].GetComponent<Node>().type = world[x, y];
        nodes[x, y].GetComponent<Node>().ChangeType();
    }

    private void GenerateTerrain(float blockPercent, int type, float onBlockPercent, int onBlockType)
    {
        var queue = new List<Vector2>();
        var tempToChange = new List<Vector2>();
        var startPosition = new Vector2(Random.Range(1, size - 1), Random.Range(1, size - 1));
        while (world[(int) startPosition.x, (int) startPosition.y] != 0)
        {
            startPosition = new Vector2(Random.Range(1, size - 1), Random.Range(1, size - 1));
        }

        queue.Add(startPosition);
        var counter = 0;
        while (queue.Count > 0)
        {
            var position = Random.Range(0, queue.Count);

            var x = (int) queue[position].x;
            var y = (int) queue[position].y;
            if (world[x, y] == 0)
            {
                world[x, y] = type;
                tempToChange.Add(new Vector2(x, y));
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
            if (counter >= size * size * blockPercent)
            {
                queue.Clear();
            }
        }

        if (onBlockPercent != 0f)
        {
            queue = new List<Vector2>();
            foreach (var node in tempToChange)
            {
                queue.Add(node);
            }

            Shuffle(queue);

            for (var index = 0; index < queue.Count * onBlockPercent; index++)
            {
                var x = (int) queue[index].x;
                var y = (int) queue[index].y;
                world[x, y] = onBlockType;
            }
        }

        foreach (var vector2 in tempToChange)
        {
            toChange.Add(new Vector2(vector2.x, vector2.y));
        }
    }

    public static void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    private void Preview()
    {
        foreach (var o in nodes)
        {
            Destroy(o);
        }

        nodes = new GameObject[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                nodes[i, j] = (GameObject) Instantiate(node, new Vector3(i, 0, j), Quaternion.identity);
                nodes[i, j].transform.SetParent(goNodes.transform);
                nodes[i, j].GetComponent<Node>().type = world[i, j];
                nodes[i, j].GetComponent<Node>().ChangeTypeImmediately();
            }
        }
    }
}