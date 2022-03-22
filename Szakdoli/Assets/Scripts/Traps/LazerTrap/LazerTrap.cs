using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTrap : MonoBehaviour
{
    //TODO HEADEREK
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject[] traps;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private float coolDownTimer = Mathf.Infinity;
    private bool isButtonTurnOff = false;
   
    private void Update(){
        if(!isButtonTurnOff){
             Button();
            if(coolDownTimer >= attackCooldown){
                DamagePlayer();    
                coolDownTimer = 0;
            }
            coolDownTimer += Time.deltaTime;
        }     
    }

    private void TurnOffTraps(){
        for(int i = 0; i <= traps.Length-1 ; i++ ){
            //Turn off animáció
            traps[i].GetComponent<Animator>().SetBool("turnOff",true);
            //Az összes collidert kikapcsolja
            BoxCollider2D[] colliders = traps[i].GetComponents<BoxCollider2D>();
            foreach(BoxCollider2D bc in colliders){
                bc.enabled = false;
            }
        }
        isButtonTurnOff = true;
    }
    private void Button(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(button.GetComponent<BoxCollider2D>().bounds.center, button.GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.left, 0, playerLayer);
        if(raycastHit.collider != null){
           if(Input.GetKey(KeyCode.E)){
               button.GetComponent<Animator>().SetBool("turnOff",true);
               button.GetComponent<Collider2D>().enabled = false;
               TurnOffTraps();
           }
        }
    }
    private void DamagePlayer(){
        for(int i = 0; i <= traps.Length-1 ; i++ ){
            RaycastHit2D raycastHit = Physics2D.BoxCast(traps[i].GetComponent<BoxCollider2D>().bounds.center, traps[i].GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.down, 0.5f, playerLayer);
                if(raycastHit.collider != null && raycastHit.transform.GetComponent<Health>()!=null){
                    raycastHit.transform.GetComponent<Health>().TakeDamage(damage);
                }
                
        }

        
    }
}
