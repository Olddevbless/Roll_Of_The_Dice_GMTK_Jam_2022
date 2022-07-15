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
    bool canMove;
    // Start is called before the first frame update
    void Start()
    {
       
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
        transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
        if (horizontalInput>0)
        {
            nextPos = Vector3.right;
            currentDir = right;
            canMove = true;
        }
        if (horizontalInput< 0)
        {
            nextPos = Vector3.left;
            currentDir = left;
            canMove = true;
        }
        if (verticalInput>0)
        {
            nextPos = Vector3.forward;
            currentDir = up;
            canMove = true;
        }
        if (verticalInput < 0)
        {
            nextPos = Vector3.back;
            currentDir = down;
            canMove = true;
        }
        if (Vector3.Distance(destination, transform.position) <= 0.0001f)
        {
            transform.localEulerAngles = currentDir;
            if (canMove)
            {
                destination = transform.position + nextPos;
            }
        }
    }   
}
