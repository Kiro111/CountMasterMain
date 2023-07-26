using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class enemyMenager : MonoBehaviour
{
    [SerializeField] public TextMeshPro CounterText;
    [SerializeField] private GameObject stickMan;
    //*************************************************
    [Range(0f, 1f)][SerializeField] private float DistanceFactor, Radius;

    public Transform enemy;
    public bool attack;

    [SerializeField] private ParticleSystem blood;



    void Start()
    {
        for(int i=0; i < Random.Range(10, 15); i++) 
        {
            Instantiate(stickMan, transform.position, new Quaternion(0f, 100f, 0f, 1f), transform);
        }

        CounterText.text = (transform.childCount - 1).ToString();

         FormatStickMan();

    }


    private void FormatStickMan()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            var NewPos = new Vector3(x, 0f, z);
            transform.transform.GetChild(i).localPosition = NewPos;
        }



    }

    // Update is called once per frame
    void Update()
    {
       if(attack && transform.childCount>1)
        {
            var enemyPoz = new Vector3(enemy.position.x, transform.position.y, enemy.position.z);
            var enemyDirection = enemy.position - transform.position;

            for(int i = 0 ; i < transform.childCount; ++i)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation,
                    Quaternion.LookRotation(enemyDirection, Vector3.up),
                    Time.deltaTime * 3);


                if (enemy.childCount > 1)
                {
                    var distace = enemy.GetChild(1).position - transform.GetChild(i).position;

                    if (distace.magnitude < 9)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                            enemy.GetChild(1).position, Time.deltaTime * 2f);
                    }
                }

            }
        }

    }
    public void AttackThen(Transform enemyForce)
    {
        enemy = enemyForce;
        attack = true;

         for (int i = 0;i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);
        }

    }


    public void StopAttacking()
    {
       plaerMenager.plaerMenagerInstanse.gameState = attack = false;
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("run", false);
        }
    }


}
