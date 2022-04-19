using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Password : MonoBehaviour
{
    [SerializeField]private Button button;

    [SerializeField]private InputField input;

    [SerializeField]private Text warningText;
 
    Dictionary<string, string> levelPassword = new Dictionary<string, string>();

    private void createLevelDictionary(){
        levelPassword.Add("aaaa","Level1");
        levelPassword.Add("bbbb","Level2");
        levelPassword.Add("cccc","Level3");
        levelPassword.Add("dddd","Level4");
        levelPassword.Add("eeee","Level5");
    }

    private void Start(){
        createLevelDictionary();
        button.onClick.AddListener(checkPassword);
    }

    private string CheckLimit (){
        if(input.text.Length > 4){
            return input.text.Substring (0, 4);
        }else{
            return input.text;
        }
    }
    private void setButtonInteractable(){
        if(input.text.Length==4){
            button.interactable = true;
        }else{
            button.interactable = false;
            warningText.enabled = false;
        }
    }

    void Update () {
        input.text = CheckLimit ();
        setButtonInteractable();      
    }
        
    public void checkPassword(){
        Debug.Log("Hey im pw"+ input.text);
        if(levelPassword.TryGetValue(input.text, out string value)){
            Debug.Log(levelPassword[input.text]);
            SceneManager.LoadScene(levelPassword[input.text]);
        }else{
           warningText.enabled = true;
        }
    }
}
