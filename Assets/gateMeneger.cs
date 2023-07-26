using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gateMeneger : MonoBehaviour
{
    public TextMeshPro GateNo;
    public int randomNumber;
    public bool multiply;
    public int gate;
    // Start is called before the first frame update
    void Start()
    {
        
        if (multiply)
        {
            randomNumber = Random.Range(2, 2);
            GateNo.text = "x" + randomNumber;

        }
        else
        {
            if (randomNumber % 2 != 0)
                randomNumber += 1;
            randomNumber = Random.Range(60,60);
            GateNo.text = randomNumber.ToString();
        }
    }
    private void Update()
    {

    }


}
