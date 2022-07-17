using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerLadder : MonoBehaviour
{
    public GameObject[] blockLadders;
    public GameObject[] openLadders;
    public GameObject[] trapBlocks;
    int trapBlocksIndex;
    int blockLadderIndex;
    int openLadderIndex;
     void OnCollisionEnter(Collision collision)
    {
        trapBlocks[trapBlocksIndex].SetActive(false);
        blockLadders[blockLadderIndex].SetActive(false);
        openLadders[openLadderIndex].SetActive(true);
    }
}
