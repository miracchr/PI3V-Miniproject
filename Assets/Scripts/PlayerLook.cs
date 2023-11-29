using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //To make sure the player cannot look all the way down and inside the body 
    public float minimumViewDistance = 25f;

    // Mouse sensitivity for camera movement
    public float mouseSensitivity = 100f;

    //To make the body of the player rotate with the camera when looking around
    public Transform playerBody;
    private float rotationX = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //Locking the curser on the screen so it won't move around when the player moves
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        
        //Making sure that the value of the rotationX cannot be lower than -90 and higher than the minimumViewDistance
        rotationX = Mathf.Clamp(rotationX, -90f, minimumViewDistance);
        
        //Since transform.localRotation is a Quaternion it is necessary to use Quaternion.Euler to rotate the object 
        transform.localRotation = Quaternion.Euler(rotationX,0f,0f);
        
        //Ensures that the Player rotates with the camera
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
