using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
///  Created by H. Lloyd 
/// For project: AT03 - Indie Game
/// References:
///  - In class material
///  - https://www.youtube.com/watch?v=Tz-2Z0vLLt8
///  - https://www.youtube.com/watch?v=o1bj-49uQ74&t=15s 
/// </summary>

public class Player : MonoBehaviour
{
    private PlayerControls _playerControls;

    public Notes noteScript;

    [SerializeField]
    float walkSpeed = 10f,  //speed at which player will walk
        runSpeed = 20f, //speed at which player will run
        jumpForce = 10f; //how high player will jump

    [Range(0f, 100f)] public float stamina;
    private float stamNormal;
    public Image staminaBar;

    private bool isRunning = false;
    private bool isGround = true;


    Vector2 moveInput; //new input system uses vector 2s
    Rigidbody rBody;

    private void OnEnable()
    {
        _playerControls = new PlayerControls();
        _playerControls.Player.Enable();
    }

    private void Start()
    {
        rBody = GetComponent<Rigidbody>(); //makes sure we got the rigid body

        stamNormal = stamina / 100f;
        staminaBar.fillAmount = stamNormal;
    }

    private void Update()
    {
        Movin();
        stamNormal = stamina / 100f;
        staminaBar.fillAmount = stamNormal;
    }

    private void Movin()
    {
        float speed = walkSpeed;
        if(isRunning)
        {
            if(stamina > 0f)
            {
                speed = runSpeed;
                stamina = stamina - Time.deltaTime;
            }
            else
            {
                isRunning = false;
            }
        }
        else if(stamina < 100f)
        {
            stamina = stamina + Time.deltaTime;
        }
        Vector3 playerVelocity = new Vector3(moveInput.x * speed, rBody.velocity.y, moveInput.y * speed); //tells the player what direction to go
        rBody.velocity = transform.TransformDirection(playerVelocity); // tells player to go
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); //Sets the movement to the keys hit
    }

    public void OnSprint()
    {
       if(Input.anyKeyDown && !isRunning)
        {
            Debug.Log("Running");
            isRunning = true;
        }
        else if(!Input.anyKeyDown && isRunning)
        {
            Debug.Log("Not Running");
            isRunning = false;
        }
    }

    public void OnJump()
    {
        if(isGround)
        {
            isGround = false;
            rBody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    public void OnInteract()
    {
        Debug.Log("interacting");
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out hit, 10))
        {
            Debug.Log("Raycast hit smth");
            if (hit.collider.gameObject.tag == "Note")
            {
                Debug.Log("hit note");
                noteScript.notesCollected++;
                GameObject gameObject1 = hit.collider.gameObject;
                gameObject1.SetActive(false);
            }
            else if(hit.collider.gameObject.tag == "Exit")
            {
                Debug.Log("door");
            }
        }
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }
}
