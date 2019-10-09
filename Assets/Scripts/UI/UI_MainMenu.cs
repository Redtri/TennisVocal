using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;

    public void Menu() {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void Credits() {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
}
