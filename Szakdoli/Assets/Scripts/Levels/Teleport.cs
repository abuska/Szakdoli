using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour{
    
    [SerializeField] private Transform otherSide;
    private static float teleportTimer = Mathf.Infinity;

    Dictionary<string, GameObject> playersInTeleport = new Dictionary<string, GameObject>();

    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Update(){
        moveAllCharacterInTeleport();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            addPlayerInTeleport(collision.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
           removePlayerInTeleport(collision.name);
        }
    }

    private void moveAllCharacterInTeleport(){
        if(playersInTeleport.Count > 0 
            && playersInTeleport.TryGetValue(playerManager.getActivePlayerName(), out GameObject value) 
            && Input.GetKey(KeyCode.E) 
            && teleportTimer>1f
        ){
            foreach( KeyValuePair<string, GameObject> player in playersInTeleport ){
                player.Value.transform.position = otherSide.transform.position;
            }
            
            teleportTimer=0;
        }

        teleportTimer+=Time.deltaTime;
    }
    private void addPlayerInTeleport(string playerName) {
        playersInTeleport.Add(playerName, playerManager.getPlayerByName(playerName));
    }

    private void removePlayerInTeleport(string playerName){
        playersInTeleport.Remove(playerName);
    }
}
