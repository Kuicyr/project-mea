using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int size;
    public GameObject node;
    public GameObject goNodes;
    public float timeToGenerate;

    private int[,] world;
    private GameObject[,] nodes;
    private List<Vector2> toChange;
    private float timePerInvoke;
    
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

        GenerateTerrain(0.12f, 2);
        GenerateTerrain(0.03f, 1);
        GenerateTerrain(0.03f, 1);
        GenerateTerrain(0.03f, 1);
        GenerateTerrain(0.03f, 1);
        GenerateTerrain(0.03f, 1);
        GenerateTerrain(0.03f, 1);
        GenerateTerrain(0.03f, 2);
        timePerInvoke = timeToGenerate / toChange.Count;
       // Debug.Log(timePerInvoke);
       // Invoke("Change",0.0015f);
        StartCoroutine(DelayAction(timePerInvoke));
    }

    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
        if (toChange.Count > 0)
        {
            var x = (int)toChange[0].x;
            var y = (int)toChange[0].y;
            nodes[x, y].GetComponent<Node>().type = world[x, y];
            nodes[x, y].GetComponent<Node>().ShowType();
            toChange.RemoveAt(0);
            StartCoroutine(DelayAction(timePerInvoke));
        }
    }

    private void Change()
    {
        if (toChange.Count > 0)
        {
            var x = (int)toChange[0].x;
            var y = (int)toChange[0].y;
            nodes[x, y].GetComponent<Node>().type = world[x, y];
            nodes[x, y].GetComponent<Node>().ShowType();
            toChange.RemoveAt(0);
            Invoke("Change",0.0015f);
        }
    }

    private void GenerateTerrain(float lakePercent, int type)
    {
        var queue = new List<Vector2>();
        var startPosition = new Vector2(Random.Range(1, size-1), Random.Range(1, size-1));
        while (world[(int) startPosition.x, (int) startPosition.y] != 0)
        {
            startPosition = new Vector2(Random.Range(1, size-1), Random.Range(1, size-1));
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
                toChange.Add(new Vector2(x,y));
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
                nodes[i, j].GetComponent<Node>().ShowType();
            }
        }
    }
}