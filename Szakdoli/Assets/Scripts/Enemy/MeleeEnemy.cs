using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    
    [Header ("Player Layer")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;

    //Refs
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update(){
        coolDownTimer += Time.deltaTime;

        if(PlayerInSight()){
            if(coolDownTimer >= attackCooldown){
                coolDownTimer = 0;
                anim.SetTrigger("attack");
            }
        }
        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
       
        
    }
    private bool PlayerInSight(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if(raycastHit.collider != null){
            playerHealth = raycastHit.transform.GetComponent<Health>();
        }
        
        return raycastHit.collider != null && playerHealth.currentHealth>0;
    }
    //Change boxCollider red in unity 
    private void OnDrawGizmos(){

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }  
    }


}
