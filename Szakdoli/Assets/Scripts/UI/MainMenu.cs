
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene(2);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void goToCreditals(){
        SceneManager.LoadScene(1);
    }
    public void goToMainManu(){
        SceneManager.LoadScene(0);
    }
}
