using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_GameMenu : MonoBehaviour
{
	public GameObject menu;
	public GameObject versus;
	public GameObject end;
    public GameObject score;
    public Text[] scores;
	public Button resumButton;
    public Button quitButton;


	private void OnEnable()
	{
        GameManager.instance.onService += OnService;
        GameManager.instance.onPause += OnPause;
        GameManager.instance.onEnd += OnEnd;
        resumButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
        GameManager.instance.onPlay += StopSound;
    }


	private void OnDisable()
	{
		GameManager.instance.onService -= OnService;
		GameManager.instance.onPause -= OnPause;
		resumButton.onClick.RemoveListener(Resume);
        quitButton.onClick.RemoveListener(Quit);
        GameManager.instance.onPlay -= StopSound;
    }

    private void Update() {
        for (int i = 0; i < scores.Length; i++) {
            scores[i].text = GameManager.instance.scores[i].ToString();
        }
    }

    private void OnService(int playerIndex)
	{
        score.SetActive(true);
		SetVersus(false);
	}

	private void OnPause(bool isPause)
	{
		SetMenu(isPause);
	}

    private void OnEnd() {
        score.SetActive(false);
        SetEnd(true);
    }

    private void SetEnd(bool b) {
        end.SetActive(b);
    }

	private void SetVersus(bool b)
	{
		versus.SetActive(b);
	}

	private void SetMenu(bool b)
	{
		menu.SetActive(b);
	}

	private void Resume()
	{
        GameManager.instance.PauseResume();
		SetMenu(false);
	}

    private void Quit() {
        GameManager.instance.LoadScene(0);
    }

    private void StopSound() {
        print("STOP");
        GetComponent<AkAmbient>().Stop(0);
    }
}
