using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public int targetSceneIndex = 0; // Индекс целевой сцены
    public float delay = 6f; // Задержка перед сменой сцены

    private bool buttonPressed = false;

    private void Update()
    {
        if (buttonPressed)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f)
            {
                // Сменить сцену по индексу
                SceneManager.LoadScene(targetSceneIndex);
            }
        }
    }

    public void OnButtonClick()
    {
        // Обработчик нажатия кнопки
        buttonPressed = true;
    }
}
