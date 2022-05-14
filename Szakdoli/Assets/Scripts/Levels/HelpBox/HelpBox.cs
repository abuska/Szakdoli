using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour
{
    [SerializeField] private GameObject infoText;
    [SerializeField] private bool showInfoInTrigger;

       private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            if(( showInfoInTrigger || Input.GetKey(KeyCode.E))){
                setInfoTextVisibility(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            setInfoTextVisibility(false);
        }
    }

    private void setInfoTextVisibility(bool visibility){
        infoText.SetActive(visibility);
    }
}
