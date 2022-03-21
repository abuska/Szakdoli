using UnityEngine;

//Ez az osztály valósítja meg a közelharci ellenségeket.
public class MeleeEnemy : MonoBehaviour
{

    //attackCooldown: Ezzel adhatjuk meg mennyi idő teljen el két támadás között. 
    //Természetellenes érzést, kelt ha az ellség folyamatossan, szünet nélkül támad.
    //Az animációval problémák léphetnek fel emelett a működés mellett, a folyamatos újrakezdése miatt szagattottnak tűnik az animáció stb.
    //Ez a megoldás olyan érzetet ad a játékosnak, mintha az ellenfél "fáradna" támadás közben, 
    //kis időre szüksége lenne "pihenni" a következő támadás előtt.  
    //range: paraméterrel tudjuk megadni, hogy mekkora legyen a támadási terület
    //damage: paraméterrel állítjuk be, hogy mekkorát sebezzen a karakter
    
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    //colliderDistance: paraméterrel tudjuk a játékos és támadási terület távolságát beállítani. 
    //boxCollider: a "támadási terület" ha a player a területén tartózkodik akkor sebzi a támadás
    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
   
    //playerLayer: az itt megkapott paraméterű layer objektumaira fog támadni az enemy, jelen esetbe ez a player layer.
    [Header ("Player Layer")]
   
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;

    //Refs
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
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
                coolDownTimer = 0;
                anim.SetTrigger("attack");
            }
        }
        //Ez a rész csak akkor lép működésbe, ha az adott enemy egy járőr.
        //Arról gondoskodik, hogy a járőr megálljon, ha a player a támadási területen van, tehát az enemy "látja" a playert.
        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
       
        
    }

    //Ez a rész dönti el, hogy az elleség "látja-e" a playert.
    private bool PlayerInSight(){
        //raycastHit: információval tér vissza, ha a megadott területen lett-e detektálva objektum a player layerről.
        //Ha talált ilyet, akkor az adott objektummal tér vissza, illetve raycastHit.collider != null
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        if(raycastHit.collider != null){
            //ha van player az enemy közelében akit megtámadhat, akkor itt eltároljuk a player Helath-jét. Ez az osztály felelős
            //a player életerejének a nyilvántartásáért, illetve a "sérülésekért".
            playerHealth = raycastHit.transform.GetComponent<Health>();
        }
        //Akkor tér vissza igazzal, ha van a közelben player, és az adott player nem halott.
        return raycastHit.collider != null && playerHealth.currentHealth>0;
    }


    //Ez a függvény fejlesztést és tesztelést segíti, a támadási területet színezi az enginben pirosra.
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        
    }

    //Ez valósítja meg a támadást, akkor ha a player a támadási területen tartózkodik még.
    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }  
    }


}
