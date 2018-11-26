using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxScript : MonoBehaviour
{
    public float BaseDamage = 20f;
    private Collider2D _coll;

    // Use this for initialization
    void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject;
        if (target.tag != "Player")
        {
            gameObject.SetActive(false);
            return;
        }

        var playerController = target.GetComponent<PlayerController>();
        playerController.ReceiveAttack(BaseDamage);

        gameObject.SetActive(false);
    }
}
