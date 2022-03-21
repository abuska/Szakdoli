using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject completeLevelUI;
    [SerializeField] public GameObject completeAllLevelUI;
    [SerializeField] public GameObject gameOverUI;
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    

  

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


}