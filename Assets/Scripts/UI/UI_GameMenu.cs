using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_GameMenu : MonoBehaviour
{
	public GameObject menu;
	public GameObject versus;
	public GameObject end;
	public Button resumButton;


	private void OnEnable()
	{
        GameManager.instance.onService += OnService;
        GameManager.instance.onPause += OnPause;
        GameManager.instance.onEnd += OnEnd;
        resumButton.onClick.AddListener(Resume);
	}


	private void OnDisable()
	{
		GameManager.instance.onService -= OnService;
		GameManager.instance.onPause -= OnPause;
		resumButton.onClick.RemoveListener(Resume);
	}

	private void OnService(int playerIndex)
	{
		SetVersus(false);
	}

	private void OnPause(bool isPause)
	{
		SetMenu(isPause);
	}

    private void OnEnd() {
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
		SetMenu(false);
	}
}
