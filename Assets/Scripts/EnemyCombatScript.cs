using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{

    public float Health = 200f;
    public float BaseDamage = 25f;
    private Animator _anim;

    private Color spriteColor;
    private SpriteRenderer _rend;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetFloat("Health", Health);

        _rend = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }

    // Update is called once per frame
    void Update()
    {
        spriteColor = GetComponent<SpriteRenderer>().color;
        _anim.SetFloat("Health", Health);
    }

    internal void ReceiveAttack(float baseDamage)
    {
        Health -= baseDamage;
        Debug.Log("Enemy health: " + Health);
        StartCoroutine(FlashSprite());

        if (Health <= 0)
        {
            _anim.SetTrigger("Death");
        }
    }

    private IEnumerator FlashSprite()
    {
        for (int n = 0; n < 2; n++)
        {
            WhiteSprite();
            yield return new WaitForSeconds(0.1f);
            NormalSprite();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void WhiteSprite()
    {
        _rend.material.shader = shaderGUItext;
        _rend.color = Color.white;
    }

    void NormalSprite()
    {
        _rend.material.shader = shaderSpritesDefault;
        _rend.color = Color.white;
    }

    private void SetSpriteColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
