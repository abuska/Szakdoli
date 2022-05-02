using UnityEngine;

 //Ez az osztály felelős a kamera mozgásért
 
public class CameraController : MonoBehaviour{

    //Játékos követése távolsággal, megkönnyíti a játékos számára, a pálya áttekintését
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    
    //Játékos követése
    private Transform player;
    private PlayerManager playerManager;
    private float lookAhead;

    private void Awake(){
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void FollowPlayerCamera(){

        player = playerManager.getActivePlayerTransform();

        transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z);

        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance*player.localScale.x), Time.deltaTime * cameraSpeed );

    }

}

