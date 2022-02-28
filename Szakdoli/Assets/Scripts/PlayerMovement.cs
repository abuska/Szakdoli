using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;

    private void Awake()
    {   
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){
        horizontalInput = Input.GetAxis("Horizontal");

        Move();
        FlipPlayerImage();
        Jump();

        //Set animator
        anim.SetBool("walk", IsWalk());
        anim.SetBool("grounded", GetGrounded());
    }

    //MovementMethods
    private void Jump(){
         if(IsJumpEnabled()){
            body.velocity = new Vector2(body.velocity.x, speed);
            anim.SetTrigger("jump");
            SetGrounded(false);
        }
    }
    private void Move(){
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }
    private void FlipPlayerImage(){
        //Flip image according player move
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    //Collider
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground"){
            SetGrounded(true);
        }
        
    }

    //State Getters
    private bool GetGrounded(){
        return grounded;
    }
    //State Setters
    private void SetGrounded(bool param){
        grounded = param;
    }

    //Anim conditions
    private bool IsWalk(){
        return horizontalInput !=0;
    }
    private bool IsJumpEnabled(){
        return Input.GetKey(KeyCode.Space) && grounded;
    }
   
}
