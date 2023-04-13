using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public CharacterController2D controller;
    public PlayerMovement move;
    public Animator animator;

    public int health = 50;
    public int maxHealth = 50;
    int currentHealth = 0;
    
    public int potionHeal = 10;

    int healthCount = 0;
    

    float deathTime;

    void Start()
    {
        UpdateAnimatorClipTimes();
    }

    
    void Update()
    {
        doHeal();
        checkDeath();
        stayAtMaxHealth();
        //doHurt();
    }

    public void UpdateAnimatorClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Player_Death":
                    deathTime = clip.length;
                    //Debug.Log("This is the jump time: " + jumpTime);
                    break;
            }
        }
    }

    public void stayAtMaxHealth()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void checkDeath()
    {
        if (health == 0)
        {
            doDie();
        }else if (health > 0)
        {
            animator.SetBool("isDead", false);
        }
    }

    public void doHurt() //Needs to go into the do damage against player script inside of the enemy.
    {
        if (health < maxHealth)
        {
            animator.SetBool("isHurting", true);
        }
        else if(health >= maxHealth)
        {
            animator.SetBool("isHurting", false);
        }
    }

    public void doHeal()
    {
        if (Input.GetButtonDown("Heal"))
        {
            if (health < maxHealth)
            {
                animator.SetFloat("RunMultiplier", 0f);
                animator.SetFloat("IdleMultiplier", 0f);
                animator.Play("Base Layer.Heal", 0, 0.5f);
                animator.SetBool("isHealing", true);
                health = potionHeal + health;
                healthCount++;
            }
        } else if (!Input.GetButtonDown("Heal") && healthCount >= 1)
        {
            animator.SetBool("isHealing", false);
            animator.SetFloat("IdleMultiplier", 1f);
            animator.SetFloat("RunMultiplier", 1f);
        }
    }

    public void doDie()
    {
        animator.SetFloat("RunMultiplier", 0f);
        animator.SetFloat("IdleMultiplier", 0f);
        animator.SetBool("isDead", true);
        animator.PlayInFixedTime("Base Layer.Death",0, deathTime);
        move.enabled = false;
    }

    public void takeNoDamage()
    {
        currentHealth = health;
        if (health < currentHealth)
        {
            health = currentHealth;
        }
    }

}
