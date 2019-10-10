using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public Button playButton;
	public Button quitButton;


    private void OnEnable()
	{
		GameManager.instance.onPlay += StopSound;
        playButton.onClick.AddListener(PlayEvent);
		quitButton.onClick.AddListener(Quit);
	}

	private void OnDisable()
	{
		GameManager.instance.onPlay -= StopSound;
        playButton.onClick.RemoveListener(PlayEvent);
		quitButton.onClick.RemoveListener(Quit);
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

	private void Quit()
	{
		GameManager.instance.QuitGame();
	}

	private void StopSound()
	{
		print("STOP");
		GetComponent<AkAmbient>().Stop(0);
	}
}
