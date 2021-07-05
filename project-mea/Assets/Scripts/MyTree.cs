using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTree : MonoBehaviour
{
    public float yPosition = 1.06f;
    public Vector3 minValues;
    public Vector3 maxValues;
    public Transform customTree;

    public void Randomize()
    {
        var scale = Random.Range(minValues.x, maxValues.x);
        var newScale = new Vector3(scale, Random.Range(minValues.y, maxValues.y), scale);
        customTree.transform.localScale = newScale;
        customTree.transform.position = new Vector3(transform.position.x, transform.position.y + newScale.y * yPosition,
            transform.position.z);
    }
}