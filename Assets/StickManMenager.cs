using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StickManMenager : MonoBehaviour
{
    public static int collectedPoints;
    public Animator StickManAnimator;
    [SerializeField] float stepToGo;
    [SerializeField] float moveDuration;
    [Range(0f, 2f)]
    [SerializeField] private float DistanceFactor, Radius;
    public float delay = 0f;
    public GameObject animatioCoins;

    private bool isCollided = false; // Флаг для отслеживания столкновения
    private float timer = 0f; // Таймер для отслеживания времени

    [SerializeField] private GameObject restartPanel;
    [SerializeField] GameObject restartPanel1;

    public int sceneIndex = 1;
    public float delayScen = 10f;

    [SerializeField] private RewardManager rewardManager; // Добавьте ссылку на класс RewardManager
    private object number;

    private void Start()
    {
        StickManAnimator = GetComponent<Animator>();
        StickManAnimator.SetBool("run", true);
    }

    private void Update()
    {
        if (isCollided)
        {
            timer += Time.deltaTime;

            if (timer >= delay)
            {
                // Остановка анимации бега
                StickManAnimator.SetBool("run", false);
                plaerMenager.plaerMenagerInstanse.roadSpeed = 0;

                // Остановка значения roadSpeed через 3 секунды
                StartCoroutine(StopRoadSpeed());
            }
        }
    }

    public void ShowRestartButton()
    {
        Invoke("ActivateRestartPanel", 15f);
        Time.timeScale = 0;
    }

    void ActivateRestartPanel()
    {
        restartPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private IEnumerator StopRoadSpeed()
    {
        yield return new WaitForSeconds(delay);
        plaerMenager.plaerMenagerInstanse.roadSpeed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "red":
                if (other.transform.parent.childCount > 0)
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                break;
            case "jump":
                transform.DOJump(transform.position, 5f, 1, 1.5f).SetEase(Ease.Flash).OnComplete(plaerMenager.plaerMenagerInstanse.FormatStickMan);
                break;
        }

        if (other.CompareTag("Stair"))
        {
            transform.parent.parent = null;
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = GetComponent<Collider>().isTrigger = false;
            StickManAnimator.SetBool("run", false);
            plaerMenager.plaerMenagerInstanse.playerSpeed = 0;

            if (!plaerMenager.plaerMenagerInstanse.moveTheCamera)
            {
                plaerMenager.plaerMenagerInstanse.moveTheCamera = true;
            }

            if (plaerMenager.plaerMenagerInstanse.player.transform.childCount == 1)
            {
                other.GetComponent<Renderer>().material.DOColor(new Color(0.4f, 0.98f, 0.65f), 0.5f).SetLoops(1000, LoopType.Yoyo)
                    .SetEase(Ease.Flash);

                ShowRestartButton();

                Time.timeScale = 1;
            }
        }

        if (other.CompareTag("Plane"))
        {
            GetComponent<Rigidbody>().isKinematic = GetComponent<Collider>().isTrigger = true;

            for (int i = 0; i < plaerMenager.plaerMenagerInstanse.player.childCount; i++)
            {
                var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
                var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

                var NewPos = new Vector3(x, 16.5f, z);
                plaerMenager.plaerMenagerInstanse.playerSpeed = 0;
                plaerMenager.plaerMenagerInstanse.player.transform.GetChild(i).DOLocalMove(NewPos, 0.5f).SetEase(Ease.OutBack);
            }
        }

        if (other.CompareTag("chest"))
        {
            int collectedPoints = 10; // Здесь 10 - пример значения очков, которое вы хотите сохранить
            PlayerPrefs.SetInt("CollectedPoints", collectedPoints); // Сохранение собранных очков в PlayerPrefs
            PlayerPrefs.Save();

            // Запуск таймера для остановки анимации бега через 3 секунды
            isCollided = true;

            if (rewardManager != null)
            {
                StartCoroutine(WaitAndCountCoins());
            }

  
        }

        if (other.CompareTag("Sea"))
        {
            StickManAnimator.SetBool("run", false);
            StickManAnimator.SetBool("cheering", true);
            transform.DOMoveY(transform.position.y  -10f, 5f);
            Destroy(gameObject, 0.9f);
            plaerMenager.plaerMenagerInstanse.numberOfStickmans--;
            plaerMenager.plaerMenagerInstanse.CounterText.text = plaerMenager.plaerMenagerInstanse.numberOfStickmans.ToString();
           
        }


    }

    private IEnumerator LoadSceneWithDelay()
    {
        // Ждем указанное количество секунд
        yield return new WaitForSeconds(delayScen);

        // Загружаем сцену по индексу
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator WaitAndCountCoins()
    {
        yield return new WaitForSeconds(3f);
        rewardManager.CountCoins();
    }
}
