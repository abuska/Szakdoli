using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            Debug.Log("True");
            anim.SetBool("isOpen", true);
            body.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            Debug.Log("True");
            anim.SetBool("isOpen", false);
            body.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
