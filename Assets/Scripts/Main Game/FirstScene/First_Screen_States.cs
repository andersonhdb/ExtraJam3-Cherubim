using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Screen_States : MonoBehaviour
{

    //All text will go here.
    string[] intro_text = {"You were invited to your work friend’s birthday celebration, but, well, work happened.",
        "A meeting ran late.  The elevator was slow.  You had to wipe the toilet paper off your face.  And so on and so forth.",
        "You arrived in the break room where you found no cake, but you instead found a pie!",
        "It’s guarded by a gruff-looking janitor who seems ready to eat it all.",
        "What do you do?"};

    string character_description = "You are a guy.You’re wearing a tie.You want to eat pie.";


    //End of all text.

    enum first_screen_state { intro, free_roaming, self_description }
    first_screen_state current_state;

    int Long_dialog_index = 0;

    bool entering_state = true;
    bool during_state = false;
    bool exiting_state = false;

    [SerializeField]
    Dialog_manager dm;
    // Start is called before the first frame update
    void Start()
    {
        current_state = first_screen_state.intro;
    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state)
        {
            case first_screen_state.intro:
                intro_routine();
                break;
            case first_screen_state.free_roaming:

                break;
            case first_screen_state.self_description:
                self_description_routine();
                break;
            default:
                Debug.Log("error, unknown game state entered");
                break;
        }    
    }

    //State specific Routines

    private void intro_routine()
    {
        if (entering_state)
        {
            Long_dialog_index = 0;
            dm.SetDialogText(intro_text[Long_dialog_index++]);
            dm.ShowDialogText();
            entering_state = false;
            during_state = true;
        }
        else if (during_state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Long_dialog_index < intro_text.Length)
                {
                    dm.SetDialogText(intro_text[Long_dialog_index++]);
                }
                else
                {
                    during_state = false;
                    exiting_state = true;
                }
            }
        }
        else if (exiting_state)
        {
            dm.HideDialogBox();
            current_state = first_screen_state.free_roaming;
            Long_dialog_index = 0;
            exiting_state = false;
            entering_state = true;
        }
        else
        {
            Debug.Log("Error, intro is in an unknown state");
        }
    }

    public void self_description_routine()
    {
        if (entering_state)
        {
            dm.SetDialogText(character_description);
            dm.ShowDialogText();
            entering_state = false;
            during_state = true;
        }
        else if (during_state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                during_state = false;
                exiting_state = true;
            }
        }
        else if (exiting_state)
        {
            dm.HideDialogBox();
            current_state = first_screen_state.free_roaming;
            Long_dialog_index = 0;
            exiting_state = false;
            entering_state = true;
        }
        else
        {
            Debug.Log("Error, self_description in unknown state");
        }
    }

    //Roam Specific Functions:

    public void ToSelf()
    {
        if (current_state == first_screen_state.free_roaming)
        {
            current_state = first_screen_state.self_description;
        }
    }


    public bool MayRoam()
    {
        if (current_state == first_screen_state.free_roaming)
        {
            return true;
        }
        return false;
    }
}
