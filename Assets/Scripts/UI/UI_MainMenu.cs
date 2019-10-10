using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;


	private void OnEnable()
	{
		GameManager.instance.onPlay += StopSound;
	}

	private void OnDisable()
	{
		GameManager.instance.onPlay -= StopSound;
	}

	public void Menu() {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void Credits() {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

	private void StopSound()
	{
		print("STOP");
		GetComponent<AkAmbient>().Stop(0);
	}
}
