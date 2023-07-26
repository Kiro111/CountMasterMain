using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class plaerMenager : MonoBehaviour
{
    public Transform player;
    public int numberOfStickmans, numberOfEnemyStickmans;
    public int gate = 1;

    [SerializeField] public TextMeshPro CounterText;
    [SerializeField] private GameObject stickMan;
    //*************************************************

    [Range(0f, 2f)][SerializeField] private float DistanceFactor, Radius;

    //*************************************************

    public bool moveByTouch, gameState;
    private Vector3 mouseStartPos, playerStartPos;
    public float playerSpeed, roadSpeed;
    private Camera Meincamera;

    [SerializeField] private Transform road;
    [SerializeField] private Transform enemy;
    private bool attack;
    public static plaerMenager plaerMenagerInstanse;

    [SerializeField] private GameObject SecondCam;
    public bool FinishLine, moveTheCamera, planeLinie;


    

    void Start()
    {
        player = transform;
        numberOfStickmans = transform.childCount - 1;

        Meincamera = Camera.main;

        plaerMenagerInstanse = this;

        gameState = false;

        var cinemachineTransposer = SecondCam.GetComponent<Cinemachine.CinemachineVirtualCamera>()
            .GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
        cinemachineTransposer.m_FollowOffset = new Vector3(8.8f, 26f, -23.7f);

        if (CounterText == null)
        {
            Debug.LogError("CounterText is not assigned!");
        }
        else
        {
            CounterText.text = numberOfStickmans.ToString();
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            var enemyDirection = new Vector3(enemy.position.x, transform.position.y, enemy.position.z) - transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation =
                    Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 3f);
            }

            if (enemy.GetChild(1).childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var Distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;

                    if (Distance.magnitude < 3f)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                            new Vector3(enemy.GetChild(1).GetChild(0).position.x, transform.GetChild(i).position.y,
                                enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime * 0.1f);
                    }
                }

            }

            else
            {
                attack = false;
                roadSpeed = -10f;
                FormatStickMan();

                for (int i = 1; i < transform.childCount; i++)
                {
                    transform.GetChild(i).rotation = Quaternion.identity;
                }

                enemy.gameObject.SetActive(false);

            }



            if (transform.childCount == 1)
            {
                enemy.transform.GetChild(1).GetComponent<enemyMenager>().StopAttacking();
                gameObject.SetActive(false);

            }
        }
        else
        {
            MoveThePlayer();

        }

        if (transform.childCount == 1 && FinishLine)
        {
            gameState = false;
        }

        if (planeLinie)
        {
            FinishLine = false;
           


        }

        if (gameState)
        {
            road.Translate(road.forward * Time.deltaTime * roadSpeed);
            //for (int i = 1; i < player.childCount; i++)
            ////{


            ////    transform.GetChild(i).GetComponent<Animator>().SetBool("run", true);
            ////}
        }
        if (moveTheCamera && transform.childCount > 1)
        {
            var cinemachineTransposer = SecondCam.GetComponent<CinemachineVirtualCamera>()
              .GetCinemachineComponent<CinemachineTransposer>();

            var cinemachineComposer = SecondCam.GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineComposer>();

            cinemachineTransposer.m_FollowOffset = new Vector3(8.8f, Mathf.Lerp(cinemachineTransposer.m_FollowOffset.y,
                transform.GetChild(1).position.y + 30f, Time.deltaTime * 1f), -23.7f);

            cinemachineComposer.m_TrackedObjectOffset = new Vector3(0f, Mathf.Lerp(cinemachineComposer.m_TrackedObjectOffset.y,
               20f, Time.deltaTime * 2f), 0f);

        }
        if (CounterText != null)
        {
            CounterText.text = numberOfStickmans.ToString();
        }



    }


    void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            moveByTouch = true;

            var plane = new Plane(Vector3.up, 0f);

            var ray = Meincamera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                mouseStartPos = ray.GetPoint(distance + 1f);
                playerStartPos = transform.position;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            moveByTouch = false;

        }

        if (moveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = Meincamera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);

                var move = mousePos - mouseStartPos;

                var control = playerStartPos + move;


                if (numberOfStickmans > 50)
                    control.x = Mathf.Clamp(control.x, -3f, 3f);
                else
                    control.x = Mathf.Clamp(control.x, -5f, 5f);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerSpeed)
                    , transform.position.y, transform.position.z);

            }
        }

    }


    public void FormatStickMan()
    {
        for (int i = 0; i < player.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            var NewPos = new Vector3(x, 0f, z);
            player.transform.GetChild(i).DOLocalMove(NewPos, 0.5f).SetEase(Ease.OutBack);
        }



    }


    public void MakeStickMan(int number)
    {
        for (int i = numberOfStickmans; i < number; i++)
        {
            Instantiate(stickMan, transform.position, Quaternion.identity, transform);
        }

        numberOfStickmans = transform.childCount - 1;
        CounterText.text = numberOfStickmans.ToString(); // Обновляем CounterText после добавления стикменов

        FormatStickMan();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false; // gate 1
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false; // gate 2

            var gateManager = other.GetComponent<gateMeneger>();

            numberOfStickmans = transform.childCount - 1;

            if (gateManager.multiply == true)
            {
                MakeStickMan(numberOfStickmans * gateManager.randomNumber);
                Destroy(other.gameObject);
            }

            else if (gateManager.multiply == false)
            {
                MakeStickMan(numberOfStickmans + gateManager.randomNumber);
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("enemy"))
        {
            enemy = other.transform;
            attack = true;

            roadSpeed = -3;
            other.transform.GetChild(1).GetComponent<enemyMenager>().AttackThen(transform);



            StartCoroutine(UpdateTheEnemyAndPlayerStickMansNumbers());

        }
        if (other.CompareTag("Finish"))
        {
            SecondCam.SetActive(true);
            FinishLine = true;
            Tower.TowerInstance.CreateTower(transform.childCount - 1);
            transform.GetChild(0).gameObject.SetActive(false);
            playerSpeed = 0;
        }
        if (other.CompareTag("Sea"))
        {
            StartCoroutine(DelayedMakeStickMan());
        }
       
        





        IEnumerator UpdateTheEnemyAndPlayerStickMansNumbers()
        {

            numberOfEnemyStickmans = enemy.transform.GetChild(1).childCount - 1;
            numberOfStickmans = transform.childCount - 1;

            while (numberOfEnemyStickmans > 0 && numberOfStickmans > 0)
            {
                numberOfEnemyStickmans--;
                numberOfStickmans--;

                enemy.transform.GetChild(1).GetComponent<enemyMenager>().CounterText.text = numberOfEnemyStickmans.ToString();
                CounterText.text = numberOfStickmans.ToString();

                yield return null;
            }

            if (numberOfEnemyStickmans == 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).rotation = Quaternion.identity;
                }
            }
        }
    }

    internal void MakeStickMan(int v, object number)
    {
        throw new NotImplementedException();
    }


    private IEnumerator DelayedMakeStickMan()
    {
        yield return new WaitForSeconds(1f); // Задержка в две секунды

        MakeStickMan(numberOfStickmans);
    }
}
