using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Created by H. Lloyd 
/// For project: AT03 - Indie Game
/// References:
///  - In class material
///  - https://www.youtube.com/watch?v=Tz-2Z0vLLt8
/// </summary>

public class MoveCamera : MonoBehaviour
{
    [SerializeField] float minView = 25f, // Lowest Player can look
        maxView = -90f, // highest player can look
        mouseSense; //how much the mouse moves

    [SerializeField] Transform playerBody; //So the body moves with the camera

    private float xRot; //so player starts at facing forward

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //will lock cursor in screen 
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime; //gets the current X position of the mouse
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime; //gets the current Y position of the mouse

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, maxView, minView); //Locks it between the min and max camera rotation

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
