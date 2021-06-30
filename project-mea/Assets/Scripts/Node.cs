using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int type;
    public GameObject[] gameObjects;
    
    public void ShowType()
    {
        foreach (var o in gameObjects)
        {
            o.SetActive(false);
        }
        
        gameObjects[type].SetActive(true);
    }
}
