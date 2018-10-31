using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour {
    public float speed = 250f;
    public float jumpForce = 12f;

    private Rigidbody2D _body;
    private Animator _anim;
    private Collider2D _box;


    /// <summary>
    /// ARRUMAR O ANIMATION CONTROLLER PARA A ANIMAÇÃO DE JUMP, SETAR A VARIAVEL BOLEANA DE UMA MANEIRA QUE VÁ FUNCIONAR
    /// </summary>

    // Use this for initialization
    void Start () {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<CapsuleCollider2D>();
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
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool isGounded = false;
        if (hit != null)
        {
            isGounded = true;
        }

        _anim.SetBool("isGrounded", isGounded);
        return isGounded;
    }

    private void HandleJump()
    {
        //pula caso a telca espaço esteja apertada e o personagem esteja no chão
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _anim.SetBool("isGrounded", true);
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
