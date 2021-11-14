using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text gameOverText;

    PacmanMovement pacman;
    int currentPoints;

    private void Start()
    {
        pacman = FindObjectOfType<PacmanMovement>();
        gameOverText.enabled = false;
    }

    private void Update()
    {
        //reset current level on pressing 'R'
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    /// <summary>
    /// Add points to current score and refresh score text
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(int points)
    {
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
        gameOverText.enabled = true;
        yield return new WaitForSeconds(2);

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
