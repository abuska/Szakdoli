using UnityEngine;

public class Health : MonoBehaviour{
    [SerializeField] private float startingHealth;
    public float currentHealth{get; private set; }
    
    private void Awake(){
        currentHealth = startingHealth;
    }

    public void TakeDemage(float _demage){
        currentHealth = Mathf.Clamp(currentHealth - _demage, 0, startingHealth);
        if(currentHealth>0){

        }else{

        }
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDemage(1);
        }
    }

}
