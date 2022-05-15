using System.Collections;
using UnityEngine;

// Ez az osztály felelős az életpontok nyilvántartásáért, mind az enemyk mind a playerek esetén.
public class Health : MonoBehaviour{


    //startingHealth: kezdő életerő
    //currentHealth: jelenlegi életerő, {get; private set; } ez a része gondoskodik arról, 
    //hogy public gettere legyen, de private maradjon a setter, azaz más osztály ne tudja módosítani az értékét.
    //anim: az animátor ami a sebzés és a halál animációt fogja kezelni.
    //dead: értéke igaz, ha már meghalt a karakter.
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth{get; private set; }
    private Animator anim;
    public bool dead {get; private set; }


    //Rengeteg játékban használják az iFrames technikát. 
    //Minden sebzés után van egy kicsi időablak amíg az adott karakter nem sebződhet meg újra.
    //Erre a játékélmény növelése miatt van szükség.
    //iFramesDuration: A "sebezhetetlenség" időtartama.
    //numberOfFlashes: A játékélmény szempontjából fontos, hogy folyamatos és azonnali vizuális visszajelzéseket
    //kapjon a játékos a játék jelenlegi állapotáról. Sebzés után a karakter a sebezhetetlenség időtartama alatt
    //a paraméterben megadott számszor fog pirosan villanni. 
    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake(){
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    //Sebzés
    public void TakeDamage(float demage){
        currentHealth = Mathf.Clamp(currentHealth - demage, 0, startingHealth);
       
        if( currentHealth > 0 ){

            //ha a karekter nem hal meg a sebzéstől elindul a sérülés animáció, 
            //illetve elindul az iFrame működése

            anim.SetTrigger("hurt");
            
            handleOlafDamage();
            handleBaleogDamage();
            
            StartCoroutine(Invonerability());

        
        }else if(!dead){

            //Abban az esetben ha a karakternek már nincs elég életereje.
            //és csak abban az esetben ha a karakter még nem halt meg
            //elindul a halál animáció
            anim.SetTrigger("die");

            handleDie();

            dead = true;
            
        }
    }


    private void handleDie(){
        //Letiltjuk a mozgatását, ha a karakter player
        if(GetComponent<PlayerMovement>() != null){
            GetComponent<PlayerMovement>().enabled = false;
            //deaktiváljuk a karaktert
            gameObject.SetActive(false);
            //Ha az aktív karakter halt meg karaktert vált.
            if(GetComponentInParent<PlayerManager>().getActivePlayer() == gameObject){
                GetComponentInParent<PlayerManager>().ChangePlayer();
            }    
        }
        //Ha a karakter járőr
        if(GetComponentInParent<EnemyPatrol>() != null){
            GetComponentInParent<EnemyPatrol>().enabled = false;
            gameObject.SetActive(false);
        }
        //Ha a karakter közel harcos
        if(GetComponent<MeleeEnemy>() != null){
            GetComponent<MeleeEnemy>().enabled = false;
            gameObject.SetActive(false);
        }
        //Ha a karaker távolsági harcos
        if(GetComponent<RangedEnemy>() != null){
            GetComponent<RangedEnemy>().enabled = false;
            gameObject.SetActive(false);
        }
    }
     
    private void handleOlafDamage(){
        //Ha Olaf pajzsa fel van emelve és megsérül huzza vissza maga elé
        if(GetComponent<PlayerMovement>().getPlayerName()=="Olaf" 
            && anim.GetBool("isShieldUp")!=null 
            && anim.GetBool("isShieldUp")==true
        ){
            GetComponent<PlayerMovement>().SetShield();
        }
    }

    private void handleBaleogDamage(){
        //Ha Baleogot sebzés éri, csapjon vissza egyet a kardjával
        if(GetComponent<PlayerMovement>().getPlayerName()=="Baleog"){
            GetComponent<PlayerAttack>().Attack();
        }
    }

    //Health növelése, pl felszedhető életerő pontokkal
    public void AddHelath(float value){
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    public void IncreaseHelath(float value){
        startingHealth =  Mathf.Clamp(startingHealth + value, 0, 10);
        currentHealth = Mathf.Clamp(currentHealth + value, 0, 10);
    }

    //A sérülés után a sebezhetetlenségért felelős fv.
    private IEnumerator Invonerability(){
        Physics2D.IgnoreLayerCollision(10,11,true);
        for(int i=0;i<numberOfFlashes; i++){
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
        }
         Physics2D.IgnoreLayerCollision(10,11,false);
    }


}
