using UnityEngine;

public class PlayerAttack : MonoBehaviour{
  
    [SerializeField] private float attackCooldown;
    [SerializeField] private LayerMask enemyLayer;

    [Header ("Melee Parameters")]
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D swordCollider;

    [Header ("Fire Parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;


    private Animator anim;
    private PlayerMovement playerMovement;
    private float coolDownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }
    void Update(){
        if(Input.GetMouseButton(0) && coolDownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
        }else if(Input.GetMouseButton(1) && coolDownTimer > attackCooldown && playerMovement.canAttack()){
            Fire();
        }
        coolDownTimer += Time.deltaTime;
    }

    public void Attack(){
       if(coolDownTimer >= attackCooldown){
                //Timer beállítása, és a támadás animáció indítása.
                coolDownTimer = 0;
                anim.SetTrigger("attack");
            }
    }
    private void TakeEnemyDamage(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(swordCollider.bounds.center + transform.right * transform.localScale.x,
        new Vector3(swordCollider.bounds.size.x, swordCollider.bounds.size.y, swordCollider.bounds.size.z), 0, Vector2.left, 0, enemyLayer);
        if(raycastHit.collider != null && raycastHit.collider.GetComponent<Health>() != null && raycastHit.collider.tag != "Player"){
            raycastHit.collider.GetComponent<Health>().TakeDamage(damage);
        }
    }


    private void Fire(){
        anim.SetTrigger("fire");
        coolDownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball(){
        for(int i = 0; i < fireballs.Length; i++ ){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
}
