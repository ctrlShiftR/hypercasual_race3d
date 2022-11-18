using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float gravity, jumpforce, verticalVelocity;
    private Vector3 move;
    private CharacterController characterController;
    public float speed = 20;
    private bool wallSlide=false;
    private Animator animator;
    private bool turn=false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        move = Vector3.zero;
        move = transform.forward;
        
        if (characterController.isGrounded)
        {
           
            wallSlide = false;
            verticalVelocity = 0;
            if (Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Space))
            {
                
                Jump();
            }
            if (turn)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, 
                    transform.eulerAngles.z);
                turn = false;
            }

        }
        if (!wallSlide)
        {
            gravity = 30;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            
            verticalVelocity -= gravity * 0.5f * Time.deltaTime;
        }
        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        characterController.Move(move*Time.deltaTime);
        animator.SetBool("grounded",characterController.isGrounded);
        animator.SetBool("wallslide",wallSlide);
    }
    void Jump()
    {
        verticalVelocity = jumpforce;
        animator.SetTrigger("Jump");
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!characterController.isGrounded)
        {
            if (hit.collider.tag == "wall"||hit.collider.tag=="Slide")
            {
                wallSlide = true;
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                    wallSlide = false;
                }
            }
        }
        else
        {
            if(hit.collider.tag == "floor" && turn == false && -hit.collider.transform.up != transform.forward)
            {
                
                turn = true;
            }
        }
       
    }
}
