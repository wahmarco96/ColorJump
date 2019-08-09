using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject gameOverPanel;
    public GameObject TouchtoStartObj;

    void Awake()
    {
        Time.timeScale = 1.0f;
    }



    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
        gameOverPanel.SetActive(true);

    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 0.01f;
        yield return new WaitForSecondsRealtime(0.7f);
        GetComponent<ScoreManager>().currentScoreText.color = Color.white;
        GetComponent<ScoreManager>().bestScoreText.color = Color.white;
        gameOverPanel.SetActive(true);

        yield break;

    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {
        TouchtoStartObj.SetActive(false);
    }

}
