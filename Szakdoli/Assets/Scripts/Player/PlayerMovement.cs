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
    private float runTime = 3f;

    private float OlafShieldUpFallingSpeed = -1f;

    private float horizontalInput;
    private float verticalInput;
  


    private void Awake(){   
        body = GetComponent<Rigidbody2D>();
        gravityScale = body.gravityScale;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate(){
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            FlipPlayerImage();
            Move();
            
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
            
            HandleOlafFall();
            
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
        if(canRun() && Input.GetMouseButton(0) && runTimer > runTime){
            anim.SetBool("run", true);
            runTimer = 0;
        }
        if((!isOnWall() 
                && isGrounded()) || 
                (playerName=="Olaf" && anim.GetBool("isShieldUp") || 
                (playerName=="Erik" && !isOnWall())) 
            ){
                if(isRun()){
                    body.velocity = new Vector2(horizontalInput * runSpeed, body.velocity.y);
                }else{
                    body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
                }
        }
    }



    public void SetShield(){
        if(shieldUpTimer > shieldUpTime){
            anim.SetBool("isShieldUp", !anim.GetBool("isShieldUp"));
            shieldUpTimer = 0;
        } 
    }

    private void Climb(){
        if(isOnLadder() && Mathf.Abs(verticalInput) > 0 && !anim.GetBool("onLadder")){
            //elkezd mászni
            anim.SetBool("onLadder", true);
            RaycastHit2D ladder = Physics2D.BoxCast(
                boxCollider.bounds.center, 
                boxCollider.bounds.size, 
                0, 
                Vector2.down, 
                0.1f, 
                ladderLayer
            );

            transform.position = new Vector3(
                ladder.transform.gameObject.transform.position.x, 
                transform.position.y, 
                transform.position.z
            );
            if(playerName=="Olaf" && anim.GetBool("isShieldUp")){
                SetShield();
            }
            setGravityScale(0);
            
            body.velocity = new Vector2(body.velocity.x, 0);
        }else if(isOnLadder() && Mathf.Abs(verticalInput) > 0 && anim.GetBool("onLadder")){
            //ha mászik
            anim.SetBool("isClimb", true);
            body.velocity = new Vector2(0, verticalInput * speed);  
        }else if(isOnLadder() && Mathf.Abs(verticalInput) <= 0 && anim.GetBool("isClimb")){
            //ha megáll mászás közben
            anim.SetBool("isClimb", false);
            body.velocity = new Vector2(0, 0);
        }else if((Mathf.Abs(horizontalInput) > 0 && !isGrounded()) || !isOnLadder()){
            //ha leugrik a létráról
            setGravityScale(gravityScale);
            anim.SetBool("isClimb", false);
            anim.SetBool("onLadder", false);
        }else if(isGrounded() && isOnLadder()){
            //ha a létra mellett áll
            setGravityScale(0);
            body.velocity = new Vector2(body.velocity.x, 0);
            anim.SetBool("isClimb", false);
            anim.SetBool("onLadder", false);
        }
    
    }

    private void HandleOlafFall(){
        if(!isGrounded() && !isOnLadder() && playerName=="Olaf" && anim.GetBool("isShieldUp") ){
            body.velocity = new Vector2(body.velocity.x, OlafShieldUpFallingSpeed);  
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

   public void setGravityScale(float value){
        body.gravityScale = value;
    }
    public void setVelocity(){
        body.velocity = new Vector2(0, 0);
    }

    public void setRunFasle(){
        if(playerName=="Erik"){
            anim.SetBool("run", false);
        }
    }

    public string getPlayerName(){
        return playerName;
    }
    
    private bool isWalk(){
        return horizontalInput !=0 /*&& !isOnWall()*/;
    }
    
    private bool canRun(){
        return anim.GetBool("walk") == true && playerName=="Erik";
    }
    
    private bool isRun(){
        return playerName=="Erik" && anim.GetBool("run");
    }
    
    private bool isGrounded(){
        groundRememberTimer += Time.deltaTime;

        RaycastHit2D raycastHitGround = Physics2D.BoxCast(
            boxCollider.bounds.center, 
            boxCollider.bounds.size, 
            0, 
            Vector2.down, 
            0.1f, 
            groundLayer
        );

        RaycastHit2D raycastHitShield = Physics2D.BoxCast(
            boxCollider.bounds.center, 
            boxCollider.bounds.size, 
            0, Vector2.down, 
            0.1f, 
            shieldLayer
        );

        bool hit = raycastHitGround.collider != null || raycastHitShield.collider != null;

        if(hit){
            groundRememberTimer = 0;
        }
        return hit || groundRememberTimer < groundRememberTime;
    }

    private bool isOnLadder(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center, 
            boxCollider.bounds.size, 0, 
            Vector2.down, 
            0.1f, 
            ladderLayer
        );
        return raycastHit.collider != null;

    }

    private bool isOnWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center, 
            boxCollider.bounds.size, 0, 
            new Vector2(transform.localScale.x, 0), 
            0.1f, 
            wallLayer
        );
        return raycastHit.collider != null;
    }

    public bool canAttack(){
        return isGrounded() && !isOnWall() && playerName=="Baleog";
    }

    public bool canJump(){
        return isGrounded();
    }
    
}
