
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header ("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool moveLeft;

    [Header ("Idle settings")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header ("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake(){
        initScale = enemy.localScale;
    }

    private void OnDisable(){
         anim.SetBool("isMove", false);
    }

    private void Update(){
        Debug.Log(moveLeft);
        if(moveLeft){
            if(enemy.position.x >=leftEdge.position.x){
                MoveInDirection(-1);
            }else{
                ChangeDirection();
            }
          
        }else{
            if(enemy.position.x <=rightEdge.position.x){
                MoveInDirection(1);
            }else{
                ChangeDirection();
            }
        }
      
    }

    private void ChangeDirection(){
        anim.SetBool("isMove", false);
        idleTimer +=Time.deltaTime;

        if(idleTimer > idleDuration){
            moveLeft = !moveLeft;
        }
       
    }

    private void MoveInDirection(int _direction){
        idleTimer = 0;
        anim.SetBool("isMove", true);
        //Flip enemy 
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //Move to direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime *_direction * speed, enemy.position.y, enemy.position.z);

    }
}
