using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public Button playButton;


    private void OnEnable()
	{
		GameManager.instance.onPlay += StopSound;
        playButton.onClick.AddListener(PlayEvent);
	}

	private void OnDisable()
	{
		GameManager.instance.onPlay -= StopSound;
        playButton.onClick.RemoveListener(PlayEvent);
    }

	public void Menu() {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void Credits() {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    private void PlayEvent() {
        GameManager.instance.LoadScene(1);
    }

	private void StopSound()
	{
		print("STOP");
		GetComponent<AkAmbient>().Stop(0);
	}
}
