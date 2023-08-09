using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    [SerializeField] private float respawnDelay = 2;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text winText;
    [SerializeField] private GameObject winnerUI;

    private int score = 0;
    public bool gameStopped = false;

    private void Start()
    {
        score = Data.scoreKeeped;
        scoreText.text = score.ToString();
    }

    public void SaveScore()
    {
        Data.scoreKeeped = score;
    }
    public void ResetScore() {
        Data.scoreKeeped = 0;
    }

    public void ResetPosition()
    {
        if (!gameStopped)
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        gameStopped= true;
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore( int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void LevelUp()
    {
        winText.text = "Seviye Tamamlandi \n Skorunuz: " + score;
        winnerUI.SetActive(true);
        Invoke(nameof(NextLevel), respawnDelay);
    }
    
    public void NextLevel()
    {
        var tempSkor = score;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        score = tempSkor;
    }
}