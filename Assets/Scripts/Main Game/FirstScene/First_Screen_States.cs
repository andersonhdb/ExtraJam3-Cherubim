using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.AI;

public class First_Screen_States : MonoBehaviour
{
    Janitor_Dialog jd;
    first_Level_Intro firstLevelIntro;
    DoorInteraction doorInteraction;
    Pie_Interaction pieInteraction;

    //All text will go here.

    string character_description = "You are a guy.You’re wearing a tie.You want to eat pie.";

    string Table_description = "You see a wood table.  It has four legs and a top.  It seems to be well-hewn.";

    string[][] microwave_text = new string[][]
    {
        new string[]
        {
            "you see a black microwave"
        },
        new string[]
        {
            "Will you heat your pie piece in the microwave?"
        },
        new string[]
        {
            "But your pie will be cold!  Oh well."
        },
        new string[]
        {
            "As if in a trance, you heat your pie until you hear a “BEEP!”  Your pie is warm, but not overdone."
        },
        new string[]
        {
            "Will you heat your pie in the microwave?"
        },
        new string[]
        {
            "Will you remove the pie tin first?"
        },
        new string[]
        {
            "You microwave the pie with the tin.  Despite urban legends, the microwave does not explode, nor do you open a rift to a parallel dimension.",
            "You do, however, witness lightning.",
            "After a familiar, “BEEP!” you find your pie warm enough to handle.  And maybe some sparks."
        },
        new string[]
        {
            "You go through the motions of microwaving your pie.",
            "  You know it’s warm after it makes its familiar ringtone of, “BEEP!”"
        }
    };

    string[][] microwave_options = new string[][]
    {
        new string[]
        {
           "yes","no"
        }
    };

    //End of all text

    // All audio will go here
    audio_Manager am;

    Dialog_Node Character_description_dialog_node;
    Dialog_Node table_description_dialog_node;

    stateChager toLooseState;
    stateChager toWinState;
    

    //__________________________Microwave Dialog Nodes___________________________________//

    Dialog_Node microwave_description;

    behavioural_Node current_Node = null;
    
    

    enum first_screen_state { intro, free_roaming, self_description, door_interaction, table_interaction, janitor_interaction, eat_pie, pie_interaction}
    enum janitor_state_machine { intro, description, talk }

    first_screen_state current_state;
    private static janitor_state_machine janitorDialogManager;

    int Long_dialog_index = 0;

    bool entering_state = true;
    bool during_state = false;
    bool exiting_state = false;

    // All the flags of the first scene go here!
    bool hasAteThePie = false;
    bool hasPieSlice = false;
    bool hasMostOfThePie = false;
    bool MadeDealWithJanitor = false;
    bool heatedThePie = false;
    bool janitorLeft = false;

    [SerializeField]
    Dialog_manager dm;

    [SerializeField]
    Image fadePannel;

    // Start is called before the first frame update
    void Start()
    {
        firstLevelIntro = gameObject.GetComponent<first_Level_Intro>();
        doorInteraction = gameObject.GetComponent<DoorInteraction>();

        am = gameObject.GetComponent<audio_Manager>();
        current_state = first_screen_state.intro;
        Character_description_dialog_node = new Dialog_Node(new string[] { character_description }, null, false, dm);
        table_description_dialog_node = new Dialog_Node(new string[] { Table_description }, null, false, dm);

        //_________________________________________Janitor Side______________________________________________________//

        jd = gameObject.GetComponent<Janitor_Dialog>();

        //________________________________________Pie Side________________________________________________________//

        pieInteraction = gameObject.GetComponent<Pie_Interaction>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current_Node == null)
        {
            switch (current_state)
            {
                case first_screen_state.intro:
                    current_Node = firstLevelIntro.GetCurrentNode();
                    current_state = first_screen_state.free_roaming;
                    break;
                case first_screen_state.free_roaming:
                    current_Node = null;
                    break;
                case first_screen_state.self_description:
                    current_Node = Character_description_dialog_node;
                    current_state = first_screen_state.free_roaming;
                    break;
                case first_screen_state.door_interaction:
                    current_Node = doorInteraction.GetCurrentNode();
                    current_state = first_screen_state.free_roaming;
                    break;
                case first_screen_state.table_interaction:
                    current_Node = table_description_dialog_node;
                    current_state = first_screen_state.free_roaming;
                    break;
                case first_screen_state.janitor_interaction:
                    current_Node = jd.GetCurrentNode();
                    current_state = first_screen_state.free_roaming;
                    break;
                case first_screen_state.eat_pie:
                    hasAteThePie = true;
                    current_state = first_screen_state.free_roaming;
                    current_Node = null;
                    break;
                case first_screen_state.pie_interaction:
                    current_Node = pieInteraction.getCurrentNode();
                    current_state = first_screen_state.free_roaming;
                    break;
                default:
                    Debug.Log("error, unknown game state entered");
                    break;
            }
        }
        else
        {
            current_Node = current_Node.Run();
        }
    }

    //State specific Routines

    //Functions specific to the janitor dialog tree

    


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
        if (current_state == first_screen_state.free_roaming && current_Node==null)
        {
            return true;
        }
        return false;
    }

    //Interactors specific function

    public void SetState(string obj)
    {
        switch (obj)
        {
            case "vase":

                break;
            case "microwave":

                break;
            case "pie":
                current_state = first_screen_state.pie_interaction;
                break;
            case "tin":

                break;
            case "door":
                current_state = first_screen_state.door_interaction;
                break;
            case "table":
                current_state = first_screen_state.table_interaction;
                break;
            case "janitor":
                current_state = first_screen_state.janitor_interaction;
                break;
            default:
                Debug.Log("unknow interactor string: " + obj);
                break;
        }
    }

    private void SetState(first_screen_state st)
    {
        current_state = st;
    }

    private class stateChager : behavioural_Node
    {
        first_screen_state toState;
        First_Screen_States gameManager;

        public stateChager(First_Screen_States gm, first_screen_state state)
        {
            toState = state;
            gameManager = gm;
        }

        public behavioural_Node Run()
        {
            gameManager.SetState(toState);  
            return null;
        }
    }

    public bool HasAtePie()
    {
        return hasAteThePie;
    }

    public void EatThePie()
    {
        hasAteThePie = true;
    }

    public bool HasDealtWithJanitor()
    {
        return janitorLeft;
    }

    public void DealWithJanitor()
    {
        janitorLeft = true;
    }

    public bool HasHeatedThePie()
    {
        return heatedThePie;
    }
}















