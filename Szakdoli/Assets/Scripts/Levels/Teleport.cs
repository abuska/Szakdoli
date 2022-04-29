using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour{
    
    [SerializeField] private Transform otherSide;
    private float teleportTimer = Mathf.Infinity;

   
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player" && Input.GetKey(KeyCode.E) && teleportTimer>1f){
            collision.transform.position = otherSide.transform.position;
            teleportTimer=0;
        }
        teleportTimer+=Time.deltaTime;
    }
}
