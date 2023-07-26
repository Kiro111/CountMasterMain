using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public int targetSceneIndex = 0; // ������ ������� �����
    public float delay = 6f; // �������� ����� ������ �����

    private bool buttonPressed = false;

    private void Update()
    {
        if (buttonPressed)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f)
            {
                // ������� ����� �� �������
                SceneManager.LoadScene(targetSceneIndex);
            }
        }
    }

    public void OnButtonClick()
    {
        // ���������� ������� ������
        buttonPressed = true;
    }
}
