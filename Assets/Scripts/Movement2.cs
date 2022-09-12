using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Movement2 : MonoBehaviour
{
    Vector3 nextPos;
    Vector3 destination;
    float rayLength = 1f;
    public GameObject[] faces;
    int faceValue = 1;
    private Vector3 RRight = new Vector3(0f,0f, -90f);
    private Vector3 RLeft = new Vector3(0f,0f, 90f);
    private Vector3 RForward = new Vector3(90f,0f, 0f);
    private Vector3 RBack = new Vector3(-90f,0f, 0f);
    private bool canMove = true;
    public bool keyHeld = false;
    public AudioSource playerAudio;
    public AudioClip keyAudioClip;
    public AudioClip hopAudioClip;
    public AudioClip successAudioClip;
    public GameObject gameManager;

    private void Start()
    {
        playerAudio.PlayOneShot(successAudioClip);
        gameManager = GameObject.FindWithTag("GameManager");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        if(Input.GetKeyDown(KeyCode.W) && canMove){
            canMove = false;
            Move(Vector3.forward);
        }else if(Input.GetKeyDown(KeyCode.A) && canMove){
            canMove = false;
            Move(Vector3.left);
        }else if(Input.GetKeyDown(KeyCode.D) && canMove){
            canMove = false;
            Move(Vector3.right);
        }else if(Input.GetKeyDown(KeyCode.S) && canMove){
            canMove = false;
            Move(Vector3.back);
        }
        else if(Input.GetKeyDown(KeyCode.M)){
            SceneManager.LoadScene("Menu");
        }
        else if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        LevelFail();
        
    }

    void Move(Vector3 dir){
        gameManager.GetComponent<GameManager>().MovesMade();
        nextPos = dir;
        faceValue = GetFaceValue(faces);
            destination = transform.position + nextPos;
                if(dir == Vector3.forward){
                    StartCoroutine(MoveObject(destination, .5f, RForward, faceValue, dir));
                }else if(dir == Vector3.back){
                    StartCoroutine(MoveObject(destination, .5f, RBack, faceValue, dir));
                }else if(dir == Vector3.left){
                    StartCoroutine(MoveObject(destination, .5f, RLeft, faceValue, dir));
                }else if(dir == Vector3.right){
                    StartCoroutine(MoveObject(destination, .5f, RRight, faceValue, dir));
                }
    }  

    IEnumerator MoveObject(Vector3 destination, float overTime, Vector3 degree, int spaces, Vector3 dir)
    {
        for(int x = 0; x < spaces; x++){
            if(Valid(dir)&&OnGround()){
                playerAudio.Play();
                var startRotation = transform.rotation;
                Vector3 startpos = transform.position;
                var endRotation = Quaternion.Euler(degree) * transform.rotation;
                destination = transform.position + dir;
                float startTime = Time.time;
                while(Time.time < startTime + overTime)
                {
                    transform.position = Vector3.Lerp(startpos, destination, (Time.time - startTime)/overTime);
                    transform.rotation = Quaternion.Slerp(startRotation, endRotation, (Time.time - startTime)/overTime);
                    yield return null;
                }
                yield return new WaitForSeconds(.1f);
            }
        }
        Localize();
        OnGround();
        canMove = true;
    }

    public bool Valid(Vector3 dir)
        {
            Ray myRay = new Ray(transform.position + (new Vector3(0, 0.2f, 0)), dir);
            RaycastHit hit;
            Debug.DrawRay(myRay.origin, myRay.direction, Color.red);

            if (Physics.Raycast(myRay,out hit, rayLength))
            {
                if (hit.collider.tag == "Wall")
                {
                    Debug.Log("ray hit wall");
                    return false;
                }
            }
            return true;
        }

        public bool OnGround()
        {
            Ray myRay = new Ray(transform.position + (new Vector3(0, 0.2f, 0)), Vector3.down);
            RaycastHit hit;
            Debug.DrawRay(myRay.origin, myRay.direction, Color.red);
            if (Physics.Raycast(myRay,out hit, 5f))
            {
                if (hit.collider.tag == "Water")
                {
                    Debug.Log("Player over water");
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    return false;
                }
            }
            return true;
        }

    public int GetFaceValue( GameObject[] faces){
        int[] facesVal = new int[] {1, 2, 3, 4, 5, 6};
        int xVal = 0;
        GameObject highestFace = faces[0];
        for(int x = 0; x < 6; x++){
            if(faces[x].transform.position.y > highestFace.transform.position.y){
                xVal = x;
                highestFace = faces[x];
            }
        }
        return facesVal[xVal];
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void LevelFail()
    {
        if (transform.position.y < -5)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            playerAudio.PlayOneShot(keyAudioClip);
            Debug.Log("i have a key");
            keyHeld = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Door") && keyHeld == true)
        {
            if(SceneManager.GetActiveScene().buildIndex == 5){
                SceneManager.LoadScene("Menu");
            }else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.CompareTag("Door")&& keyHeld != true)
        {

            Debug.Log("You must find the key!");
        }
        
    }
    private void OnCollisionEnter(Collision other) {
        if (other.transform.gameObject.CompareTag("Water"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Localize()
    {
        float localx = (float)Math.Round(transform.position.x * 2, MidpointRounding.AwayFromZero) / 2;
        float localz = (float)Math.Round(transform.position.z * 2, MidpointRounding.AwayFromZero) / 2;
        transform.position = new Vector3(localx, 0.45f, localz);
        var vec = transform.eulerAngles;
        vec.x = (Mathf.Round(vec.x / 90)) * 90;
        vec.y = (Mathf.Round(vec.y / 90)) * 90;
        vec.z = (Mathf.Round(vec.z / 90)) * 90;
        transform.eulerAngles = vec;
    }
}
