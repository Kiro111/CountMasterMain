using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlinkStaris : MonoBehaviour

 {
    public Color blinkColor = Color.red; // Цвет для мигания
    public float blinkDuration = 0.5f; // Длительность одного периода мигания

    private Renderer  renderer;
    private Color originalColor;
    private bool isBlinking = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Предположим, что игрок имеет тег "Player"
        {
            if (!isBlinking)
            {
                // Запускаем корутину мигания цвета
                StartCoroutine(BlinkColor());
            }
        }
    }

    private System.Collections.IEnumerator BlinkColor()
    {
        isBlinking = true;

        while (true)
        {
            // Применяем цвет мигания
            renderer.material.color = blinkColor;

            // Ждем заданное время
            yield return new WaitForSeconds(blinkDuration);

            // Восстанавливаем исходный цвет
            renderer.material.color = originalColor;

            // Ждем заданное время
            yield return new WaitForSeconds(blinkDuration);
        }

        isBlinking = false;
    }
}

