using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerScript : MonoBehaviour {

    private Vector3 _startPosition;
    private EnemyControllerScript _controller;
    private Transform Player;
    private Rigidbody2D _body;
    private Animator _anim;

    // Use this for initialization
    void Start () {
        Debug.Log("Chasing script started");

        _startPosition = transform.position;
        _controller = GetComponent<EnemyControllerScript>();
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        Player = PlayerManager.instance.Player.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (_controller.State != EnemyState.CHASING)
        {
            return;
        }

        var side = Mathf.Sign(Player.position.x - transform.position.x);

        _controller.DeltaX = side * _controller.Speed * Time.deltaTime;
        Debug.Log("Chasing deltaX: "+ _controller.DeltaX);
        HandleMovement(_controller.DeltaX);
    }

    void HandleMovement(float deltaX)
    {
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
        _anim.SetFloat("speed", Mathf.Abs(deltaX));        
    }
}
