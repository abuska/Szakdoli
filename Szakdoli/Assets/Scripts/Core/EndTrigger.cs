
using UnityEngine;

public class EndTrigger : MonoBehaviour{

    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision){
        //Todo átírni mind 3 playerre
        if(collision.tag == "Player"){
            gameManager.CompleteLevel();   
        }
    }
}
