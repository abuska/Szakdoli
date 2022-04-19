using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator anim;

    private bool hit;

    private BoxCollider2D boxCollider;

    private void Awake(){
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile(){
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update(){
        if(hit){
            return;
        }else{
            float movementSpeed = speed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);

            lifeTime +=Time.deltaTime;
            if(lifeTime>resetTime){
                gameObject.SetActive(false);
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision){
        hit = true;
        base.OnTriggerEnter2D(collision); //Execute parent logic first;
        boxCollider.enabled = false;
        if(anim != null){
            anim.SetTrigger("explode");
        }else{
            Deactivate();
        }
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
