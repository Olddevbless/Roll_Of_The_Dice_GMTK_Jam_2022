using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleToNext : MonoBehaviour
{
    
    //public GameObject target;
public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("moved to level" + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }
}
