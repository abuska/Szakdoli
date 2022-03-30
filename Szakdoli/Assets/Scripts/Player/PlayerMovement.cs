using UnityEngine;

//todo turn off move animation onWall

public class PlayerMovement : MonoBehaviour{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private string playerName;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask shieldLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask ladderLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    public float gravityScale{get; private set; }

    //Ha a karakter még pont ne ért le a földre, de a játékos már megnyomta az ugrás gombot akkor nem jön létre az ugrás,
    //a jobb játékélmény miatt kell ez a timer, hogy egy pár mp-ig tárolja az ugrás parancsot, így gördülékenyebbnek hat a játékmenet
    private float jumpRememberTime = 0.2f;
    private float jumpRememberTimer;

    //Ground timer a jump timerhez hasonlóan
    private float groundRememberTime = 0.1f;
    private float damageGroundTime = 5f;
    private float groundRememberTimer = Mathf.Infinity;

    private float horizontalInput;
    private float verticalInput;

    private float shieldUpTimer = Mathf.Infinity;


    private void Awake(){   
        body = GetComponent<Rigidbody2D>();
        gravityScale = body.gravityScale;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            FlipPlayerImage();
            
            if((!isOnWall() && isGrounded())){
                Move();
            }
            
            Climb();
            
            if(Input.GetKey(KeyCode.Space) ){
                jumpRememberTimer = jumpRememberTime;
                if(playerName=="Olaf"){
                    SetShield();
                }else if(playerName=="Erik"){
                    Jump();
                }      
            }
            Debug.Log(body.gravityScale);
            
            //Set animator
            anim.SetBool("walk", isWalk());
            anim.SetBool("grounded", isGrounded());

            shieldUpTimer +=Time.deltaTime;
            jumpRememberTimer -= Time.deltaTime;
    }
    
    //MovementMethods

    private void Jump(){
        //jump height is depend jumpPower and gravityScale
       
        if(canJump() && (jumpRememberTimer > 0)){
            jumpRememberTimer = 0;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");       
        }
    }
    private void Move(){

        //https://www.youtube.com/watch?v=vFsJIrm2btU
        //TODO

        /*float velocity = body.velocity.x;
        velocity += horizontalInput;
        velocity *= Mathf.Pow(1f - horizontalInput, Time.deltaTime * 10f);
        body.velocity = new Vector2(velocity, body.velocity.y);*/
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    public void SetShield(){
        if(shieldUpTimer > 0.5){
            anim.SetBool("isShieldUp", !anim.GetBool("isShieldUp"));
            shieldUpTimer = 0;
            if(anim.GetBool("isShieldUp")){
                body.gravityScale = gravityScale/3;
            }else{
                body.gravityScale = gravityScale;
            }
        } 
    }

    private void Climb(){
        if(onLadder() && Mathf.Abs(verticalInput) > 0 && !anim.GetBool("onLadder")){
            anim.SetBool("onLadder", true);
            body.gravityScale = 0;
            body.velocity = new Vector2(body.velocity.x, 0);
        }else if(onLadder() && Mathf.Abs(verticalInput) > 0 && anim.GetBool("onLadder")){
            anim.SetBool("isClimb", true);
            body.velocity = new Vector2(body.velocity.x, verticalInput * speed);  
        }else if(onLadder() && Mathf.Abs(verticalInput) <= 0 && anim.GetBool("isClimb")){
            anim.SetBool("isClimb", false);
            body.velocity = new Vector2(body.velocity.x, 0);
        }else if((Mathf.Abs(horizontalInput) > 0 && !isGrounded()) || !onLadder()){
            //TODO ezt szebben meg kéne oldani éééés a zuhanás még nem oké
           if(playerName=="Olaf" && anim.GetBool("isShieldUp")!=null && anim.GetBool("isShieldUp")){
               body.gravityScale = gravityScale/3;
            }else{
                body.gravityScale = gravityScale;
            }

            anim.SetBool("isClimb", false);
            anim.SetBool("onLadder", false);
        }else if(isGrounded() && onLadder()){
            body.gravityScale = 0;
            body.velocity = new Vector2(body.velocity.x, 0);
            anim.SetBool("isClimb", false);
            anim.SetBool("onLadder", false);
        }





        
    }
    private void FlipPlayerImage(){
        //Flip image according player move
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    //Anim conditions
    private bool isWalk(){
        return horizontalInput !=0;
    }
    private bool isGrounded(){
        groundRememberTimer += Time.deltaTime;

        RaycastHit2D raycastHitGround = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycastHitShield = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, shieldLayer);
        bool hit = raycastHitGround.collider != null || raycastHitShield.collider != null;

        if(hit){
            //TODO a nagy eséstől sérüljön a karakter;
            /*if(groundRememberTimer>damageGroundTime){
                GetComponent<Health>().TakeDamage(1);
            }*/
            groundRememberTimer = 0;
        }
        return hit || groundRememberTimer < groundRememberTime;
    }

    private bool onLadder(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, ladderLayer);
        return raycastHit.collider != null;

    }

    private bool isOnWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack(){
        return isGrounded() && !isOnWall() && playerName=="Baleog";
    }

    public bool canJump(){
        return isGrounded();
    }
    
    public void setGravityScale(float value){
        gravityScale = value;
        body.gravityScale = gravityScale;
    }

    public string getPlayerName(){
        return playerName;
    }
}
