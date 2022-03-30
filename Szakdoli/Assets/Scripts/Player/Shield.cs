using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Animator anim;
    private bool isShieldUp;

    void Start(){
         anim = GetComponentInParent<Animator>();

    }
    void Update()
    {
        if(anim.GetBool("isShieldUp") && !isShieldUp){
            isShieldUp = true;
            Physics2D.IgnoreLayerCollision( 10, 17, false);
        }else if(!anim.GetBool("isShieldUp") && isShieldUp){
            isShieldUp = false;
            Physics2D.IgnoreLayerCollision( 10, 17, true);
        }
        
    }
}
