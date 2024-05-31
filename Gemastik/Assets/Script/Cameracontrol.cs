using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Cameracontrol : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
    }
    public float moveSpeed = 5f; 

    void Update()
    {
        Vector3 movement = Vector3.zero;

        // Check if the arrow keys or WASD keys are being pressed
        if (Input.GetKey(KeyCode.W))
        {
            movement.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += moveSpeed * Time.deltaTime;
        }

        // Apply the movement to the camera's position
        if (cam != null)
        {
            cam.transform.position += movement;
        }
    }
}
