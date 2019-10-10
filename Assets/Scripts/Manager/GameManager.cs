using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eGAME_PHASE { MENU, VERSUS, SERVE, PLAY, END };
public enum eGAME_STATE { RUN, LOAD, PAUSE};
[DefaultExecutionOrder(-1000)]
public class GameManager : MonoBehaviour
{
    public const int maxPoints = 1;
    private bool isPaused;
    public static GameManager instance { get; private set; }
    public eGAME_PHASE gamePhase { get; private set; }
    public eGAME_STATE gameState { get; private set; }
    private bool loaded;
    public int[] scores { get; private set; }
    private int lastPlayerGoal;

    public delegate void Service(int playerIndex);
    public event Service onService;

    public delegate void End();
    public event End onEnd;

	public delegate void Play();
	public event Play onPlay;

	public delegate void Pause(bool isPause);
	public event Pause onPause;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
		
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake() {
        lastPlayerGoal = 0;
        scores = new int[2];
        gameState = eGAME_STATE.RUN;
        gamePhase = eGAME_PHASE.MENU;
        if (!instance) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

	private void Start()
	{
		StartService(0);
	}

	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseResume();
        }
    }

    public void PauseResume() {
        if (isPaused) {
            isPaused = false;
            Time.timeScale = 1f;
          //  currentCanvas.transform.GetChild(0).gameObject.SetActive(false);
        } else {
            isPaused = true;
            Time.timeScale = 0f;
           // currentCanvas.transform.GetChild(0).gameObject.SetActive(true);
        }
		onPause?.Invoke(isPaused);
    }

    private IEnumerator StartVersus() {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        StartService(lastPlayerGoal);
    }

    private IEnumerator CoService(int playerIndex) {
        yield return new WaitForSecondsRealtime(3f);
        StartService(playerIndex);
    }

    private void StartService(int playerIndex) {
		onService?.Invoke(playerIndex);
    }

    //LOADING

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Invoke("LoadCanvas", 0.01f);
        if(scene.buildIndex == 1) {
            StartCoroutine(StartVersus());
        }
    }

    public void LoadScene(int index) {
		onPlay?.Invoke();
        SceneManager.LoadScene(index);
    }
    
    public void AddScore(int playerIndex) {
		Debug.Log("add score");
        scores[playerIndex] += 1;
        lastPlayerGoal = playerIndex;
        if (scores[playerIndex] == maxPoints) {
            onEnd?.Invoke();
        } else {
            StartCoroutine(CoService(playerIndex));
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}