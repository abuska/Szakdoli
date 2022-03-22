using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour
{
    [SerializeField] private GameObject infoText;
    [SerializeField] private bool showInfoInTrigger;
    private bool isPlayerInCollider;

    private void Update(){
        if(isPlayerInCollider){
            if(showInfoInTrigger || Input.GetKey(KeyCode.E)){
                infoText.SetActive(true);
            }
        }else{
            infoText.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
           isPlayerInCollider = true;
        }
    }
     private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
           isPlayerInCollider = false;
        }
    }
}
