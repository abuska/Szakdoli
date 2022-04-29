using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour{
    
    [SerializeField] private Transform otherSide;
    private float teleportTimer = Mathf.Infinity;

    Dictionary<string, GameObject> playersInTeleport = new Dictionary<string, GameObject>();

    private void Update(){
        if(collision.tag == "Player" && Input.GetKey(KeyCode.E) && teleportTimer>1f){
            collision.transform.position = otherSide.transform.position;
            teleportTimer=0;
        }
        teleportTimer+=Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            playersInTeleport.Add(playerName, collision);
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            string playerName = collision.name;
            playersInTeleport.Remove(playerName);
        }
    }
}
