using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	public GameObject pauseCanvas;
	private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                PauseGame();
                paused = true;
            } else {
                ResumeGame();
                paused = false;
            }
        }
    }

    private void PauseGame() {

        Time.timeScale = 0;


        GetComponent<PauseUINavigation>().ResetCursor();
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }

    public bool IsPaused() {
        return paused;
    }

}
