using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideandDodge : MonoBehaviour
{

    public CharacterController2D controller;
    public DetectGround detect;
    public PlayerMovement move;
    public PlayerHealth pHealth;
    public Animator animator;
    public Collider2D dodgeThrough;
    public Collider2D playerAllColl;
    
    public float dodgeTime;
    public float slideTime;

    bool coroutineTimer;

    void Start()
    {
        UpdateAnimatorClipTimes();
    }

    void Update()
    {
        DoDodgeandSlide();
        controller.m_Grounded = detect.move;
    }

    public void UpdateAnimatorClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Player_Dodge":
                    dodgeTime = clip.length;
                    break;
                case "Player_Slide":
                    slideTime = clip.length;
                    //Debug.Log("This is the jump time: " + jumpTime);
                    break;
            }
        }
    }

    public void dodge()
    {
        if (controller.m_Grounded && controller.m_FacingRight)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(3750f, 0f));
        }
        else if (controller.m_Grounded && !controller.m_FacingRight)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(-3750f, 0f));
        }
    }

    public void slide()
    {
        if (controller.m_Grounded && controller.m_FacingRight)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(3000f, 0f));
        }
        else if (controller.m_Grounded && !controller.m_FacingRight)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(-3000f, 0f));
        }
    }

    public void DoDodgeandSlide()
    {
        if (Input.GetButtonDown("Dodge"))
        {
            animator.Play("Base Layer.Dodge", 0, 0.5f);
            animator.SetBool("isDodging", true);
            animator.SetFloat("RunMultiplier", 0f);
            animator.SetFloat("IdleMultiplier", 0f);

            playerAllColl.enabled = false;
            this.dodge();
            StartCoroutine(StartTimer());
        }
        else if (Input.GetButtonDown("Slide"))
        {
            animator.Play("Base Layer.Slide", 0, 0.5f);
            animator.SetBool("isSliding", true);
            animator.SetFloat("RunMultiplier", 0f);
            animator.SetFloat("IdleMultiplier", 0f);

            playerAllColl.enabled = false;
            this.slide();
            StartCoroutine(StartTimerTwo());
        }
        else if (!Input.GetButtonDown("Dodge") && !Input.GetButtonDown("Slide") && !coroutineTimer && !move.jump)
        {
            //boxcoll2D.enabled = false;

            animator.SetBool("isDodging", false);
            animator.SetBool("isSliding", false);
            animator.SetFloat("RunMultiplier", 1f);
            animator.SetFloat("IdleMultiplier", 1f);
            playerAllColl.enabled = true;
        }
    }

    IEnumerator StartTimer()
    {
        coroutineTimer = true;
        //Debug.Log("Started the timer");
        dodgeThrough.enabled = true;
        StartCoroutine(WaitSomeSeconds());
        yield return new WaitForSeconds(dodgeTime - 0.0555f);
        //boxcoll2D.enabled = false;
        StopCoroutine(WaitSomeSeconds());
        dodgeThrough.enabled = false;
        coroutineTimer = false;
        //Debug.Log("Stopped the timer");
    }

    IEnumerator StartTimerTwo()
    {
        coroutineTimer = true;
        //Debug.Log("Started the timer");
        dodgeThrough.enabled = true;
        StartCoroutine(WaitSomeSeconds());
        yield return new WaitForSeconds(slideTime - 0.5f);
        dodgeThrough.enabled = false;
        StopCoroutine(WaitSomeSeconds());
        coroutineTimer = false;
        //Debug.Log("Stopped the timer");
    }

    IEnumerator WaitSomeSeconds()
    {
        //Debug.Log("Coroutine in progress");
        pHealth.takeNoDamage();
        yield return null;
    }
}
