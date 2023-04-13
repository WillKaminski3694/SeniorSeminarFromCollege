using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoDamage : MonoBehaviour
{
    public PlayerDamage damage;
    public EnemyHealth eHealth;

    public int pDamage = 0;
    void Start()
    {
        pDamage = 0;
    }

    void Update()
    {
        pDamage = damage.damage;
        //eHealth = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //eHealth = collision.gameObject.GetComponent<EnemyHealth>();
            //eHealth.TakeDamage(pDamage);

            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(pDamage);
        }
    }
}
