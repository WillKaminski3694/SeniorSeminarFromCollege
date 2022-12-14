using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    private AnimationClip clip;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public bool grounded;

    int spacePressed = 0;
    int numOfJumps = 2;
    //int upgradedJump = 3;

    public float jumpTime;
    public float runTime;
    public float idleTime;

    GameObject player;
    float playerY;
    float playerY2;

    private void Awake()
    {
       
    }

    void Start()
    {
        controller.m_Grounded = true;

        player = this.gameObject;

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("Error: Did not find animator!");
        }
        else
        {
            //Debug.Log("Got the animator!");
        }
        UpdateAnimatorClipTimes();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Run", Mathf.Abs(horizontalMove));

        grounded = controller.m_Grounded;

        doJump();
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);
    }

    public void UpdateAnimatorClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Player_Jump":
                    jumpTime = clip.length;
                    //Debug.Log("This is the jump time: " + jumpTime);
                    break;
                case "Player_Idle":
                    idleTime = clip.length;
                    //Debug.Log("This is the idle time: " + idleTime);
                    break;
                case "Player_Run":
                    runTime = clip.length;
                    //Debug.Log("This is the run time: " + runTime);
                    break;
            }
        }
    }

    public void doJump()
    {
        if (Input.GetButtonDown("Jump"))
        {

            if (controller.m_Grounded && spacePressed == 0)
            {
                animator.SetFloat("IdleMultiplier", 0f);
                animator.SetFloat("RunMultiplier", 0f);
                animator.SetBool("isJumping", true);
                
                jump = true;
                spacePressed++;
                controller.m_Grounded = true;
                controller.Jump(controller.m_Grounded, jump);
                playerY = player.transform.position.y;
                //Debug.Log("this is the players y position: " + playerY);
            }
            else if (controller.m_Grounded && spacePressed == 1 && jump)
            {
                animator.SetFloat("JumpMultiplier", 0.5f);
                animator.SetFloat("IdleMultiplier", 0f);
                animator.SetFloat("RunMultiplier", 0f);
                animator.Play("Base Layer.Jump", 0, 0.25f);
                animator.SetBool("isJumping", true);

                spacePressed++;
                controller.m_Grounded = true;
                controller.Jump(controller.m_Grounded, jump);
            }
        }

        if (spacePressed >= numOfJumps && controller.m_Grounded && playerY > playerY2)
        {
            //Debug.Log("This is to test to see if this is working or not.");
            jump = false;
            controller.m_Grounded = false;
            spacePressed = 0;

            animator.SetBool("isJumping", false);
            animator.SetFloat("JumpMultiplier", 1f);
            animator.SetFloat("IdleMultiplier", 1f);
            animator.SetFloat("RunMultiplier", 1f);
        }
        else if (spacePressed >= 1 &&  jump && playerY > playerY2)
        {
            //Debug.Log("This is to test to see if this is working or not 2.");
            jump = false;
            controller.m_Grounded = false;
            spacePressed = 0;

            animator.SetBool("isJumping", false);
            animator.SetFloat("JumpMultiplier", 1f);
            animator.SetFloat("IdleMultiplier", 1f);
            animator.SetFloat("RunMultiplier", 1f);
        }
        playerY2 = player.transform.position.y;
        //Debug.Log("this is the update players y position: " + playerY2);
    }
}
