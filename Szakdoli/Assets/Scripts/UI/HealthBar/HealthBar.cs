using UnityEngine;
using UnityEngine.UI;

//A UI képernyőn megjelenő health bart irányítja
public class HealthBar : MonoBehaviour
{

   //playerHealth: Az adott karakter Helath osztálya
   //totalHealthBar: A maximális életpontját jeleníti meg a karakternek
   //currentHealthBar: A jelenlegi élepontját jeleníti meg a karakternek.
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

   // induláskor a maximális érték szerint beállítja a maximális életerőt a UI-on.
    private void Start(){
       totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    //Beállítja az életerő jelenlegi státuszát a currentHealth értékealapján.
    private void Update(){
       currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
