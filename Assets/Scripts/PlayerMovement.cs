using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    CharacterController Controller;
    Animator anim;
    float playerSpeed = 5;
    Transform cam; //contains postion, rotation, scale for x, y, z
    float gravity = 10;
    float yVelocity = 0;
    float jumpValue = 4;
    float sprintValue = 1;

    bool updateEnabled = true;
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(updateEnabled){
        //Getting Player's Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isSprint = Input.GetKey("left shift");
        sprintValue = isSprint? 2 : 1;


        // if(Input.GetMouseButtonDown(0)){
        //    attack
        // }

        //putting Player's movements inside of a Vector containing x and z axis
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        // controlling animation: Magnitude = sqrt(x^2 + y^2 + z^2)
        // if the magnitude = 0 animation will be "idle".
        // if the magnitude = 0.5 animation will be "walk"
        // when isSprint returns true animation will be "run"
        anim.SetFloat("Speed", Mathf.Clamp(moveDirection.magnitude, 0, 0.5f) + (isSprint? 0.5f : 0));

        if(Input.GetKeyDown("space") && Controller.isGrounded){
            anim.SetTrigger("Jump");
            yVelocity = jumpValue;
        }else{
            yVelocity -= gravity * Time.deltaTime;
        }

        if(moveDirection.magnitude > 0.1){
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        moveDirection = cam.TransformDirection(moveDirection);
        
        moveDirection = new Vector3(moveDirection.x * sprintValue, yVelocity, moveDirection.z * sprintValue);
        Controller.Move(moveDirection * Time.deltaTime * playerSpeed);}
    }


    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("LavaGround")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
