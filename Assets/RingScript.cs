using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.gameObject;
        Debug.Log("collision tag: " + Player.tag);
        if (Player.tag == "Player")
        {
            GameManager.instance.PlayerWin();
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ring collision");
    }
}
