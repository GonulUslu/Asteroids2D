using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;

    public int score = 0;
    public TextMeshProUGUI scoreValue;
    public Image[] lifeSprites;
    public GameObject gameOverPanel;
  
    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        lives--;

        if (lives<=0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
        UpdateLife(lives);
    }

    public void UpdateLife(int lives)
    {
        lifeSprites[lives - 1].enabled = false;
    }
    public void Respawn()
    {
        player.transform.position = Vector2.zero;
        player.gameObject.SetActive(true);
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
    }


    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        lives = 3;
        score = 0;
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
        Time.timeScale = 0;
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        if (asteroid.size < 0.75f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.25f)
        {
            score += 75;
        }
        else
        {
            score += 25;
        }

        scoreValue.text = score.ToString();
    }



}
