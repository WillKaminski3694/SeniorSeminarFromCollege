using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public DetectGround detect;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    public bool jump = false;

    public bool grounded = true;
    public bool coroutineTimer;

    public int spacePressed = 0;

    public float jumpTime;
    public float jumpTwo;

    GameObject player;

    void Start()
    {
        controller.m_Grounded = true;

        player = this.gameObject;

        grounded = true;

        UpdateAnimatorClipTimes();

        coroutineTimer = false;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Run", Mathf.Abs(horizontalMove));

        grounded = detect.move;

        DoJump();
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
                    break;
                case "Player_Jump2":
                    jumpTwo = clip.length;
                    break;
            }
        }
    }

    public void DoJump()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            //First jump
            if (!coroutineTimer && grounded && spacePressed == 0 && SceneManager.GetActiveScene().buildIndex != 2)
            {
                jump = true;
                animator.SetFloat("IdleMultiplier", 0f);
                animator.SetFloat("RunMultiplier", 0f);
                animator.SetBool("isJumping", true);
                animator.SetBool("isGrounded", false);
                controller.m_Grounded = grounded;
                controller.Jump(controller.m_Grounded, jump);
                spacePressed++;
                controller.m_Grounded = false;
                StartCoroutine(StartTimer());
            }
            //Double Jump
            else if (jump && spacePressed == 1 && coroutineTimer && !grounded)
            {   
                animator.SetFloat("IdleMultiplier", 0f);
                animator.SetFloat("RunMultiplier", 0f);
                animator.SetBool("isJumping", false);
                animator.SetBool("isJumping2", true);
                animator.SetBool("isGrounded", false);
                
                controller.m_Grounded = true;
                controller.Jump(controller.m_Grounded, jump);
                spacePressed++;
                controller.m_Grounded = false;
                StartCoroutine(TimerTwo());
                StopCoroutine(StartTimer());
            }
        }
    }

    IEnumerator StartTimer()
    {
        coroutineTimer = true;
        controller.m_Grounded = false;
        StartCoroutine(WaitSomeSeconds());
        yield return new WaitForSeconds(jumpTime + 0.65f);
        StopCoroutine(WaitSomeSeconds());
        jump = false;
        animator.SetBool("isJumping", false);
        animator.SetBool("isGrounded", true);
        animator.SetFloat("IdleMultiplier", 1f);
        animator.SetFloat("RunMultiplier", 1f);
        
        spacePressed = 0;
        //controller.m_Grounded = true;
        coroutineTimer = false;
    }

    IEnumerator TimerTwo(){
        coroutineTimer = true;
        controller.m_Grounded = false;
        StartCoroutine(WaitSomeSeconds());
        yield return new WaitForSeconds(jumpTwo + 1f);
        StopCoroutine(WaitSomeSeconds());
        animator.SetBool("isJumping2", false);
        animator.SetBool("isGrounded", true);
        animator.SetFloat("IdleMultiplier", 1f);
        animator.SetFloat("RunMultiplier", 1f);
        jump = false;
        //controller.m_Grounded = true;
        spacePressed = 0;
        coroutineTimer = false;
    }

    IEnumerator WaitSomeSeconds()
    {
        yield return null;
    }
}

