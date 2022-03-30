using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour{

    /*[SerializeField] private PlayerManager playerManager;
    public float gravityScale{get; private set; }

    private float verticalInput;
    private bool onLadder;

    private bool isClimb;


    private void Update(){
        if(onLadder && Mathf.Abs(verticalInput) > 0){
                isClimb = true;
                playerManager.getActivePlayer().GetComponent<PlayerMovement>().Climb();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" ){
            onLadder = true;
            gravityScale = collision.GetComponent<Rigidbody2D>().gravityScale;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            onLadder = false;
            collision.GetComponent<PlayerMovement>().setGravityScale(gravityScale);
            gravityScale = 0;
        }
    }*/


}