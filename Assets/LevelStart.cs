using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{

    public void RestartGame()
    {
        Time.timeScale = 1f; // Восстановить нормальное время
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

