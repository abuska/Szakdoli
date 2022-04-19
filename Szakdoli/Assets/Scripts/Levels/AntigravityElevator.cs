using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigravityElevator : MonoBehaviour{
    [SerializeField] private float speed;
    public float gravityScale{get; private set; }

    private bool inTrigger;
    Dictionary<string, Rigidbody2D> playersInElevator = new Dictionary<string, Rigidbody2D>();
    Dictionary<string, float> playersGravity = new Dictionary<string, float>();

    private void Update(){
        if(inTrigger){ 
            foreach( KeyValuePair<string, Rigidbody2D> players in playersInElevator ){
                players.Value.velocity = new Vector2(players.Value.velocity.x, speed);
            }
        }
           
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            inTrigger = true;

            string playerName = collision.name;
            playersGravity.Add(playerName, collision.GetComponent<Rigidbody2D>().gravityScale);
            playersInElevator.Add(playerName, collision.GetComponent<Rigidbody2D>());

            collision.GetComponent<PlayerMovement>().setGravityScale(0);
          
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
       
        if(collision.tag == "Player"){

            string playerName = collision.name;

            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, 0);
            collision.GetComponent<PlayerMovement>().setGravityScale(playersGravity[playerName]);

            playersInElevator.Remove(playerName);
            playersGravity.Remove(playerName);
            
            if(playersInElevator.Count==0){
                inTrigger = false;
            }
        }
    }


}
