using UnityEngine;

public class Health : MonoBehaviour{
    [SerializeField] private float startingHealth;
    public float currentHealth{get; private set; }
    private Animator anim;
    private bool dead;
    
    private void Awake(){
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _demage){
        currentHealth = Mathf.Clamp(currentHealth - _demage, 0, startingHealth);
        if(currentHealth>0){
            anim.SetTrigger("hurt");
        }else{
            if(!dead){
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
          
        }
    }
    public void AddHelath(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
