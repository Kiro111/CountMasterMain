using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlinkStaris : MonoBehaviour

 {
    public Color blinkColor = Color.red; // ���� ��� �������
    public float blinkDuration = 0.5f; // ������������ ������ ������� �������

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
        if (collision.gameObject.CompareTag("Player")) // �����������, ��� ����� ����� ��� "Player"
        {
            if (!isBlinking)
            {
                // ��������� �������� ������� �����
                StartCoroutine(BlinkColor());
            }
        }
    }

    private System.Collections.IEnumerator BlinkColor()
    {
        isBlinking = true;

        while (true)
        {
            // ��������� ���� �������
            renderer.material.color = blinkColor;

            // ���� �������� �����
            yield return new WaitForSeconds(blinkDuration);

            // ��������������� �������� ����
            renderer.material.color = originalColor;

            // ���� �������� �����
            yield return new WaitForSeconds(blinkDuration);
        }

        isBlinking = false;
    }
}

