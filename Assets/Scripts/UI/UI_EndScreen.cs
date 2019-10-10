using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndScreen : MonoBehaviour
{
    public Text txtP1;
    public Text txtP2;

    private void OnEnable() {
        GameManager.instance.onEnd += SetScores;
    }

    private void OnDisable() {
        GameManager.instance.onEnd -= SetScores;
    }

    private void SetScores()
    {
        txtP1.text = GameManager.instance.scores[0].ToString();
        txtP2.text = GameManager.instance.scores[1].ToString();
    }
}
