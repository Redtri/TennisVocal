using UnityEngine;
using System.Collections;

public class UI_GameMenu : MonoBehaviour
{
	public GameObject menu;
	public GameObject versus;

	private void OnEnable()
	{
		FindObjectOfType<GameManager>().onService += OnService;
		FindObjectOfType<GameManager>().onPause += OnPause;
	}


	private void OnDisable()
	{
		GameManager.instance.onService -= OnService;
		GameManager.instance.onPause -= OnPause;

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
		Debug.Log("menu");
		menu.SetActive(b);
	}
}
