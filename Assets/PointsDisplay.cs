using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    void Start()
    {
        // Отображение собранных очков
        pointsText.text = StickManMenager.collectedPoints.ToString();
    }
}
