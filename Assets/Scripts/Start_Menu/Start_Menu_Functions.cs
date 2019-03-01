using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu_Functions : MonoBehaviour
{
    enum states { menu, credits}
    private states currentState = states.menu;
    [SerializeField]
    private CanvasGroup credits_Screen;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.menu;
        credits_Screen.alpha = 0;
        credits_Screen.gameObject.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case states.menu:

                break;
            case states.credits:
                if (Input.GetMouseButtonDown(0))
                {
                    currentState = states.menu;
                    credits_Screen.alpha = 0;
                    credits_Screen.gameObject.active = false;
                }
                break;
            default:
                Debug.Log("error");
                break;
        }
    }

    public void displayCredits()
    {
        if(currentState == states.menu)
        {
            credits_Screen.alpha = 1;
            credits_Screen.gameObject.active = true;
            currentState = states.credits;
        }
    }

    public void QuitGame()
    {
        if(currentState == states.menu)
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        if(currentState == states.menu)
        {
            SceneManager.LoadScene("Main_Game");
        }
    }
}



