using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevelPassword : MonoBehaviour
{
    [SerializeField] private GameObject infoText;

       private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            infoText.SetActive(false);
        }
        
    }
}
