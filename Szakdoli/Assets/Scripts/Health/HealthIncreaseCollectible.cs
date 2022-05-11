using UnityEngine;

//Összegyüjthető életerőpontok
public class HealthIncreaseCollectible : MonoBehaviour{

    //healthValue: mennyi életpontot ad.
    [SerializeField] private float healthValue; 

    private void OnTriggerEnter2D(Collider2D collision){
        //Csak abban az esetben aktiválódjon, ha ez az objektum a player taget viseli.
        if (collision.tag == "Player"){
            //Meghívjuk az adott player IncreaceHelath függvényét, 
            //amely hozzáadja a player életéhez a megfelelő pontszámot
            collision.GetComponent<Health>().IncreaseHelath(healthValue);
            //Deaktiváljuk az objektumot, így az "eltűnik"
            gameObject.SetActive(false);
        }
    }
}
