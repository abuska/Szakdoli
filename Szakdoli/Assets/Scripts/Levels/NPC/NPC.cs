using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject infoText;
    [SerializeField] private bool showInfoInTrigger;
    private bool isPlayerAlreadySeenMessage = false;
    
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player" && !isPlayerAlreadySeenMessage){
            if(( showInfoInTrigger || Input.GetKey(KeyCode.E))){
                setNpcTextVisibility(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player" && !isPlayerAlreadySeenMessage){
            setNpcTextVisibility(false);
            isPlayerAlreadySeenMessage = true;
        }
        
    }

    private void setNpcTextVisibility(bool visibility){
        infoText.SetActive(visibility);
    }


}
