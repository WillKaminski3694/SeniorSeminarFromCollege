using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;

    public List<Transform> points;

    Vector3 enemyScale;
    Vector3 oppositeEnemyScale;

    int listSize;

    int nextIndex = 0;
    int indexChangeValue = 1;
    int indexChangeValue1 = 2;
    int indexChangeValue2 = 3;

    int randNum = 1;
    int randNum2;
    int randNum3;

    int count = 0;

    public float runSpeed;
    public float currentSpeed;
    public float waitTime;
    
    float horizontalMove = 0f;

    bool coroutineTimer2;

    public Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        enemyScale = transform.localScale;
        oppositeEnemyScale = new Vector3(-enemyScale.x, enemyScale.y, enemyScale.z);

        runSpeed = currentSpeed;

        listSize = points.Count;

        coroutineTimer2 = true;
    }

    void Update()
    {
        animator.SetFloat("Run", horizontalMove);

        if (coroutineTimer2)
        {
            StartCoroutine(WaitForIdleTime());
        }
        if (randNum == 0 && count < 1 && !coroutineTimer2)
        {
            StartCoroutine(StartTimer());
        }
        else if(randNum == 1 || randNum3 == 1)
        {
            runSpeed = currentSpeed;
            horizontalMove = runSpeed;
            MakeEnemyMove();
            randNum3 = Random.Range(0, 2);
            if (randNum3 == 0)
            {
                count = 0;
                Debug.Log("First random if");
            }
            else if(randNum3 == 1)
            {
                count = 1;
                Debug.Log("Second random if");
            }
        }
    }

    public void MakeEnemyMove()
    {
        if(points[nextIndex].transform.position.x > transform.position.x)
        {
            transform.localScale = oppositeEnemyScale;
            horizontalMove = runSpeed;
        }
        else
        {
            transform.localScale = enemyScale;
            horizontalMove = runSpeed;
        }

        if (listSize == 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[nextIndex].position, runSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[nextIndex].position) < 1f)
            {
                if (nextIndex == listSize - listSize)
                {
                    nextIndex = indexChangeValue;
                }
                else if (nextIndex == listSize - 1)
                {
                    nextIndex = 0;
                }
            }
        }
        else if(listSize == 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[nextIndex].position, runSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[nextIndex].position) < 1f)
            {
                randNum2 = Random.Range(0, 4);
                if (randNum2 == 0)
                {
                    nextIndex = indexChangeValue;
                }
                else if (randNum2 == 1)
                {
                    nextIndex = indexChangeValue1;
                }
                else if (randNum2 == 2)
                {
                    nextIndex = indexChangeValue2;
                }
                else if (randNum2 == 3)
                {
                    nextIndex = 0;
                }
            }
        }
    }

    IEnumerator StartTimer()
    {
        count = 1;
        runSpeed = 0f;
        horizontalMove = runSpeed;
        StartCoroutine(WaitSomeSeconds());
        yield return new WaitForSeconds(waitTime);
        StopCoroutine(WaitSomeSeconds());
    }

    IEnumerator WaitForIdleTime()
    {
        coroutineTimer2 = false;
        StartCoroutine(WaitSomeSeconds());
        yield return new WaitForSeconds(waitTime);
        StopCoroutine(WaitSomeSeconds());
        randNum = Random.Range(0, 2);
        coroutineTimer2 = true;
    }

    IEnumerator WaitSomeSeconds()
    {
        yield return null;
    }
}
