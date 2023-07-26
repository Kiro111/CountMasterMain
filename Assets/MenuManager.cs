using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject startMenuObj;
  
   
    public void StartTheGame()
    {
        startMenuObj.SetActive(false);
        
        plaerMenager.plaerMenagerInstanse.gameState = true;

        plaerMenager.plaerMenagerInstanse.player.GetChild(1).GetComponent<Animator>().SetBool("run", true);
    }
}
