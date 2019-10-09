using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eGAME_PHASE { MENU, CREDITS, VERSUS, SERVE, PLAY, POINT, END };
public enum eGAME_STATE { RUN, LOAD, PAUSE, QUIT };

public class GameManager : MonoBehaviour
{
    public static GameObject currentCanvas;
    private static bool isPaused;
    public static GameManager instance { get; private set; }
    public static eGAME_PHASE gamePhase { get; private set; }
    public static eGAME_STATE gameState { get; private set; }
    private static bool loaded;
    public static int score { get; private set; }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake() {
        loaded = false;
        gameState = eGAME_STATE.RUN;
        gamePhase = eGAME_PHASE.MENU;
        if (!instance) {
            instance = this;
        } else {
            Destroy(this);
        }
    }
    
    void Start() {
        isPaused = false;
    }

    void Update() {
        switch (gameState) {
            case eGAME_STATE.PAUSE:
                break;
            case eGAME_STATE.LOAD:
                switch (gamePhase) {
                    case eGAME_PHASE.MENU:
                        break;
                    case eGAME_PHASE.CREDITS:

                        break;
                    case eGAME_PHASE.VERSUS:
                        LoadScene("GameScene");
                        break;
                    case eGAME_PHASE.PLAY:
                        break;
                }
                break;
            case eGAME_STATE.RUN:
                switch (gamePhase) {
                    case eGAME_PHASE.MENU:
                        break;
                    case eGAME_PHASE.VERSUS:
                        break;
                    case eGAME_PHASE.PLAY:
                        break;
                }
                break;
            case eGAME_STATE.QUIT:
                Application.Quit();
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseResume();
        }
    }


    public void PauseResume() {
        if (isPaused) {
            isPaused = false;
            Time.timeScale = 1f;
            currentCanvas.SetActive(false);
        } else {
            isPaused = true;
            Time.timeScale = 0f;
            currentCanvas.SetActive(true);
        }
    }

    public void SetNewPhase(eGAME_PHASE newPhase) {
        gamePhase = newPhase;
    }

    //LOADING

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        loaded = true;
        currentCanvas = FindObjectOfType<Canvas>().gameObject;
    }

    private void LoadScene(string name) {
        LoadGameScene(name);
        loaded = false;
    }

    private IEnumerator LoadGameScene(string name) {
        SetNewPhase(eGAME_PHASE.VERSUS);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    //SCORE

    public void AddScore(int playerIndex) {

    }
}
