using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndScreen : MonoBehaviour
{
    public Text txtP1;
    public Text txtP2;
    public Button quitButton;
    public GameObject layer;

    private void OnEnable() {
        GameManager.instance.onEnd += SetScores;
        quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable() {
        GameManager.instance.onEnd -= SetScores;
        quitButton.onClick.RemoveListener(Quit);
    }

    private void SetScores()
    {
        layer.SetActive(true);
        txtP1.text = GameManager.instance.scores[0].ToString();
        txtP2.text = GameManager.instance.scores[1].ToString();
    }

    private void Quit() {
        GameManager.instance.LoadScene(0);
    }
}
