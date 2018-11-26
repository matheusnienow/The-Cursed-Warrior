using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementScript : MonoBehaviour
{
    private Rigidbody2D _body;
    private Animator _anim;
    private Collider2D _coll;
    private EnemyControllerScript _controller;
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
        _controller = GetComponent<EnemyControllerScript>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (_controller.State != EnemyState.PATROL)
        {
            return;
        }

        deltaTime += Time.deltaTime;
        _controller.DeltaX = side * _controller.Speed * Time.deltaTime;
        HandleMovement(_controller.DeltaX);
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
