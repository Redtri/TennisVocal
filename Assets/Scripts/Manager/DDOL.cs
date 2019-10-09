using UnityEngine;


public class DDOL : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Time.timeScale = 0;
        }
    }
}
