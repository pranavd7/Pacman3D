using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] float timeToWaitAfterGameOver = 5f;

    Pacman pacman;
    int currentPoints;
    int totalFood;
    bool isRestarting;
    float timer;

    private void Start()
    {
        pacman = FindObjectOfType<Pacman>();
        totalFood = FindObjectsOfType<Food>().Length;
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        //reset current level on pressing 'R'
        if (Input.GetKeyDown(KeyCode.R) && !isRestarting)
        {
            RestartLevel();
        }

        //quit on pressing 'Escape'
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadLevel(0);
        }

        if (totalFood <= 0 && !isRestarting)
        {
            gameOverText.text = "Level Completed!";
            gameOverText.fontSize = 50;
            EndGame();
        }

        if (isRestarting)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                RestartLevel();
            }
            timer -= Time.deltaTime;

            //refresh the gameover text
            timerText.text = "Restarting in " + (int)timer + " seconds";
        }
    }

    /// <summary>
    /// Add points to current score and refresh score text
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(int points, bool isTrap)
    {
        //keep track of the number of food available
        if (!isTrap)
            totalFood--;
        currentPoints += points;
        scoreText.text = "Score: " + currentPoints;
    }

    /// <summary>
    /// End the game 
    /// </summary>
    public void EndGame()
    {
        StartCoroutine(GameOver());
    }

    /// <summary>
    /// Destroy player and traps then restart level
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOver()
    {
        isRestarting = true;

        //disable all traps
        Trap[] traps = FindObjectsOfType<Trap>();
        foreach (Trap trap in traps)
        {
            trap.gameObject.SetActive(false);
        }

        //play death animation and wait for 2 seconds
        pacman.Die();
        yield return new WaitForSeconds(2);
        Destroy(pacman.gameObject);

        //show gameover screen
        gameOverPanel.SetActive(true);
        controlsPanel.SetActive(false); ;
        timer = timeToWaitAfterGameOver;
        yield return new WaitForSeconds(timeToWaitAfterGameOver);

        RestartLevel();
    }

    /// <summary>
    /// Load a scene
    /// </summary>
    /// <param name="level"></param>
    void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Restart current level
    /// </summary>
    void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
