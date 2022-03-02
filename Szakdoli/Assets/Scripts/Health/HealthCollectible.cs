using UnityEngine;

public class HealthCollectible : MonoBehaviour{
    [SerializeField] private float healthVale; 

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){
            collision.GetComponent<Health>().AddHelath(healthVale);
            gameObject.SetActive(false);
        }
    }
}
