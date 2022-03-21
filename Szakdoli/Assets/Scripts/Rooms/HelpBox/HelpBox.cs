using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour
{
    [SerializeField] private GameObject infoText;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            infoText.SetActive(true);
        }
    }
     private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            infoText.SetActive(false);
        }
    }
}
