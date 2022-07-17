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
        for (int x = 0; x < trapBlocks.Length; x++)
        {
            if (x < blockLadders.Length)
            {
                blockLadders[x].SetActive(false);
                openLadders[x].SetActive(true);
            }
            trapBlocks[x].SetActive(false);
        }
    }
}
