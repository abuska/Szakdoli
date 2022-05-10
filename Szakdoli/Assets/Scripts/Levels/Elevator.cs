using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header ("End Points")]
    [SerializeField] private Transform edgeUp; //Felsőpt
    [SerializeField] private Transform edgeDown; //Alsópt

    [Header ("Elevator")]
    [SerializeField] private Transform elevator;
    [SerializeField] private Transform elevatorGround;
    [SerializeField] private BoxCollider2D elevatorCollider; 
    [SerializeField] private LayerMask playerLayer;

    [Header ("Movement parameters")]
    [SerializeField] private float speed;
    private bool moveUp;
    private bool isMove;

    private bool activePlayerInElevator;

    private float changeDirectionTimer = Mathf.Infinity;

    private PlayerManager playerManager;

    private void Awake(){ 
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void FixedUpdate()
    {
        if(isPlayerInElevator() && Input.GetKey(KeyCode.E)){
            isMove = true;
        }
        if(isMove){
            Debug.Log("IsMove");
            if(Input.GetKey(KeyCode.E) && changeDirectionTimer > 0.5 && isPlayerInElevator()){
                ChangeDirection();
                stopMove();
            }

            if(moveUp){
                if(elevator.position.y <= edgeUp.position.y){
                    MoveInDirection(1);
                }else{
                    ChangeDirection();
                    stopMove();
                }
            //ha lefelé megy
            }else{
                if(elevator.position.y >= edgeDown.position.y){
                    MoveInDirection(-1);
                }else{
                    ChangeDirection();
                    stopMove();
                }
            }
        }
        changeDirectionTimer +=Time.deltaTime;
            
       

    }
    //Irányváltás
    private void ChangeDirection(){
            changeDirectionTimer = 0;
            moveUp = !moveUp;  
    }

    private void stopMove(){
        isMove = false;
    }

    //Mozgás
    private void MoveInDirection(int _direction){
        //Elevator mozgatása a megfelelő irányba
        elevator.position = new Vector3(elevator.position.x , elevator.position.y + Time.deltaTime *_direction * speed, elevator.position.z);
        elevatorGround.position = new Vector3(elevator.position.x , elevator.position.y + Time.deltaTime *_direction * speed, elevator.position.z);
    }
    private bool isPlayerInElevator(){
        //visszaadja az összes elemet ami ütközik a elevator colliderével, a playerLayer paraméter miatt szűkítve adja vissza a playerekre
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll( 
            new Vector2(elevatorCollider.bounds.center.x-elevatorCollider.bounds.size.x/2, elevatorCollider.bounds.center.y-elevatorCollider.bounds.size.y/2),
            new Vector2(elevatorCollider.bounds.size.x, elevatorCollider.bounds.size.y),
            elevatorCollider.bounds.size.x, 
            playerLayer
        );
        bool isActivePlayerInElevator = false;
        for(int i=0;i<raycastHit.Length;i++){
            Debug.Log(raycastHit[i].collider.name);
            //ellenőrzi, hogy az aktív charakter is a liften található-e 
            if(raycastHit[i].collider.name==playerManager.getActivePlayerName()){
                isActivePlayerInElevator=true;
                break;
            }
        }
        //akkor tér vissza igazzal, ha az aktív player a liften tartózkodik.
        return isActivePlayerInElevator;
    }
}
