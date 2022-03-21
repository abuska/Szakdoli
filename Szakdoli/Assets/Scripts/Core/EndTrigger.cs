
using UnityEngine;
using System.Collections.Generic;

public class EndTrigger : MonoBehaviour{

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameManager gameManager;
    Dictionary<string, bool> isPlayerAtGoal = new Dictionary<string, bool>();

    private void Awake()
    {
        for(int i = 0; i<playerManager.getPlayerNumber(); i++){
            isPlayerAtGoal.Add(playerManager.getPlayerByIndex(i).name,false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            isPlayerAtGoal[playerName] = true;
           
        }
        foreach( KeyValuePair<string, bool> isPlayerAtGoal in isPlayerAtGoal )
        {
            if(isPlayerAtGoal.Value==false){
                return;
            }    
        }
        gameManager.CompleteLevel();  
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            isPlayerAtGoal[playerName] = false;
        }
    }
}
