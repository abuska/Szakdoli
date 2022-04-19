using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeWithKey : MonoBehaviour
{
    [SerializeField] private BoxCollider2D keyHoleColl;
    [SerializeField] private GameObject key;
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;
    private bool isPlayerHasKey = false;
    

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void Update(){
        if(!isPlayerHasKey){
            checkKey();
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" && isPlayerHasKey){
            anim.SetBool("isOpen", true);
            keyHoleColl.enabled = false;
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
