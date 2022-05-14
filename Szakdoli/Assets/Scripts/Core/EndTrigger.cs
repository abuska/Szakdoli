
using UnityEngine;
using System.Collections.Generic;

public class EndTrigger : MonoBehaviour{

    private PlayerManager playerManager;
    private GameManager gameManager;
    Dictionary<string, bool> isPlayerAtGoal = new Dictionary<string, bool>();

    private void Awake(){
        
        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();

        for(int i = 0; i<playerManager.getPlayerCount(); i++){
            isPlayerAtGoal.Add(playerManager.getPlayerByIndex(i).name,false);
        }
    }

    public void checkAllplayerInGoalOrDie(){

        foreach( KeyValuePair<string, bool> isPlayerAtGoal in isPlayerAtGoal ){
            if(isPlayerAtGoal.Value==false &&  (
                playerManager.getPlayerByName(isPlayerAtGoal.Key)!=null && 
                !playerManager.getPlayerByName(isPlayerAtGoal.Key).GetComponent<Health>().dead
            )){
                /*ha van olyan karakter aki NINCS a célban ÉS NEM halott,
                akkor ne vizsgálja tovább, hanem lépjen ki a metódusból*/
                return;
            }    
        }

        if(playerManager.getDeadPlayerNumber() > 0 || playerManager.getPlayerCount() == playerManager.getDeadPlayerNumber()){
            //Ha minden élő játékos a célba van de, van legalább egy halott
            gameManager.GameOver();
        }else{
            //Ha minden élő játékos a célban van és nincs halott
            gameManager.CompleteLevel();  
        }

    }

    private void SetPlayerStatusInList(string name, bool status){
        isPlayerAtGoal[name] = status;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            SetPlayerStatusInList(playerName, true);
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            SetPlayerStatusInList(playerName, false);
        }
    }

}
