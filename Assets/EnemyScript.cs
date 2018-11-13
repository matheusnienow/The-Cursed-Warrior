using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 250f;
    private Rigidbody2D _body;
    private Animator _anim;
    private Collider2D _coll;

    private float startTime = 0f;
    private float side = 1;

    // Use this for initialization
    void Start ()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<CapsuleCollider2D>();
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float deltaX = side * speed * Time.deltaTime;

        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
        _anim.SetFloat("speed", Mathf.Abs(deltaX));

        var deltaTime = Time.time - startTime;
        if (deltaTime > 2000)
        {
            side *= -1;
        }
    }
}
