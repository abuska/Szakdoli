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
            anim.SetBool("isOpen", true);
            doorCollider.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player" && isPlayerHasKey){
            anim.SetBool("isOpen", false);
            doorCollider.enabled = true;
        }
    }


    private void checkKey(){
        RaycastHit2D raycastHitPlayer = Physics2D.BoxCast(key.GetComponent<BoxCollider2D>().bounds.center, key.GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.left, 0, playerLayer);
        if(raycastHitPlayer.collider != null && Input.GetKey(KeyCode.E)){
            isPlayerHasKey = true;
            key.SetActive(false);
        }
    }

}
