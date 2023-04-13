using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGround2 : MonoBehaviour
{
    public GameObject ground2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            ground2.SetActive(false);
        }
    }
}
