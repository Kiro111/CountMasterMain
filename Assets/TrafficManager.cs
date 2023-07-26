using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public GameObject blueSpritePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            GameObject blueSpriteInstance = Instantiate(blueSpritePrefab, transform.position, Quaternion.identity);

            blueSpriteInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            blueSpriteInstance.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            plaerMenager.plaerMenagerInstanse.numberOfStickmans--;
            plaerMenager.plaerMenagerInstanse.CounterText.text = plaerMenager.plaerMenagerInstanse.numberOfStickmans.ToString();

        }


    }

    
}
