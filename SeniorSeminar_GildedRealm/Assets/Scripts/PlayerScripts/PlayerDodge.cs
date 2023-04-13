using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{

    public CharacterController2D controller;
    public PlayerHealth pHealth;
    public Collider2D dodgeThrough;
    public Collider2D playerAllColl;
    public Animator animator;

    public float dodgeTime;

    bool coroutineTimer;

    void Start()
    {
        UpdateAnimatorClipTimes();
    }

    void Update()
    {
        doDodge();
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

    public void doDodge()
    {
        if (Input.GetButtonDown("Dodge"))
        {
            animator.Play("Base Layer.Dodge", 0, 0.5f);
            animator.SetBool("isDodging", true);
            animator.SetFloat("RunMultiplier", 0f);
            animator.SetFloat("IdleMultiplier", 0f);
            
            playerAllColl.enabled = false;
            //boxcoll2D.enabled = true;
            this.dodge();
            StartCoroutine(StartTimer());
        }
        else if (!Input.GetButtonDown("Dodge") && !coroutineTimer)
        {
            //boxcoll2D.enabled = false;
            
            animator.SetBool("isDodging", false);
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
        //playerAllColl.enabled = true;
        //Debug.Log("Stopped the timer");
    }

    IEnumerator WaitSomeSeconds()
    {
        //Debug.Log("Coroutine in progress");
        pHealth.takeNoDamage();
        yield return null;
    }
}

