using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players = {};
    private int activePlayerIndex = 0;
    private float changePlayerTimer = Mathf.Infinity;

    private void Awake(){
        ActivatePlayer(0);
    }

    void Update(){

        if(Input.GetKey(KeyCode.LeftShift) && changePlayerTimer > 0.5){
            ChangePlayer();
        }
        changePlayerTimer +=Time.deltaTime;
    }



    private void ChangePlayer(){

        DeactivatePlayer(activePlayerIndex);

        if( activePlayerIndex < players.Length - 1 ){
            activePlayerIndex +=1 ;
        }else{
           activePlayerIndex = 0;
        }

        ActivatePlayer(activePlayerIndex);

        changePlayerTimer = 0;
    }

    private void DeactivatePlayer(int index){
        players[index].GetComponent<PlayerMovement>().enabled = false;
        players[index].GetComponent<PlayerAttack>().enabled = false;
    }
    private void ActivatePlayer(int index){
        players[activePlayerIndex].GetComponent<PlayerMovement>().enabled = true;
        players[activePlayerIndex].GetComponent<PlayerAttack>().enabled = true;
    }

    public Transform getActivePlayerTransform(){
        return players[activePlayerIndex].transform;
    }
    public GameObject getActivePlayer(){
        return players[activePlayerIndex];
    }

}
