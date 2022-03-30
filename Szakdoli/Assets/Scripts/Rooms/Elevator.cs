using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header ("End Points")]
    [SerializeField] private Transform edge1; //Felsőpt
    [SerializeField] private Transform edge2; //Alsópt

    //Itt adjuk meg paraméterként, hogy melyik enemy legyen a járőr
    [Header ("Elevator")]
    [SerializeField] private Transform elevator;
    [SerializeField] private BoxCollider2D elevatorCollider;
    [SerializeField] private LayerMask playerLayer;

    //speed: mozgási sebesség, 
    //initScale: ez tárolja hogy éppen merre "néz" a karakter, 
    //moveLeft: ez alapján mozog a kaater jobbra vagy balra
    [Header ("Movement parameters")]
    [SerializeField] private float speed;
    private bool moveEdge1;
    private bool isMove;

    private float changeDirectionTimer = Mathf.Infinity;

    void Update()
    {
        //TODO ÁTÍRNI HOGY MINDEN IRÁNYBA MŰKÖDJÖN
        if(isPlayerInElevator() && Input.GetKey(KeyCode.E)){
            isMove = true;
        }
        if(isMove){
            if(Input.GetKey(KeyCode.E) && changeDirectionTimer > 0.5){
                ChangeDirection();
            }
            if(moveEdge1){
                if(elevator.position.y <= edge1.position.y){
                    MoveInDirection(1);
                }else{
                    ChangeDirection();
                }
            //ha edge 2 fele megy
            }else{
                if(elevator.position.y >= edge2.position.y){
                    MoveInDirection(-1);
                }else{
                    ChangeDirection();
                }
            }
        }
        changeDirectionTimer +=Time.deltaTime;
            
       

    }
     //Irányváltás
    private void ChangeDirection(){
            changeDirectionTimer = 0;
            isMove = false;
            moveEdge1 = !moveEdge1;  
    }

    //Mozgás
    //TODO Meg kellene csinálni, hogy ne olyan szaggatottan mozogjon
    private void MoveInDirection(int _direction){
        //Elevator mozgatása a megfelelő irányba
        elevator.position = new Vector3(elevator.position.x , elevator.position.y + Time.deltaTime *_direction * speed, elevator.position.z);

    }
    private bool isPlayerInElevator(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(elevatorCollider.bounds.center, elevatorCollider.bounds.size, 0, Vector2.down, 0.1f, playerLayer);
        return raycastHit.collider != null;
    }
}
