using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigravityElevator : MonoBehaviour{
    [SerializeField] private float speed;
    public float gravityScale{get; private set; }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            gravityScale = collision.GetComponent<Rigidbody2D>().gravityScale;
            collision.GetComponent<PlayerMovement>().setGravityScale(0);
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, speed);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, 0);
            collision.GetComponent<PlayerMovement>().setGravityScale(gravityScale);
            gravityScale = 0;
        }
    }


}
