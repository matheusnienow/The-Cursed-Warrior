using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject Player;
    public bool IsPlayerAlive { get; set; }

    private void Start()
    {
        IsPlayerAlive = true;
    }

    internal bool IsPlayerDead()
    {
        return !IsPlayerAlive;
    }

    public void Die()
    {
        Player.SendMessage("Die");
    }
}
