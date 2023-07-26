using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStickmanManager : MonoBehaviour
{
    public GameObject redSprintePrefab;
    public GameObject blueSpritePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.parent.childCount > 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
          
        }

        if (other.CompareTag("Player"))
        {
            GameObject blueSpriteInstance = Instantiate(blueSpritePrefab, transform.position, Quaternion.identity);
            // spriteInstance.GetComponent<SpriteRenderer>().color = Color.red;
            blueSpriteInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            blueSpriteInstance.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            GameObject redSpriteInstance = Instantiate(redSprintePrefab, transform.position, Quaternion.identity);
            // spriteInstance.GetComponent<SpriteRenderer>().color = Color.red;
            redSpriteInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            redSpriteInstance.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
