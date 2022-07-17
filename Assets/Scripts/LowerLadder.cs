using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerLadder : MonoBehaviour
{
    public GameObject blockLadder;
    public GameObject openLadder;
    
     void onCollisionEnter(Collision collision)
    {
        Debug.Log("i have collided with" + collision.gameObject.name);
        blockLadder.SetActive(false);
        openLadder.SetActive(true);
    }
}
