using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public delegate void optionClicked();

public class Dialog_manager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup dialogBox;
    [SerializeField]
    private Text dialog_Text;
    [SerializeField]
    private Button[] options = new Button[4];
    [SerializeField]
    private Text[] optionsText = new Text[4];
    //private UnityAction[] optionsEvents = new UnityAction[4];
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogText(string t)
    {
        dialog_Text.text = t;
    }

    public void ShowDialogText()
    {
        dialogBox.alpha = 1;
        //dialogBox.gameObject.SetActive(true);
    }

    public void HideDialogBox()
    {
        dialogBox.alpha = 0;
        //dialogBox.gameObject.SetActive(false);
    }
    
    public void setOptions(string[] option_Text)
    {
        if (option_Text.Length < 5)
        {
            for(int i=0; i<option_Text.Length; i++)
            {
                optionsText[i].text = option_Text[i];
                //optionsEvents[i] = events[i];
                //options[i].onClick.AddListener(events[i]);
                options[i].gameObject.SetActive(true);
                options[i].enabled = true;
            }
            //optionsEvents[0]();
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                optionsText[i].text = option_Text[i];
                //optionsEvents[i] = events[i];
                options[i].gameObject.SetActive(true);
            }
        }
    }

    public void DisableOptions()
    {
        foreach(Button opt in options)
        {
            if (opt.IsActive())
            { 
                opt.gameObject.SetActive(false);
            }
        }
    }

    public Button[] SendButtonReference()
    {
        return options;
    }
}
