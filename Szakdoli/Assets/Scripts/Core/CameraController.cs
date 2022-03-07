using UnityEngine;

public class CameraController : MonoBehaviour{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    //Follow player
    private Transform player;
    [SerializeField] private PlayerManager playerManager;
    //Follow player with distance
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    private float lookAhead;
    private float lookVerticalAhead;

    private void Update(){
       FollowPlayerCamera();
    }

    public void MoveToNewRoom(Transform _newRoom){
        currentPosX = _newRoom.position.x;
    }

    private void RoomCamera(){
         transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
    }

    private void FollowPlayerCamera(){
        player = playerManager.getActivePlayerTransform();

        Debug.Log(transform.position.y);
        Debug.Log(player.position.y);

        transform.position = new Vector3(player.position.x + lookAhead, player.position.y+3, transform.position.z);

        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance*player.localScale.x), Time.deltaTime * cameraSpeed );

    }

    public float getVerticalCameraDistance(){
        if(Mathf.Abs(player.position.y - transform.position.y ) < 3){
            return player.position.y - transform.position.y;
        }else{  
           return 3;
        }
    }
}

