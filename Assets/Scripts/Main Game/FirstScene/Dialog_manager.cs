using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_manager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup dialogBox;
    [SerializeField]
    private Text dialog_Text;
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

}
