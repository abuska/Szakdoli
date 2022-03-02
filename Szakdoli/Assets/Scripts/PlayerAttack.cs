using UnityEngine;

public class PlayerAttack : MonoBehaviour{
  
    [SerializeField] private float attackCooldown;
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
    void Update()
    {
        if(Input.GetMouseButton(0) && coolDownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
        }
        coolDownTimer += Time.deltaTime;
        
    }

    private void Attack(){
        anim.SetTrigger("attack");
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
