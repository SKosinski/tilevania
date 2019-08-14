using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] float LevelLoadDelay = 0.5f;
    [SerializeField] float SlowMo = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("zupa");
        if (collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    
    IEnumerator LoadNextLevel()
    {
        if(FindObjectOfType<ScenePersist>())
            Destroy(FindObjectOfType<ScenePersist>().gameObject);
        //Time.timeScale = SlowMo;
        yield return new WaitForSeconds(LevelLoadDelay);

        if(SceneManager.GetActiveScene().name == "Success Screen")
        {
            SceneManager.LoadScene(0);
            FindObjectOfType<GameSession>().ResetGameSession();
        }
        else
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex + 1);
        }
        Time.timeScale = 1f;
    }
}
