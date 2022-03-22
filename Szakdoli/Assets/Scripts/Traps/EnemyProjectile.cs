using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;

    public void ActivateProjectile(){
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update(){
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime +=Time.deltaTime;
        if(lifeTime>resetTime){
            gameObject.SetActive(false);
        }
        Physics2D.IgnoreLayerCollision(11,12, true); //Ignorálja a létrákat
    }

    private void OnTriggerEnter2D(Collider2D collision){
        base.OnTriggerEnter2D(collision); //Execute parent logic first;
        gameObject.SetActive(false);
    }
}
