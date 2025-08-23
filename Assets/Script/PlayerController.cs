using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed;
    public Rigidbody rb;
    private float mouseX;
    private float mouseY;
    public Transform cameraTransform;
    public float horizontal;
    public float vertical;
    public float jumpForce;
    public float jumpCooldown;
    private bool grounded;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    async void Update()
    {      
        mouseX = Input.GetAxis("Mouse X")*rotationSpeed*Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y")*rotationSpeed*Time.deltaTime;
        transform.Rotate(0 , mouseX, 0);
        cameraTransform.Rotate(-mouseY ,0, 0);
        vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Vector3 direction=transform.right*horizontal+transform.forward*vertical;
        rb.MovePosition(transform.position + direction);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space)&&grounded==true)
        {
            rb.AddForce(Vector3.up*jumpForce);
            await Awaitable.WaitForSecondsAsync(jumpCooldown);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag== "ground")
        {
            grounded = true;
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            grounded = false;
        }

    }



}
