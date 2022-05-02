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
    

    private GameManager gameManager;

    private void Awake(){
        gameManager = FindObjectOfType<GameManager>();
        ActivatePlayer(0);
    }

    void Update(){
        if(Input.GetKey(KeyCode.LeftControl) && changePlayerTimer > 0.5){
            ChangePlayer();
        }
        changePlayerTimer +=Time.deltaTime; 
        
    }

    public void ChangePlayer(){
        DeactivatePlayer(activePlayerIndex);
        FindAvailablePlayer();
        changePlayerTimer = 0;
    }

    public int getDeadPlayerNumber(){
        int playerCounter = getPlayerCount();
        int deadPlayerCounter = 0;
          for(int i = 0 ; i < playerCounter; i++){
                if(players[i].GetComponent<Health>().dead){
                    deadPlayerCounter+=1;
                }
        }
        return deadPlayerCounter;
    }

    public void ChangeDeadPlayer(){
        if(true){
            DeactivatePlayer(activePlayerIndex);
            FindAvailablePlayer();
            changePlayerTimer = 0;
        }
    }

    private void FindAvailablePlayer(){

            int playerCounter = getPlayerCount();
            int nextPlayerIndex = activePlayerIndex;

            for(int i = 0 ; i < playerCounter; i++){
                if( nextPlayerIndex < getPlayerCount() - 1 ){
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
    
    public string getActivePlayerName(){
        return players[activePlayerIndex].GetComponent<PlayerMovement>().getPlayerName();
    }
    
    public int getPlayerCount(){
        return players.Length;
    }
    
    public GameObject getPlayerByIndex(int index){
        return players[index];
    }
    
    public GameObject getPlayerByName(string playerName){
        int playerCounter = getPlayerCount();
        for(int i = 0 ; i < playerCounter; i++){
            if(players[i].GetComponent<PlayerMovement>().name==playerName){
                return players[i];
            }
        }
        return null;
    }

}
