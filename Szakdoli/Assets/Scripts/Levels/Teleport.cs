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
        if(playersInTeleport.Count>0 && playersInTeleport[playerManager.getActivePlayerName()]!=null && Input.GetKey(KeyCode.E) && teleportTimer>1f){
            foreach( KeyValuePair<string, GameObject> player in playersInTeleport ){
                player.Value.transform.position = otherSide.transform.position;
            }
            
            teleportTimer=0;
        }
        teleportTimer+=Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            playersInTeleport.Add(playerName, playerManager.getPlayerByName(collision.name));
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            playersInTeleport.Remove(playerName);
        }
    }
}
