using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public float gravity, jumpforce, verticalVelocity;
    private Vector3 move;
    private CharacterController characterController;
    public float speed = 20;
    private bool wallSlide = false;
    private Animator animator;
    private bool turn = false,Jump=true;
    void Awake()
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
            
            verticalVelocity = 0;
            raycasting();
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
            gravity = 15;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        



        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        characterController.Move(move * Time.deltaTime);
        animator.SetBool("grounded", characterController.isGrounded);
        animator.SetBool("wallslide", wallSlide);

    }
    void raycasting()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 8f)&&hit.collider.tag=="wall")
        {
                jump();
            //Jump = true;
            
            
        }
    }
    void jump()
    {
        verticalVelocity = jumpforce;
        animator.SetTrigger("Jump");
    }
    IEnumerator lateJump(float time)
    {
        
        Jump = false;
        wallSlide = true;
        yield return new WaitForSeconds(time);
        if (!characterController.isGrounded)
        {
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,
                                                transform.eulerAngles.z);
            verticalVelocity = jumpforce;
            animator.SetTrigger("Jump");
        }
        Jump = true;
        wallSlide=false;
    }
   
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       
        
            if (hit.collider.tag == "wall")
            {
                if (Jump)
                {
                    StartCoroutine(lateJump(Random.Range(0.2f, 0.5f)));

                }
                if (verticalVelocity < 0)
                {
                    wallSlide = true;
                }
            
                
            }
            else if (hit.collider.tag == "Slide" &&characterController.isGrounded )
            {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180,
                                                 transform.eulerAngles.z);
        }
            else if (hit.collider.tag == "Slide")
            {
            wallSlide= true;
            }
        
       

    }
}
