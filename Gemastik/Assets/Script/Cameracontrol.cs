using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class Cameracontrol : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cam;

    public float moveSpeed = 5f;
    public float zoomSpeed = 10f; // The speed of zooming
    public float minZoom = 2f;   // The minimum zoom limit
    public float maxZoom = 10f;   // The maximum zoom limit
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
        if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            float scrollData;
            scrollData = Input.GetAxis("Mouse ScrollWheel");
            cam.m_Lens.OrthographicSize -= scrollData * zoomSpeed;
            cam.m_Lens.OrthographicSize = Mathf.Clamp(cam.m_Lens.OrthographicSize, minZoom, maxZoom);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.R))
            {
                cam.m_Lens.OrthographicSize = 5;
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
