using UnityEngine;

//todo turn off move animation onWall

public class PlayerMovement : MonoBehaviour{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private string playerName;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask ladderLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    public float gravityScale{get; private set; }
    //private float wallJumpCooldown;

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

            if(!(isOnWall() && !isGrounded())){
                Move();
            }
            
            Climb();
            Jump(); 
            
            //Set animator
            anim.SetBool("walk", isWalk());
            anim.SetBool("grounded", isGrounded());
            Physics2D.IgnoreLayerCollision(10,10); //Többi játékossal való ütközést ignorálja
    }
    
    //MovementMethods

    private void Jump(){
        //jump height is depend jumpPower and gravityScale
         if(Input.GetKey(KeyCode.Space) && canJump()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");       
        }
    }

    private void Move(){
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    private void Climb(){
        if(canClimb()){
            body.gravityScale = 0;
            body.velocity = new Vector2(body.velocity.x, 0);
            if(Mathf.Abs(verticalInput) > 0){
                body.velocity = new Vector2(body.velocity.x, verticalInput * speed);
            } 
        }else{
            body.gravityScale = gravityScale;
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool canClimb(){
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
        return isGrounded() && playerName=="Erik";
    }
    
    public void setGravityScale(float value){
        gravityScale = value;
        body.gravityScale = gravityScale;
    }


/////////////////////////////////////////////////////////////////////////////

/*
    private void JumpWithWallJump(){
        //jump height is depend jumpPower and gravityScale
         if(Input.GetKey(KeyCode.Space)){
            if(isGrounded()){
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                anim.SetTrigger("jump");
            }else if (isOnWall() && !isGrounded()){
                if(horizontalInput == 0){
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }else{
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                }
                wallJumpCooldown = 0;
                
            }
           
        }
    }
    private void UpdateWithWallJump(){
        horizontalInput = Input.GetAxis("Horizontal");
        FlipPlayerImage();

        if(wallJumpCooldown > 0.2f){
            
            Move();
            if(isOnWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }else{
                body.gravityScale = gravityScale;
            }
            Jump(); 
        }else{
            wallJumpCooldown += Time.deltaTime;
        }
        
        //Set animator
        anim.SetBool("walk", isWalk());
        anim.SetBool("grounded", isGrounded());
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground"){
            setGrounded(true);
        }
        
    }
    private void setGrounded(bool param){
        grounded = param;
    }
    private bool isJumpEnabled(){
        return Input.GetKey(KeyCode.Space) && isGrounded();
    }
*/
}
