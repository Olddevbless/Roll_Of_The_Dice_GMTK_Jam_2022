using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetLevel1(){
        SceneManager.LoadScene("Level 1");
    }
    public void SetLevel2(){
        SceneManager.LoadScene("Level 2");
    }
    public void SetLevel3(){
        SceneManager.LoadScene("Level 3");
    }
    public void SetLevel4(){
        SceneManager.LoadScene("Level 4");
    }
    public void SetLevel5(){
        SceneManager.LoadScene("Level 5");
    }
    public void SetLevelExit(){
        Application.Quit();
    }
}
