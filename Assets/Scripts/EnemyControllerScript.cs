using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControllerScript : MonoBehaviour
{
    [Header("Properties")]
    public float Health = 200f;
    public float BaseDamage = 25f;
    public float AlertRadius = 1f;
    public float AttackRadius = 0.3f;
    public float Speed = 40f;

    [Header("UI")]
    public Image healthBar;

    private Animator _anim;    
    private SpriteRenderer _rend;

    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    private EnemyCombatScript combatScript;
    
    public EnemyState State { get; set; }
    public Transform Player { get; set; }
    public float DeltaX { get; set; }

    // Use this for initialization
    void Start()
    {
        State = EnemyState.PATROL;
        Player = PlayerManager.instance.Player.transform;
        
        _anim = GetComponent<Animator>();
        _anim.SetFloat("Health", Health);
        _rend = gameObject.GetComponent<SpriteRenderer>();

        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");

        combatScript = GetComponent<EnemyCombatScript>();
    }

    internal void StopAttack()
    {
        combatScript.StopAttack();
    }

    internal void CastAttack()
    {
        combatScript.CastAttack();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("Health", Health);
        Debug.Log("State: " + State);
        LookForPlayer();
    
        HandleSpriteInvertion();
    }

    private void LookForPlayer()
    {
        if (PlayerManager.instance.IsPlayerDead())
        {
            State = EnemyState.PATROL;
            return;
        }

        var playerDistance = Vector2.Distance(Player.position, transform.position);
        if (playerDistance <= AlertRadius)
        {
            if (playerDistance <= AttackRadius)
            {
                State = EnemyState.ATTACKING;
                _anim.SetTrigger("Attacking");
            } else { 
                State = EnemyState.CHASING;
            }
        } else
        {
            State = EnemyState.PATROL;
        }
    }

    public void StartPatrol()
    {
        State = EnemyState.PATROL;
    }

    internal void ReceiveAttack(float baseDamage)
    {
        combatScript.ReceiveAttack(baseDamage);
    }

    public IEnumerator FlashSprite()
    {
        for (int n = 0; n < 2; n++)
        {
            WhiteSprite();
            yield return new WaitForSeconds(0.1f);
            NormalSprite();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void HandleSpriteInvertion()
    {
        if (!Mathf.Approximately(DeltaX, 0))
        {
            transform.localScale = new Vector3((Mathf.Sign(DeltaX) * 0.8f) * -1f, 0.8f, 0.8f);
            healthBar.fillOrigin = (int)(Mathf.Sign(DeltaX) == 1 ? Image.OriginHorizontal.Left : Image.OriginHorizontal.Right);
        }
    }

    private void WhiteSprite()
    {
        _rend.material.shader = shaderGUItext;
        _rend.color = Color.white;
    }

    private void NormalSprite()
    {
        _rend.material.shader = shaderSpritesDefault;
        _rend.color = Color.white;
    }

    private void SetSpriteColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AlertRadius);
    }
}
