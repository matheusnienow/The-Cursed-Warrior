using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysMovementScript : MonoBehaviour
{
    public float speed = 40f;
    private Rigidbody2D _body;
    private Animator _anim;
    private Collider2D _coll;
    
    private float side = -1;
    private float deltaTime = 0;
    public float secondsWalking = 3f;

    private bool ShouldMove = true;

    // Use this for initialization
    void Start ()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<CapsuleCollider2D>();
    }
	
	// Update is called once per frame
	void Update()
    {
        deltaTime += Time.deltaTime;
        float deltaX = side * speed * Time.deltaTime;
        HandleMovement(deltaX);
    }

    void HandleMovement(float deltaX)

    {
        if (!ShouldMove)
        {
            return;
        }

        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
        _anim.SetFloat("speed", Mathf.Abs(deltaX));

        if (deltaTime > secondsWalking)
        {
            deltaTime = 0;
            side *= -1;
        }
        HandleSpriteInvertion(deltaX);
    }

    void HandleSpriteInvertion(float deltaX)
    {
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3((Mathf.Sign(deltaX) * 0.8f) * -1f, 0.8f, 0.8f);
        }
    }

    public void StopMoving()
    {
        ShouldMove = false;
    }

    public void StartMoving()
    {
        ShouldMove = true;
    }
}
