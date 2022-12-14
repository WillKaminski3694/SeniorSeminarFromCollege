using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    GameObject someGameObject;
    GameObject player;

    private void Start()
    {
        someGameObject = this.gameObject;
        player = GameObject.FindWithTag("Player");
        player.transform.position = someGameObject.transform.position;
    }
}
