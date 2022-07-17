using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMPro.TextMeshProUGUI movesText;
    public int movesMade;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movesText.text = ("Moves : " + movesMade);
    }
    public void MovesMade()
    {
        movesMade++;
    }
}
