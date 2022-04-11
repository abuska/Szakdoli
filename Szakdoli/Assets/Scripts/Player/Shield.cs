using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Animator anim;
  
    [SerializeField] private BoxCollider2D boxCollider;

    private bool isShieldUp;

    void Start(){
        anim = GetComponentInParent<Animator>();

    }
    void Update()
    {
        if(anim.GetBool("isShieldUp") && !isShieldUp){
          
            
            isShieldUp = true;
            boxCollider.usedByEffector = true;
            Physics2D.IgnoreLayerCollision( 10, 17, false);
        }else if(!anim.GetBool("isShieldUp") && isShieldUp){
            isShieldUp = false;
            boxCollider.usedByEffector = false;
            Physics2D.IgnoreLayerCollision( 10, 17, true);
        }
        
    }
}
