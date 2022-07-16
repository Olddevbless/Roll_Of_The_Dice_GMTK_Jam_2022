using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 up = Vector3.zero;
    Vector3 right = new Vector3(0, 90, 0);
    Vector3 down = new Vector3(0, 180, 0);
    Vector3 left = new Vector3(0, 270, 0);
    Vector3 currentDir = Vector3.zero;
    Vector3 direction;
    Vector3 nextPos;
    Vector3 destination;
    public float horizontalInput;
    public float verticalInput;
    float speed = 5f;
    float rayLength = 1.3f;
    public bool canMove= false;
    public int cubeValue;
    GameObject playerDie;
    // Start is called before the first frame update
    void Start()
    {
        playerDie = GameObject.Find("PlayerDie");
        currentDir = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Moving();
        
    }
    void Moving()
    {
        canMove = false;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
        {
            nextPos = Vector3.right * cubeValue; 
            currentDir = right;
            canMove = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            nextPos = Vector3.left*cubeValue;
            currentDir = left;
            canMove = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            playerDie.transform.Rotate(90, 0, 0);
            nextPos = Vector3.forward*cubeValue;
            currentDir = up;
            canMove = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            nextPos = Vector3.back*cubeValue;
            currentDir = down;
            canMove = true;
        }
        if (Vector3.Distance(destination, transform.position) <=0.0001f)
        {
            transform.localEulerAngles = currentDir;
            if (canMove== true)
            {
                if (Valid())
                {
                    destination = transform.position + nextPos;
                    direction = nextPos;
                    canMove = false;
                }
            }
        }
        bool Valid()
        {
            Ray myRay = new Ray(transform.position + (new Vector3(0, 0.3f, 0)), transform.forward);
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
    }   
}
