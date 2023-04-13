using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerHealth pHealth;
    public Animator animator;
    public Collider2D dodgeThrough;
    public Collider2D playerAllColl;

    public float slideTime;

    bool coroutineTimer;

    void Start()
    {
        UpdateAnimatorClipTimes();
    }

    
    void Update()
    {
        doSlide();
    }

    public void UpdateAnimatorClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Player_Slide":
                    slideTime = clip.length;
                    //Debug.Log("This is the jump time: " + jumpTime);
                    break;
            }
        }
    }

    public void slide()
    {
        if(controller.m_Grounded && controller.m_FacingRight)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(3000f, 0f));
        }
        else if (controller.m_Grounded && !controller.m_FacingRight)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(-3000f, 0f));
        }
    }

    public void doSlide()
    {
        if (Input.GetButtonDown("Slide"))
        {
            animator.Play("Base Layer.Slide", 0, 0.5f);
            animator.SetBool("isSliding", true);
            animator.SetFloat("RunMultiplier", 0f);
            animator.SetFloat("IdleMultiplier", 0f);

            playerAllColl.enabled = false;
            this.slide();
            StartCoroutine(StartTimer());
        }
        else if (!Input.GetButtonDown("Slide") && !coroutineTimer)
        {
            animator.SetBool("isSliding", false);
            animator.SetFloat("RunMultiplier", 1f);
            animator.SetFloat("IdleMultiplier", 1f);
            //playerAllColl.enabled = true;
        }
    }

    IEnumerator StartTimer()
    {
        coroutineTimer = true;
        //Debug.Log("Started the timer");
        dodgeThrough.enabled = true;
        StartCoroutine(WaitSomeSeconds());
        yield return new WaitForSeconds(slideTime);
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
