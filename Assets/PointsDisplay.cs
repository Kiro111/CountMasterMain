using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    void Start()
    {
        // ����������� ��������� �����
        pointsText.text = StickManMenager.collectedPoints.ToString();
    }
}
