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

    private float cameraLimitUp;
    private float cameraLimitDown;
    private float cameraLimitLeft;
    private float cameraLimitRight;

    private void Awake(){
        playerManager = FindObjectOfType<PlayerManager>();
        
        cameraLimitUp = this.transform.GetChild(0).gameObject.GetComponent<Transform>().position.y;
        cameraLimitDown = this.transform.GetChild(1).gameObject.GetComponent<Transform>().position.y;
        cameraLimitLeft = this.transform.GetChild(2).gameObject.GetComponent<Transform>().position.x;
        cameraLimitRight = this.transform.GetChild(3).gameObject.GetComponent<Transform>().position.x;
    }

    public void FollowPlayerCamera(){

        player = playerManager.getActivePlayerTransform();

        transform.position = new Vector3(
            Mathf.Clamp(player.position.x + lookAhead, cameraLimitLeft, cameraLimitRight), 
            Mathf.Clamp(player.position.y, cameraLimitDown, cameraLimitUp), 
            transform.position.z
        );

        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance*player.localScale.x), Time.deltaTime * cameraSpeed );

    }

}

