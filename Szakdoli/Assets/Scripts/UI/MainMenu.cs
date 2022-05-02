
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake(){
        gameManager = FindObjectOfType<GameManager>();
    }
    public void StartGame(){
        SceneManager.LoadScene(5);
    }
    public void ContinueGame(){
        gameManager.ContinueGame();
    }
    public void RestartLevel(){
        gameManager.Restart();
        gameManager.ContinueGame();

    }
    public void QuitGame(){
        Application.Quit();
    }
    public void goToCreditals(){
        SceneManager.LoadScene(4);
    }
    public void goToControls(){
        SceneManager.LoadScene(3);
    }
    public void goToMainManu(){
        SceneManager.LoadScene(0);
    }

    public void goToPassword(){
        SceneManager.LoadScene(2);
        gameManager.ContinueGame();
    }
}
