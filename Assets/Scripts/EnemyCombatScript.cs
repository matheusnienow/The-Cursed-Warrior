using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    private float fullHealth;

    private EnemyControllerScript _controller;
    private Animator _anim;
    private Collider2D _col;
    private float lastAttackTime = 0;
    private float attackDelay = 1;

    public LayerMask playerLayer;
    private GameObject _hitbox;

    // Use this for initialization
    void Start ()
    {
        _controller = GetComponent<EnemyControllerScript>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<Collider2D>();
        _hitbox = transform.Find("Hitbox").gameObject;
        fullHealth = _controller.Health;
    }

    public void CastAttack()
    {
        _hitbox.SetActive(true);
    }

    public void StopAttack()
    {
        _hitbox.SetActive(false);
    }

    internal void ReceiveAttack(float baseDamage)
    {
        _controller.Health -= baseDamage;
        Debug.Log("Enemy health: " + _controller.Health);
        StartCoroutine(_controller.FlashSprite());

        _controller.healthBar.fillAmount = _controller.Health / fullHealth;

        if (_controller.Health <= 0)
        {
            _anim.SetTrigger("Death");
        }
    }
}
