using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerLadder : MonoBehaviour
{
    public GameObject blockLadder;
    public GameObject openLadder;
     void OnCollisionEnter(Collision collision)
    {
        blockLadder.SetActive(false);
        openLadder.SetActive(true);
    }
}
