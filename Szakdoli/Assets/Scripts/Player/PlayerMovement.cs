using UnityEngine;

//todo turn off move animation onWall

public class PlayerMovement : MonoBehaviour{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float runSpeed;
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
    private float shieldUpTimer = Mathf.Infinity;
    private float runTimer = Mathf.Infinity;

    private float shieldUpTime = 0.5f;
    private float runTime = 1.5f;

    private float OlafShieldUpFallingSpeed = -1f;

    private float horizontalInput;
    private float verticalInput;
  


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
            
            if((!isOnWall() /*&& isGrounded()*/)){
                Move();
            }
            
            //TODO kiszervezni a faltörést methbe
            if(anim.GetBool("walk") == true && playerName=="Erik" && Input.GetMouseButton(0) && runTimer > runTime ){

                //TODO itt van egy olyan hiba h touchpaddal nem működik

                body.velocity = new Vector2(horizontalInput * runSpeed, body.velocity.y);
                anim.SetTrigger("run");
                runTimer = 0;
            }

            if(Input.GetKey(KeyCode.Space) ){
                jumpRememberTimer = jumpRememberTime;
                if(playerName=="Olaf"){
                    SetShield();
                }else if(playerName=="Erik"){
                    Jump();
                }      
            }else{
                Climb();
            }
            
            if(!isGrounded() && !onLadder() && playerName=="Olaf" && anim.GetBool("isShieldUp") ){
                body.velocity = new Vector2(body.velocity.x, OlafShieldUpFallingSpeed);  
            }
            
            //Set animator
            anim.SetBool("walk", isWalk());
            anim.SetBool("grounded", isGrounded());

            shieldUpTimer += Time.deltaTime;
            jumpRememberTimer -= Time.deltaTime;
            runTimer += Time.deltaTime;
    }
    
    //MovementMethods

    private void Jump(){
        //jump height is depend jumpPower and gravityScale
       
        if(canJump() && (jumpRememberTimer > 0)){
            setGravityScale(gravityScale);
            jumpRememberTimer = 0;
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");       
        }
    }
    private void Move(){
      
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        //https://www.youtube.com/watch?v=vFsJIrm2btU
        //TODO

        /*float velocity = body.velocity.x;
        velocity += horizontalInput;
        velocity *= Mathf.Pow(1f - horizontalInput, Time.deltaTime * 10f);
        body.velocity = new Vector2(velocity, body.velocity.y);*/
       
    }

    public void SetShield(){
        if(shieldUpTimer > shieldUpTime){
            anim.SetBool("isShieldUp", !anim.GetBool("isShieldUp"));
            shieldUpTimer = 0;
        } 
    }

    private void Climb(){
        if(onLadder() && Mathf.Abs(verticalInput) > 0 && !anim.GetBool("onLadder")){
            anim.SetBool("onLadder", true);
            setGravityScale(0);
            body.velocity = new Vector2(body.velocity.x, 0);
        }else if(onLadder() && Mathf.Abs(verticalInput) > 0 && anim.GetBool("onLadder")){
            anim.SetBool("isClimb", true);
            body.velocity = new Vector2(body.velocity.x, verticalInput * speed);  
        }else if(onLadder() && Mathf.Abs(verticalInput) <= 0 && anim.GetBool("isClimb")){
            anim.SetBool("isClimb", false);
            body.velocity = new Vector2(body.velocity.x, 0);
        }else if((Mathf.Abs(horizontalInput) > 0 && !isGrounded()) || !onLadder()){
            setGravityScale(gravityScale);
            anim.SetBool("isClimb", false);
            anim.SetBool("onLadder", false);
        }else if(isGrounded() && onLadder()){
            setGravityScale(0);
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
        body.gravityScale = value;
    }

    public string getPlayerName(){
        return playerName;
    }
}
