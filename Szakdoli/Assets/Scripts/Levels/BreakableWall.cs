using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private Animator anim;

     private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" && collision.GetComponent<Animator>().GetBool("run") == true){
            anim.SetTrigger("break");
        }
    }

    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
