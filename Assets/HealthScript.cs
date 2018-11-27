using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.gameObject;
        Debug.Log("collision tag: "+ Player.tag);
        if (Player.tag == "Player")
        {
            var controler = Player.GetComponent<PlayerController>();
            controler.OnHealthPotion();
            Destroy(gameObject);
        }
    }
}
