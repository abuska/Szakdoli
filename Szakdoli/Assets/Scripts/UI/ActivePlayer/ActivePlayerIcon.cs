using UnityEngine;
using UnityEngine.UI;

//A UI képernyőn megjelenő active player megjelölést irányítja
public class ActivePlayerIcon : MonoBehaviour
{

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private string playerName;
    [SerializeField] private Image activeIcon;


    //Beállítja az aktív player jelenlegi státuszát az alapján hogy ez a player-e az aktív player.
    private void Update(){
       activeIcon.enabled = playerManager.getActivePlayerName() == playerName; 
    }
}