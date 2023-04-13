using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingYurtTent : MonoBehaviour
{

    public Animator animator;

    public PlayerMovement move;
    public PlayerDamage damage;
    public PlayerSlideandDodge athletics;

    public Collider2D collide;

    public float constructTime;

    void Start()
    {
        UpdateAnimatorClipTimes();
    }

    public void UpdateAnimatorClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Construct":
                    constructTime = clip.length;
                    break;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            animator.SetBool("hasEntered", true);
            StartCoroutine(StartTimer());
        }
    }

    IEnumerator StartTimer()
    {
        StartCoroutine(WaitSomeSeconds());
        //move.enabled = false;
        move.runSpeed = 0;
        damage.enabled = false;
        athletics.enabled = false;
        yield return new WaitForSeconds(constructTime);
        StopCoroutine(WaitSomeSeconds());
        //move.enabled = true;
        move.runSpeed = 50;
        damage.enabled = true;
        athletics.enabled = true;
        collide.enabled = false;
    }

    IEnumerator WaitSomeSeconds()
    {
        yield return null;
    }
}
