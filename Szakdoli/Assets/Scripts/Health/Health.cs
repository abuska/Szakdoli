using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth{get; private set; }
    private Animator anim;
    public bool dead {get; private set; }

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake(){
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _demage){
        currentHealth = Mathf.Clamp(currentHealth - _demage, 0, startingHealth);
        if(currentHealth>0){
            anim.SetTrigger("hurt");
            StartCoroutine(Invonerability());
        }else{
            if(!dead){
                anim.SetTrigger("die");

                if(GetComponent<PlayerMovement>() != null){
                    GetComponent<PlayerMovement>().enabled = false;
                }

                if(GetComponentInParent<EnemyPatrol>() != null){
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponent<MeleeEnemy>() != null){
                    GetComponent<MeleeEnemy>().enabled = false;
                }

                dead = true;
            }
          
        }
    }

    public void AddHelath(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invonerability(){
        Physics2D.IgnoreLayerCollision(10,11,true);
        for(int i=0;i<numberOfFlashes; i++){
            spriteRend.color = new Color(255,0,0,0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
        }
         Physics2D.IgnoreLayerCollision(10,11,false);
    }


}
