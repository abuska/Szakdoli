using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Több komment!
//Pálya választásnál óvaosan.

//1500-2000sor kód.

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players = {};
    private int activePlayerIndex = 0;
    private float changePlayerTimer = Mathf.Infinity;

    private bool allPlayerDead = false;

    private void Awake(){
        ActivatePlayer(0);
    }

    void Update(){

        if(Input.GetKey(KeyCode.LeftControl) && changePlayerTimer > 0.5){
            ChangePlayer();
        }
        changePlayerTimer +=Time.deltaTime;
    }

    private void ChangePlayer(){
        DeactivatePlayer(activePlayerIndex);
        FindAvailablePlayer();
        changePlayerTimer = 0;
    }

    private void FindAvailablePlayer(){
        if(!allPlayerDead){
            int playerCounter = players.Length;
            int nextPlayerIndex = activePlayerIndex;

            for(int i = 0 ; i < playerCounter; i++){
                if( nextPlayerIndex < players.Length - 1 ){
                    nextPlayerIndex +=1 ;
                    
                }else{
                    nextPlayerIndex = 0;
                }

                if(!players[nextPlayerIndex].GetComponent<Health>().dead){
                        activePlayerIndex = nextPlayerIndex;
                        ActivatePlayer(activePlayerIndex);
                        return;
                }
            }
            allPlayerDead = true;
        }
       
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
