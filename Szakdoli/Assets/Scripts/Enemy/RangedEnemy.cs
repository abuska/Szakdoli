using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Fire Parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
   
    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;


     private void Awake(){
        anim = GetComponent<Animator>();
        //enemyPatrol: opcionális, ha a unity enginben a hierarchyában van ilyen objektum akkor járőrként fog viselkedni,
        //a parent objektum beállításai szerint, ha nem akkor egy helyben fog várakozni, és támad ha a player a támadási területre lép.
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update(){
        coolDownTimer += Time.deltaTime;
        if(PlayerInSight()){
            if(coolDownTimer >= attackCooldown){
                //Timer beállítása, és a támadás animáció indítása.
               
                anim.SetTrigger("shootAttack");
                coolDownTimer = 0;

            }
        }
        //Ez a rész csak akkor lép működésbe, ha az adott enemy egy járőr.
        //Arról gondoskodik, hogy a járőr megálljon, ha a player a támadási területen van, tehát az enemy "látja" a playert.
        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void rangedAttack(){
       
        fireballs[FindProjectile()].transform.position = firePoint.position;
        fireballs[FindProjectile()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindProjectile(){
        for(int i = 0; i < fireballs.Length; i++ ){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
    private bool PlayerInSight(){
        //raycastHit: információval tér vissza, ha a megadott területen lett-e detektálva objektum a player layerről.
        //Ha talált ilyet, akkor az adott objektummal tér vissza, illetve raycastHit.collider != null
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        //Akkor tér vissza igazzal, ha van a közelben player, és az adott player nem halott.
        return raycastHit.collider != null;
    }


    //Ez a függvény fejlesztést és tesztelést segíti, a támadási területet színezi az enginben pirosra.
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        
    }
}
