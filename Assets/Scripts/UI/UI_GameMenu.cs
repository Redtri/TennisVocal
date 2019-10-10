using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_GameMenu : MonoBehaviour
{
	public GameObject menu;
	public GameObject versus;
	public Button resumButton;


	private void OnEnable()
	{
		FindObjectOfType<GameManager>().onService += OnService;
		FindObjectOfType<GameManager>().onPause += OnPause;
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
