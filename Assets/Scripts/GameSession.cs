using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int coinStash = 0;
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] TextMeshProUGUI coins;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lives.text = playerLives.ToString();
        coins.text = coinStash.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            playerLives--;
            lives.text = playerLives.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            ResetGameSession();
        }
    }

    public void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        Destroy(FindObjectOfType<LivesAndCoinsCanvas>().gameObject);
        Destroy(FindObjectOfType<ScenePersist>().gameObject);
    }

    public void ProcessCoinPickup()
    {
        coinStash++;
        coins.text = coinStash.ToString();
    }
}
