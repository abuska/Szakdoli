using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]public GameObject completeLevelUI;
    [SerializeField]public GameObject completeAllLevelUI;
    [SerializeField]public GameObject gameOverUI;

    public bool gameHasEnded = false;
    public float restartDelay = 1f;

    public PlayerManager playerManager;

    private float mainMenuTimer = Mathf.Infinity;
    private float mainMenuTime = 1f;
    private bool isPause;

    private void Awake(){
        playerManager = FindObjectOfType<PlayerManager>();
        Time.timeScale = 1;
        isPause = false;
    }

    private void Update()
    {
        if(isGameOver()){
            GameOver();
        }else{
            if(Input.GetKey(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0 && mainMenuTimer > mainMenuTime ){
                if(!isPause){
                    PauseGame();
                }
            }
            mainMenuTimer +=Time.deltaTime;
        }
    }

    public void PauseGame(){
        Time.timeScale = 0;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        mainMenuTimer = 0;
        isPause = true;
    }

    public void ContinueGame(){
        SceneManager.UnloadSceneAsync(1);
        Time.timeScale = 1;
        mainMenuTimer = 0;
        isPause = false;
    }

    public void Restart (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel(){
        if(SceneManager.sceneCountInBuildSettings-1==SceneManager.GetActiveScene().buildIndex){
            completeAllLevelUI.SetActive(true);
        }else{
            completeLevelUI.SetActive(true);
        }
      
    }
    private bool isGameOver(){
        return playerManager.getPlayerNumber() == playerManager.getDeadPlayerNumber();
    }

    public void GameOver (){
        if(gameHasEnded == false){
            gameHasEnded = true;
            gameOverUI.SetActive(true);
        }
   
    }
    public void goToMainMenu(){
         SceneManager.LoadScene(0);
    }


}