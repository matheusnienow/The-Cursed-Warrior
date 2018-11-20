using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour {
    public float speed = 250f;
    public float jumpForce = 12f;
    public float Health = 100f;

    private Rigidbody2D _body;
    private Animator _anim;
    private Collider2D _coll;
    private GameObject _hitbox;

    public LayerMask layerMask;

    private bool isGrounded = true;
    private int noOfClicks = 0;
    private float lastClickedTime = 0;
    private float maxComboDelay = 1;
    private float minComboDelay = 0;

    void Start () {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coll = GetComponent<CapsuleCollider2D>();
        _hitbox = transform.Find("Hitbox").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        HandleMovement(deltaX);
        HandleJump();
        HandleSpriteInvertion(deltaX);
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        var deltaTime = Time.time - lastClickedTime;
        _anim.SetFloat("clickDeltaTime", deltaTime);

        if (Input.GetKeyDown(KeyCode.J) & IsGrounded())
        {
            OnAttackButtonClick(deltaTime);
        }
        
    }

    private void OnAttackButtonClick(float deltaTime)
    {
        //Record time of last button click

        if (noOfClicks == 0)
        {
            _anim.SetTrigger("attack1");
            lastClickedTime = Time.time;
            noOfClicks++;
        }
        else if (noOfClicks == 1 && deltaTime > 0.4)
        {
            _anim.SetTrigger("attack2");
            lastClickedTime = Time.time;
            noOfClicks++;
        }
        else if (noOfClicks == 2 && deltaTime > 0.5)
        {
            _anim.SetTrigger("attack3");
            lastClickedTime = Time.time;
            noOfClicks++;
        }

        //limit/clamp no of clicks between 0 and 3 because you have combo for 3 clicks
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }

    public void CastAttack()
    {
        _hitbox.SetActive(true);
    }

    public void StopAttack()
    {
        _hitbox.SetActive(false);
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
