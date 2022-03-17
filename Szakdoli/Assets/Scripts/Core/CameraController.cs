using UnityEngine;

 //Ez az osztály felelős a kamera mozgásért
 
public class CameraController : MonoBehaviour{

    //Szoba kamera nézethez
    [SerializeField] private float speed;  
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    //Játékos követése
    private Transform player;
    [SerializeField] private PlayerManager playerManager;
    //Játékos követése távolsággal, megkönnyíti a játékos számára, a pálya áttekintését
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;

    private float lookAhead;
    //Todo: player létra használata közben is legyen egy kis vertikális követési távolság, a jobb játékélmény miatt
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

        transform.position = new Vector3(player.position.x + lookAhead, player.position.y+3, transform.position.z);

        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance*player.localScale.x), Time.deltaTime * cameraSpeed );

    }

}

