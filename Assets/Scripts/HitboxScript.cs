using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public float BaseDamage = 50f;
    private Collider2D _coll;

    // Use this for initialization
    void Start ()
    {
        _coll = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject;
        if (target.tag != "Enemy")
        {
            gameObject.SetActive(false);
            return;
        }

        var enemyScript = target.GetComponent<EnemyCombatScript>();
        enemyScript.ReceiveAttack(BaseDamage);

        gameObject.SetActive(false);
    }
}
