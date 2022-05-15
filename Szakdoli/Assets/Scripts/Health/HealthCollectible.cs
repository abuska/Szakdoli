using UnityEngine;

//Összegyüjthető életerőpontok
public class HealthCollectible : MonoBehaviour{

    //healthValue: mennyi életpontot ad.
    [SerializeField] private float healthValue; 

    //ez a függvény a Unity egyik beépített függvénye, csak trigger típusú objektumoknál működik
    //akkor aktiválódik, ha valami "hozzáér" az objektum Collideréhez.
    private void OnTriggerStay2D(Collider2D collision){
        //Csak abban az esetben aktiválódjon, ha ez az objektum a player taget viseli.
        if (collision.tag == "Player" && Input.GetKey(KeyCode.E)){
            //Meghívjuk az adott player AddHelath függvényét, 
            //amely hozzáadja a player életéhez a megfelelő pontszámot
            collision.GetComponent<Health>().AddHelath(healthValue);
            //Deaktiváljuk az objektumot, így az "eltűnik"
            gameObject.SetActive(false);
        }
    }
}
