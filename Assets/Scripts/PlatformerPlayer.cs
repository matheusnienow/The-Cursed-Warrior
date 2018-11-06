using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour {
    public float speed = 250f;
    public float jumpForce = 12f;

    private Rigidbody2D _body;
    private Animator _anim;
    private Collider2D _coll;

    public LayerMask layerMask;

    private bool isGrounded = true;

    void Start () {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        HandleMovement(deltaX);
        HandleJump();
        HandleSpriteInvertion(deltaX);
    }

    private void HandleMovement(float deltaX)
    {
        //move o personagem
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;
        //seta a velocidade no parâmetro do animator
        _anim.SetFloat("speed", Mathf.Abs(deltaX));
    }

    private bool IsGrounded()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, Vector3.down, _coll.bounds.extents.y + 0.1f, layerMask);
        _anim.SetBool("isGrounded", isGrounded);
        return isGrounded;
    }

    private void HandleJump()
    {
        //pula caso a telca espaço esteja apertada e o personagem esteja no chão
        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {
            Debug.Log("jumping");
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HandleSpriteInvertion(float deltaX)
    {
        //inverte o sprite caso o personagem vá para a direita ou esquerda
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

    }
}
