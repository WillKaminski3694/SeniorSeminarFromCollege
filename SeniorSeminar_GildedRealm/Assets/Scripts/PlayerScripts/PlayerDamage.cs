using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public CharacterController2D controller;
    public DetectGround detect;
    public PlayerMovement move;
    public Collider2D coll2D;
    public Collider2D secondColl2D;
    public Animator animator;

    public GameObject player;

    public bool coroutineTimer;

    public int numOfInput = 0;
    int maxInput = 2;

    bool attackTwo;

    public float attackTime;
    public float attackTimeTwo;

    public int damage;

    void Start()
    {
        coll2D.enabled = false;
        secondColl2D.enabled = false;
        UpdateAnimatorClipTimes();
        damage = 5;
    }

    
    void Update()
    {
        controller.m_Grounded = detect.move;
        doAttack();
    }

    public void UpdateAnimatorClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Player_Attack":
                    attackTime = clip.length;
                    //Debug.Log("This is the jump time: " + jumpTime);
                    break;
                case "Player_Attack2":
                    attackTimeTwo = clip.length;
                    //Debug.Log("This is the jump time: " + jumpTime);
                    break;
            }
        }
    }

    public void attack()
    {
        if (controller.m_Grounded && controller.m_FacingRight)
        {
            controller.m_Rigidbody2D.AddForce(new Vector2(50f, 0f));
        }
        else if (controller.m_Grounded && !controller.m_FacingRight)
        {
           controller.m_Rigidbody2D.AddForce(new Vector2(-50f, 0f));
        }
    }

    public void doAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (numOfInput == 0 && controller.m_Grounded)
            {
                damage = 5;
                animator.SetFloat("RunMultiplier", 0f);
                animator.SetBool("isAttacking", true);
                coll2D.enabled = true;
                this.attack();
                attackTwo = true;
                move.runSpeed = 0;
                StartCoroutine(StartTimer());
                numOfInput++;
            }
        }

        if (Input.GetButtonDown("Fire2") && numOfInput == 1 && controller.m_Grounded && attackTwo)
        {
            damage = 10;
            animator.SetBool("isAttacking", false);
            animator.SetFloat("RunMultiplier", 0f);
            animator.SetBool("isAttacking2", true);
            secondColl2D.enabled = true;
            move.runSpeed = 0;
            this.attack();
            StopCoroutine(TimerTwo());
                
            numOfInput++;
        }

        if (numOfInput >= 0 && numOfInput < 2 && controller.m_Grounded && attackTwo && !coroutineTimer)
        {
            
            attackTwo = false;
            numOfInput = 0;

            animator.SetBool("isAttacking", false);
            animator.SetFloat("RunMultiplier", 1f);
        }
        else if (numOfInput >= maxInput && controller.m_Grounded && !coroutineTimer)
        {
            animator.SetBool("isAttacking2", false);
            attackTwo = false;
            numOfInput = 0;

            animator.SetBool("isAttacking", false);
            
            animator.SetFloat("RunMultiplier", 1f);
            StartCoroutine(TimerTwo());
            StopCoroutine(TimerTwo());
        }
    }
    IEnumerator StartTimer()
    {
        coroutineTimer = true;
        StartCoroutine(WaitSomeSeconds());
        move.runSpeed = 0;
        yield return new WaitForSeconds(attackTime + 0.5000002f);
        StopCoroutine(WaitSomeSeconds());
        
        coll2D.enabled = false;
        move.runSpeed = 50;
        coroutineTimer = false;
    }

    IEnumerator TimerTwo()
    {
        coroutineTimer = true;
        StartCoroutine(WaitSomeSeconds());
        move.runSpeed = 0;
        animator.SetBool("isAttacking2", false);
        yield return new WaitForSeconds(attackTimeTwo + 0.3500001f);
        StopCoroutine(WaitSomeSeconds());
        
        secondColl2D.enabled = false;
        move.runSpeed = 50;
        coroutineTimer = false;
    }

    IEnumerator WaitSomeSeconds()
    {
        yield return null;
    }

}
