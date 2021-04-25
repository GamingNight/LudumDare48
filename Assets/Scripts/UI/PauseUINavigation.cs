using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUINavigation : MonoBehaviour
{

	public GameObject pauseCanvas;
	public GameObject selector;
	public LevelManager levelManager;
    public Text restart;
    public Text levelList;
    public Text quit;

    private int currentCursorValue;
    private float prevVerticalvalue;
    private PauseManager pauseManager;

    void Start()
    {
    	pauseManager = GetComponent<PauseManager>();
        currentCursorValue = 0;
    }


    public void ResetCursor() {
        currentCursorValue = 0;
    }

    void Update() {

        if (!pauseManager.IsPaused()) {
            return;
        }
        float v = Input.GetAxisRaw("Horizontal");

        if (v != 0 && v != prevVerticalvalue) {
            int newCursorValue = currentCursorValue + (int)v;
            if (newCursorValue < 0) {
                newCursorValue = 2;
            } else if (newCursorValue > 2) {
                newCursorValue = 0;
            }
            FocusCursorOnvalue(newCursorValue);
        }

        prevVerticalvalue = v;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            switch (currentCursorValue) {
                case 0:
                    Restart();
                    break;
                case 1:
                    LevelSelection();
                    break;
                case 2:
                    Quit();
                    break;
                default:
                    break;
            }
        }
    }

    public void Restart() {
    	if (levelManager != null)
    	{
    		levelManager.ActiveNextLevel(0);
    	}
        pauseManager.ResumeGame();
    }

    public void LevelSelection() {
        if (levelManager != null)
    	{
    		levelManager.ActiveNextLevel(-100);
    	}
    	pauseManager.ResumeGame();
    }

    public void Quit() {
        if (levelManager != null)
    	{
    		levelManager.ActiveNextLevel(-200);
    	}
    	pauseManager.ResumeGame();
    }

    public void FocusCursorOnvalue(int value) {
        switch (value) {
            case 0:
            	selector.transform.position = restart.transform.position;
                break;
            case 1:
            	selector.transform.position = levelList.transform.position;
                break;
            case 2:
            	selector.transform.position = quit.transform.position;
                break;
            default:
                break;
        }

        currentCursorValue = value;
    }
}
