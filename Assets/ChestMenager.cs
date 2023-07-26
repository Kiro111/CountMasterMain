using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMenager : MonoBehaviour
{
    private Animator chestanimator;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject coin_1;
    [SerializeField] private GameObject coin_2;
    [SerializeField] private GameObject coin_3;
    [SerializeField] private GameObject coin_4;
    private Animation chestanimator2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestanimator = GetComponent<Animator>();
            chestanimator.SetBool("open", true);

            // ��������� �������� ��� ��������� ����� ����� 1 �������
            StartCoroutine(ActivateCoinsAfterDelay(1f));
        }
    }

    // �������� ��� ��������� ����� ����� �������� �����
    private IEnumerator ActivateCoinsAfterDelay(float delay)
    {
        // ���� �������� �����
        yield return new WaitForSeconds(delay);

        // ���������� ������
        coin.SetActive(true);
        coin_1.SetActive(true);
        coin_2.SetActive(true);
        coin_3.SetActive(true);
        coin_4.SetActive(true);

        // ��������� �������� �����
        coin.GetComponent<Animator>().SetBool("AnimationCoin", true);
        coin_1.GetComponent<Animator>().SetBool("AnimationCoin_1", true);
        coin_2.GetComponent<Animator>().SetBool("AnimatioCoin_2", true);
        coin_3.GetComponent<Animator>().SetBool("AnimationCoin_3", true);
        coin_4.GetComponent<Animator>().SetBool("AnimationCoin_4", true);
    }
}
