using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithKey : MonoBehaviour
{
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private GameObject key;
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;
    private bool isPlayerHasKey = false;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update(){
      
        if(!isPlayerHasKey){
            checkKey();
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" && isPlayerHasKey){
            openDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player" && isPlayerHasKey){
            closeDoor();
        }
    }


    private void checkKey(){
        RaycastHit2D raycastHitPlayer = Physics2D.BoxCast(key.GetComponent<BoxCollider2D>().bounds.center, key.GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.left, 0, playerLayer);
        if(raycastHitPlayer.collider != null && Input.GetKey(KeyCode.E)){
            collectKey();
        }
    }

    private void collectKey(){
        isPlayerHasKey = true;
        key.SetActive(false);
    }

    private void openDoor(){
        anim.SetBool("isOpen", true);
        doorCollider.enabled = false;
    }
    
    private void closeDoor(){
        anim.SetBool("isOpen", false);
        doorCollider.enabled = true;
    }
}
