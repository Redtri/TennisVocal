using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    private bool isPaused;
    
    // Start is called before the first frame update
    void Start() {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PauseResume();
        }
    }

    public void PauseResume() {
        if (isPaused) {
            isPaused = false;
            Time.timeScale = 1f;
            canvas.SetActive(false);
        } else {
            isPaused = true;
            Time.timeScale = 0f;
            canvas.SetActive(true);
        }
    }
}
