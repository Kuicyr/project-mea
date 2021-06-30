using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int type;
    
    public void ShowType()
    {
        switch (type)
        {
            case 0:
            {
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            }
            case 1:
            {
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            }
        }
    }
}
