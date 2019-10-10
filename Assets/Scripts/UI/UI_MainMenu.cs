using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject splash;
    public Button playButton;
	public Button quitButton;


    private void OnEnable()
	{
		GameManager.instance.onPlay += StopSound;
        playButton.onClick.AddListener(PlayEvent);
		quitButton.onClick.AddListener(Quit);
        splash.SetActive(true);
        StartCoroutine(Splash());
	}

    private void OnDisable() {
        GameManager.instance.onPlay -= StopSound;
        playButton.onClick.RemoveListener(PlayEvent);
		quitButton.onClick.RemoveListener(Quit);
	}


    private IEnumerator Splash() {
        yield return new WaitForSeconds(4.6f);
        splash.SetActive(false);
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
