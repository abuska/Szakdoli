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
    public void TakeDamage(float _demage){
        currentHealth = Mathf.Clamp(currentHealth - _demage, 0, startingHealth);
        //ha a karekter nem hal meg a sebzéstől elindul a sérülés animáció, illetve elindul az iFrame működése
        if(currentHealth>0){
            anim.SetTrigger("hurt");
            StartCoroutine(Invonerability());
        //Abban az esetben ha a karakternek már nincs elég életereje.
        }else{
            //csak abban az esetben ha a karakter még nem halt meg
            if(!dead){
                //elindul a halál animáció
                anim.SetTrigger("die");

                //És letiltjuk a mozgatását
                //Ha a karakter player
                if(GetComponent<PlayerMovement>() != null){
                    GetComponent<PlayerMovement>().enabled = false;
                    gameObject.SetActive(false);
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

                dead = true;
            }
          
        }
    }

    //Health növelése, pl felszedhető életerő pontokkal
    public void AddHelath(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //A sérülés után a sebezhetetlenségért felelős fv.
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
