using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public GameObject CompleteLevelUI;

    public GameObject CompleteAllLevelUI;

    public void Restart (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel(){
        if(SceneManager.sceneCountInBuildSettings-1==SceneManager.GetActiveScene().buildIndex){
            CompleteAllLevelUI.SetActive(true);
        }else{
            CompleteLevelUI.SetActive(true);
        }
      
    }

    public void GameOver (){
        if(gameHasEnded == false){
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
   
    }


}