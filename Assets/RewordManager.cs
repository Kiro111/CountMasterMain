using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private GameObject pileOfCoins1;
    [SerializeField] private GameObject pileOfCoins2;
    [SerializeField] private GameObject pileOfCoins3;
    [SerializeField] private GameObject pileOfCoins4;
    [SerializeField] private GameObject pileOfCoins5;
    [SerializeField] private GameObject pileOfCoins6;
    [SerializeField] private GameObject pileOfCoins7;
    [SerializeField] private GameObject pileOfCoins8;
    [SerializeField] private GameObject pileOfCoins9;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private RectTransform[] coinRectTransforms;
    [SerializeField] private int coinsAmount;
    [SerializeField] private RectTransform targetRectTransform; // Спрайт, к которому двигаются монеты

    private Vector2[] initialPositions;

    private float animationSpeed = 1.5f; // Скорость анимации
    private float targetScale = 1f; // Целевой масштаб монет

    private void Start()
    {
        if (coinsAmount == 0)
            coinsAmount = 10; // Замените это значение на фактическое количество монет в инспекторе

        initialPositions = new Vector2[coinsAmount];

        for (int i = 0; i < coinsAmount; i++)
        {
            initialPositions[i] = coinRectTransforms[i].anchoredPosition;
        }
    }

    public void CountCoins()
    {
        pileOfCoins.SetActive(true);
        pileOfCoins1.SetActive(true);
        pileOfCoins2.SetActive(true);
        pileOfCoins3.SetActive(true);
        pileOfCoins4.SetActive(true);
        pileOfCoins5.SetActive(true);
        pileOfCoins6.SetActive(true);
        pileOfCoins7.SetActive(true);
        pileOfCoins8.SetActive(true);
        pileOfCoins9.SetActive(true);
        float delay = 0f;

        DOTween.timeScale = animationSpeed; // Установка скорости анимации

        for (int i = 0; i < coinsAmount; i++)
        {
            Vector3[] path = new Vector3[] {
                new Vector3(coinRectTransforms[i].anchoredPosition.x, coinRectTransforms[i].anchoredPosition.y, 0f), // Добавляем компонент "z" равный 0
                new Vector3(targetRectTransform.anchoredPosition.x, coinRectTransforms[i].anchoredPosition.y, 0f),
                new Vector3(targetRectTransform.anchoredPosition.x, targetRectTransform.anchoredPosition.y, 0f)
            };

            coinRectTransforms[i].DOPath(path, 1f / animationSpeed, PathType.CatmullRom)
                .SetDelay(delay).SetEase(Ease.OutBack);

            coinRectTransforms[i].DOLocalRotate(new Vector3(360f, 360f, 360f), 1f / animationSpeed, RotateMode.LocalAxisAdd)
                .SetDelay(delay + 0.5f / animationSpeed).SetEase(Ease.Linear);

            coinRectTransforms[i].DOScale(Vector3.zero, 8f / animationSpeed).SetDelay(delay + 1f / animationSpeed).SetEase(Ease.OutBack);

            delay += 0.5f / animationSpeed;

            counter.transform.parent.GetChild(0).DOScale(1.1f, 0.1f / animationSpeed)
                .SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f / animationSpeed);
        }

        StartCoroutine(CountDollars());
    }

    private IEnumerator CountDollars()
    {
        yield return new WaitForSecondsRealtime(0.5f / animationSpeed);
        PlayerPrefs.SetInt("CountDollar", PlayerPrefs.GetInt("CountDollar") + 200 + PlayerPrefs.GetInt("BPrize"));
        counter.text = PlayerPrefs.GetInt("CountDollar").ToString();
        PlayerPrefs.SetInt("BPrize", 0);
    }
}
