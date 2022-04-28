using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject completeLevelUI;
    [SerializeField] public GameObject completeAllLevelUI;
    [SerializeField] public GameObject gameOverUI;
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    private float mainMenuTimer = Mathf.Infinity;
    private float mainMenuTime = 1f;

    private bool isPause = false;



    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && mainMenuTimer > mainMenuTime ){
            if(!isPause){
                PauseGame();
            }
        }
        mainMenuTimer +=Time.deltaTime;
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