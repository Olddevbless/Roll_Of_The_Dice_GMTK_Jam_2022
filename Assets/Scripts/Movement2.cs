using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    Vector3 nextPos;
    Vector3 destination;
    float speed = 5f;
    float rayLength = 1.3f;
    GameObject playerDie;
    int faceValue = 1;
    public GameObject[] faces;
    void Start()
    {
        playerDie = GameObject.Find("PlayerDie");
        nextPos = Vector3.forward;
        destination = transform.position;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            Move(Vector3.forward);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Move(Vector3.left);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            Move(Vector3.right);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            Move(Vector3.back);
        }
    }

    void Move(Vector3 dir){
        Debug.Log("moving " + GetFaceValue(faces) + " Spaces");
        for(int x = 0; x < GetFaceValue(faces); x++){
            if(Valid(dir)){
            destination = transform.position + nextPos;
            if(dir == Vector3.forward){
                while(transform.position.z < destination.z){
                transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
                transform.Rotate(Vector3.right, 446 * Time.deltaTime, Space.World);
                }
            }else if(dir == Vector3.back){
                while(transform.position.z > destination.z){
                transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
                transform.Rotate(Vector3.left, 446 * Time.deltaTime, Space.World);
                }
            }else if(dir == Vector3.left){
                Debug.Log("Destination = "+destination + " Transform = "+transform.position);
                while(transform.position.x > destination.x){
                transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
                transform.Rotate(Vector3.forward, 446 * Time.deltaTime, Space.World);
                }
            }
            else if(dir == Vector3.right){
                Debug.Log("Destination = "+destination + " Transform = "+transform.position);
                while(transform.position.x < destination.x){
                    transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
                    transform.Rotate(Vector3.back, 446 * Time.deltaTime, Space.World);
                }
            }
            }
        }
    }  
    public bool Valid(Vector3 dir)
        {
            Ray myRay = new Ray(transform.position + (new Vector3(0, 0.3f, 0)), dir);
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

    public int GetFaceValue(GameObject[] faces){
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
}
