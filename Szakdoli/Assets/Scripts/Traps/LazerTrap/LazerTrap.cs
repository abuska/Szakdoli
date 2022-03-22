using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTrap : MonoBehaviour
{
    [SerializeField] private BoxCollider2D buttonCollider;
    [SerializeField] private BoxCollider2D[] trapsCollider;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private float coolDownTimer = Mathf.Infinity;
    private bool isButtonTurnOff = false;
   
    private void Update(){
        Button();
        if(isButtonTurnOff){
            Debug.Log("Button Turn Off");
        }else{
            if(coolDownTimer >= attackCooldown){
                coolDownTimer = 0;
                DamagePlayer();
            }
        }
    }

    private void Button(){
       
        RaycastHit2D raycastHit = Physics2D.BoxCast(buttonCollider.bounds.center + transform.right,
        new Vector3(buttonCollider.bounds.size.x, buttonCollider.bounds.size.y, buttonCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        if(raycastHit.collider != null){
           if(Input.GetKey(KeyCode.E)){
               isButtonTurnOff = true;
           }
        }
    }
    private void DamagePlayer(){
       
        RaycastHit2D raycastHit = Physics2D.BoxCast(buttonCollider.bounds.center + transform.right,
        new Vector3(buttonCollider.bounds.size.x, buttonCollider.bounds.size.y, buttonCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        if(raycastHit.collider != null){
           raycastHit.transform.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
