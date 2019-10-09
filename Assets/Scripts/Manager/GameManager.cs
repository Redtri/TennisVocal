using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGAME_PHASE { MENU, VERSUS, PLAY };
public enum eGAME_STATE { RUN, PAUSE, QUIT };

public class GameManager : MonoBehaviour
{
    public static GameObject canvas;
    private static bool isPaused;
    public static GameManager instance { get; private set; }
    public static eGAME_PHASE gamePhase { get; private set; }
    public static eGAME_STATE gameState { get; private set; }

    private void Awake() {
        if (!instance) {
            instance = this;
        } else {
            Destroy(this);
        }
    }
    
    void Start() {
        isPaused = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
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
