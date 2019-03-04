using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Menu_Functions : MonoBehaviour
{
    enum states { menu, credits}
    private states currentState = states.menu;
    [SerializeField]
    private CanvasGroup credits_Screen;
    [SerializeField]
    private VerticalLayoutGroup scroller;
    private float scrollPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.menu;
        credits_Screen.alpha = 0;
        credits_Screen.gameObject.SetActive(false);
        scrollPosition = scroller.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case states.menu:

                break;
            case states.credits:
                scroller.transform.Translate(0,20*Time.deltaTime,0);
                Debug.Log(scroller.transform.position);
                if (Input.GetMouseButtonDown(0) || scroller.transform.position.y> 710)
                {
                    currentState = states.menu;
                    credits_Screen.alpha = 0;
                    credits_Screen.gameObject.SetActive(false);
                    Vector3 reset = new Vector3(scroller.transform.position.x, scrollPosition, scroller.transform.position.z);
                    scroller.transform.position = reset;
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
            credits_Screen.gameObject.SetActive(true);
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



