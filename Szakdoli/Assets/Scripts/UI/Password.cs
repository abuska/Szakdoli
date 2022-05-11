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
        levelPassword.Add("TREL","Level1");
        levelPassword.Add("ZVVE","Level2");
        levelPassword.Add("QKDY","Level3");
        levelPassword.Add("CEDL","Level4");
        levelPassword.Add("SKYQ","Level5");
        levelPassword.Add("AIXY","Level6");
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
        string str = input.text.ToUpper();
        if(levelPassword.TryGetValue(str, out string value)){
            SceneManager.LoadScene(levelPassword[str]);
        }else{
           warningText.enabled = true;
        }
    }
}
