using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject deathPanel;
    public GameObject victoryPanel;

    private GameObject player;
    private Scene currentScene;

    public bool GameWon { get; private set; }

    // Use this for initialization
    void Start () {
        player = PlayerManager.instance.Player;
        currentScene = SceneManager.GetActiveScene();
        GameWon = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayerDeath()
    {
        deathPanel.SetActive(true);
        PlayerManager.instance.IsPlayerAlive = false;
        PlayerManager.instance.Die();
    }

    public void TryAgain()
    {
        deathPanel.SetActive(false);
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene != null)
        {
            SceneManager.LoadScene(sceneName);

            currentScene = scene;
        }
    }

    public void PlayerWin()
    {
        victoryPanel.SetActive(true);
        GameWon = true;
    }

}
