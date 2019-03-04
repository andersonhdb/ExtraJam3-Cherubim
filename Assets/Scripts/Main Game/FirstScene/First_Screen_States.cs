using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class First_Screen_States : MonoBehaviour
{

    //All text will go here.
    string[] intro_text = {"You were invited to your work friend’s birthday celebration, but, well, work happened.",
        "A meeting ran late.  The elevator was slow.  You had to wipe the toilet paper off your face.  And so on and so forth.",
        "You arrived in the break room where you found no cake, but you instead found a pie!",
        "It’s guarded by a gruff-looking janitor who seems ready to eat it all.",
        "What do you do?"};

    string character_description = "You are a guy.You’re wearing a tie.You want to eat pie.";

    string[] door_text =
    {
        "This door is the only known way to this office’s break room.",
        "Now that you have eaten pie, this door is the only known exit to this office’s break room.  You are convinced that today was a good day to pie.",
        "You’re here to eat, not quit!"
    };

    string Table_description = "You see a wood table.  It has four legs and a top.  It seems to be well-hewn.";

    string[][] janitor_text = new string[][]
    {
        new string []{ "The Janitor"},
        new string []{
        "You see one of the building’s janitors. You may have seen his legs before when you wanted alone time in a bathroom.",
        "He smells of tobacco smoke and has a scraggly beard.",
        "He seems hungry for pie."
        },
        new string[]
        {
            "The janitor looks up at you with a large spoon in his hand.",
        },
        new string[]
        {
            "Janitor: Whadda ya want?"
        },

        new string[]
        {
            "Janitor: What do you think of my beard?  It took me nine months to get it the way I like it!"
        },
        new string[]
        {
            "“Beardist!” he shouts, putting his face first into the pie and gobbling it faster than you thought possible",
            "You feel bad",
            "You also lose."
        },
        new string[]
        {
            "Janitor: A man of fine taste!  Want some pie?"
        },
        new string[]
        {
            "“Suit yourself!” he says, putting his face first into the pie and gobbling it faster than you thought possible.",
            "You were so close, and yet you lost.",
            "Maybe next time you’ll win."
        },
        new string[]
        {
            "In a flash, he uses his spoon to scoop out a pie piece for you, placing it on the table near you.  He then puts his face first into the rest of the pie, eating it faster than you thought possible, seemingly inhaling it.",
            "Come to think of it, you aren’t sure this guy is human.",
            "Better not dwell on that thought.",
            "The janitor quickly stands from his chair and heads to the door, opening it in a flash, and closing it just the same."
        },
        new string[]
        {
            "Janitor: Hhmph"
        },
        new string[]
        {
            "Janitor: There never was a cake.  The cake is a pie!"
        },
        new string[]
        {
            "Janitor: Ehh.  I wasn’t there for it"
        },
        new string[]
        {
            "Janitor: Get yer own pie!"
        },
        new string[]
        {
            "He looks like he’s about to say no, but then quickly realizes this could be a mutually beneficial agreement."
        },
        new string[]
        {
            "Janitor: What’s your offer?"
        },
        new string[]
        {
            "Janitor: Thanks, but President Salzman already said no.  What else ya got?"
        },
        new string[]
        {
            "You offer him $20."
        },
        new string[]
        {
            "Janitor: That seems like a mostly fair trade"
        },
        new string[]
        {
            "He quickly scoops out a pie piece for himself and seemingly inhales it before leaving you the rest.",
            "He also leaves the room in a flash. This seems like your time to feast upon that pie!"
        },
        new string[]
        {
            "You try reaching at him using a maneuver you saw on TV."
        },
        new string[]
        {
            "In a flash, the janitor stands and does some sort of martial arts move, knocking you back."
        },
        new string[]
        {
            "After you regain consciousness, you realize the janitor is gone, and so is the pie!",
            "You lost!  The moral of the story seems to be that janitors are mighty and like eating pie!"
        },
        new string[]
        {
            "Janitor: I’ll give you a piece only on the condition that you owe me a favor later."
        },
        new string[]
        {
            "Janitor: You either promise and get the pie or don’t!"
        },
        new string[]
        {
            "Janitor: Then no pie for you!",
            "he gobbles down the pie with such a fast speed that it seemed physically impossible."
        }, 
        new string[]
        {
            "You lost.",
            "Maybe that favor wasn’t so bad after all."
        },
        new string[]
        {
            "Janitor: Then we have a deal!",
            "Before you know it, There is only a pie piece on the plate on the table with the rest of the pie gone, assumedly ingested or inhaled by the janitor."
        }
    };

    string[][] janitor_options = new string[][]
    {
        new string[] { "Examine", "Talk" },
        new string[] { "Beard", "Cake", "Pie", "End Conversation" },
        new string[] { "Dislike", "Like", "No Commet" },
        new string[] { "Maybe later.", "Very yes!" },
        new string[] { "Birthday Party" },
        new string[] { "May I have some?" },
        new string[] { "Advocate for Time Off", "Cash", "Fight", "Split the Pie" },
        new string[] { "Favour details", "No Deal", "Yes way!" }
    };

    string[][] Pie_text = new string[][]
    {
        new string[]
        {
            "This pie is yours for the eating!"
        },
        new string[]
        {
            "It’s even warm and smelling like your favorite pie flavor!"
        },
        new string[]
        {
            "This pie is cold, like it was recently removed from a fridge."
        },
        new string[]
        {
            "Before you know what has happened, you finish consuming the pie!",
            "It was deliciously worth it.",
            "You also feel like you celebrated your work friend’s birthday properly.",
            "You are happy and may leave now."
        },
        new string[]
        {
            "You ignore the pie for now, perhaps much to your own peril."
        }
    };

    string[][] pie_options = new string[][]
    {
        new string[]
        {
            "Eat Pie!", "Leave Pie!",
        }
    };

    //End of all text

    // All audio will go here
    audio_Manager am;

    Dialog_Node intro_dialog_node;
    Dialog_Node Character_description_dialog_node;
    Dialog_Node table_description_dialog_node;

    stateChager toLooseState;

    //__________________Janitor Dialog Nodes______________________//

    Dialog_Node Janitor_intro;
    Dialog_Node Janitor_description;
    Dialog_Node Janitor_Talk_1;
    Dialog_Node Janitor_Talk_2;
    Dialog_Node Janitor_Beard;
    Dialog_Node Janitor_Beard_Dislike;
    Dialog_Node Janitor_Beard_Like;
    Dialog_Node Janitor_Beard_Like_Later;
    Dialog_Node Janitor_Beard_Like_Yes;
    Dialog_Node Janitor_Beard_NoComment;
    Dialog_Node Janitor_Cake;
    Dialog_Node Janitor_Cake_birthday;
    Dialog_Node Janitor_Pie;
    Dialog_Node Janitor_Pie_some1;
    Dialog_Node Janitor_Pie_some2;
    Dialog_Node Janitor_Pie_some_advocate;
    Dialog_Node Janitor_Pie_some_cash1;
    Dialog_Node Janitor_Pie_some_cash2;
    Dialog_Node Janitor_Pie_some_cash3;
    Dialog_Node janitor_Pie_some_cash4;
    Dialog_Node janitor_Pie_some_fight1;
    Dialog_Node janitor_Pie_some_fight2;
    Dialog_Node janitor_Pie_some_fight3;
    Dialog_Node janitor_Pie_some_split;
    Dialog_Node janitor_Pie_some_split_details;
    Dialog_Node janitor_Pie_some_split_no1;
    Dialog_Node janitor_Pie_some_split_no2;
    Dialog_Node janitor_Pie_some_split_yes;

    Audio_Node Janitor_Talk_sound;
    Audio_Node Janitor_Beard_sound;
    Audio_Node Janitor_Beard_dislike_sound;
    Audio_Node Janitor_Beard_like_sound;
    Audio_Node Janitor_Beard_Like_Later_sound;
    Audio_Node Janitor_Beard_NoComment_sound;
    Audio_Node Janitor_Cake_sound;
    Audio_Node Janitor_Cake_Birthday_sound;
    Audio_Node Janitor_Pie_sound;
    Audio_Node Janitor_Pie_some_sound;
    Audio_Node Janitor_Pie_some_advocate_sound;
    Audio_Node Janitor_Pie_some_cash_sound;
    Audio_Node Janitor_Pie_some_split_sound;
    Audio_Node Janitor_Pie_some_split_details_sound;
    Audio_Node Janitor_Pie_some_split_no_sound;
    Audio_Node Janitor_Pie_some_split_yes_sound;

    Movement_Node Janitor_Exit1;
    Movement_Node Janitor_Exit2;

    Movement_Node Janitor_MoveToFight;
    Movement_Node Player_MoveToFIght;
    Movement_Node Janitor_Puch;
    Movement_Node Player_fall;

    FadeNode Fight_Fade;

    Destructor_Node Janitor_Exit3;
    Destructor_Node Janitor_Beard_Like_Yes_PieDestruction;
    Destructor_Node Janitor_Pie_some_cash_pieDestruction;

    Destructor_Node Janitor_Pie_some_fight_pie1;
    Destructor_Node Janitor_Pie_some_fight_pie2;
    Destructor_Node Janitor_Pie_some_fight_janitor;

    Destructor_Node Janitor_Pie_some_split_no_pie_Destruction1;
    Destructor_Node Janitor_Pie_some_split_no_pie_Destruction2;

    Destructor_Node Janitor_Pie_some_split_yes_pie_Destruction;

    //____________________________Pie Dialog Nodes______________________________________//

    Dialog_Node Pie_description1;
    Dialog_Node Pie_description2;
    Dialog_Node Pie_description3;
    Dialog_Node Pie_eat;
    Dialog_Node Pie_Leave;

    Conditional_Node Pie_description_heatCondition;

    Destructor_Node Pie_eat_pie;

    stateChager atePie;

    behavioural_Node current_Node = null;
    
    

    enum first_screen_state { intro, free_roaming, self_description, door_interaction, table_interaction, janitor_interaction, LooseGame, eat_pie, WinGame, pie_interaction}
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

    [SerializeField]
    Dialog_manager dm;

    [SerializeField]
    NavMeshAgent janitor_Mesh;
    [SerializeField]
    Animator janitor_Animator;
    [SerializeField]
    NavMeshAgent player_Mesh;
    [SerializeField]
    Animator player_Animator;

    [SerializeField]
    Image fadePannel;
    // Start is called before the first frame update
    void Start()
    {
        am = gameObject.GetComponent<audio_Manager>();
        current_state = first_screen_state.intro;
        intro_dialog_node = new Dialog_Node(intro_text, null, false, dm);
        Character_description_dialog_node = new Dialog_Node(new string[] { character_description }, null, false, dm);
        table_description_dialog_node = new Dialog_Node(new string[] { Table_description }, null, false, dm);

        toLooseState = new stateChager(this,first_screen_state.LooseGame);

        //_________________________________________Janitor Side______________________________________________________//

        Janitor_intro = new Dialog_Node(janitor_text[0], janitor_options[0], true, dm);
        Janitor_Talk_1 = new Dialog_Node(janitor_text[2], null, false, dm);
        Janitor_Talk_2 = new Dialog_Node(janitor_text[3], janitor_options[1], true, dm);
        Janitor_description = new Dialog_Node(janitor_text[1], null, false, dm);
        Janitor_Beard = new Dialog_Node(janitor_text[4], janitor_options[2], true, dm);
        Janitor_Beard_Dislike = new Dialog_Node(janitor_text[5], null, false, dm);
        Janitor_Beard_Like = new Dialog_Node(janitor_text[6], janitor_options[3],true, dm);
        Janitor_Beard_Like_Later = new Dialog_Node(janitor_text[7], null, false, dm);
        Janitor_Beard_Like_Yes = new Dialog_Node(janitor_text[8], null, false, dm);
        Janitor_Beard_NoComment = new Dialog_Node(janitor_text[9], null, false, dm);
        Janitor_Cake = new Dialog_Node(janitor_text[10], janitor_options[4], true, dm);
        Janitor_Cake_birthday = new Dialog_Node(janitor_text[11], null, false, dm);
        Janitor_Pie = new Dialog_Node(janitor_text[12], janitor_options[5], true, dm);
        Janitor_Pie_some1 = new Dialog_Node(janitor_text[13], null, false, dm);
        Janitor_Pie_some2 = new Dialog_Node(janitor_text[14], janitor_options[6], true, dm);
        Janitor_Pie_some_advocate = new Dialog_Node(janitor_text[15], null, false, dm);
        Janitor_Pie_some_cash1 = new Dialog_Node(janitor_text[16], null, false, dm);
        Janitor_Pie_some_cash2 = new Dialog_Node(janitor_text[17], null, false, dm);
        Janitor_Pie_some_cash3 = new Dialog_Node(janitor_text[18], null, false, dm);
        janitor_Pie_some_fight1 = new Dialog_Node(janitor_text[19], null, false, dm);
        janitor_Pie_some_fight2 = new Dialog_Node(janitor_text[20], null, false, dm);
        janitor_Pie_some_fight3 = new Dialog_Node(janitor_text[21], null, false, dm);
        janitor_Pie_some_split = new Dialog_Node(janitor_text[22], janitor_options[7], true, dm);
        janitor_Pie_some_split_details = new Dialog_Node(janitor_text[23], null, false, dm);
        janitor_Pie_some_split_no1 = new Dialog_Node(janitor_text[24], null, false, dm);
        janitor_Pie_some_split_no2 = new Dialog_Node(janitor_text[25], null, false, dm);
        janitor_Pie_some_split_yes = new Dialog_Node(janitor_text[26], null, false, dm);

        Janitor_Talk_sound = new Audio_Node("JAN_Grunt", Janitor_Talk_2, am);
        Janitor_Beard_sound = new Audio_Node("JAN_Question", Janitor_Beard, am);
        Janitor_Beard_dislike_sound = new Audio_Node("JAN_Disapproval", Janitor_Beard_Dislike, am);
        Janitor_Beard_like_sound = new Audio_Node("JAN_Approval", Janitor_Beard_Like, am);
        Janitor_Beard_Like_Later_sound = new Audio_Node("JAN_Grunt", Janitor_Beard_Like_Later, am);
        Janitor_Beard_NoComment_sound = new Audio_Node("JAN_Grunt", Janitor_Beard_NoComment, am);
        Janitor_Cake_sound = new Audio_Node("JAN_Approval", Janitor_Cake, am);
        Janitor_Cake_Birthday_sound = new Audio_Node("JAN_Disapproval", Janitor_Cake_birthday, am);
        Janitor_Pie_sound = new Audio_Node("JAN_Disapproval", Janitor_Pie, am);
        Janitor_Pie_some_sound = new Audio_Node("JAN_Question", Janitor_Pie_some2, am);
        Janitor_Pie_some_advocate_sound = new Audio_Node("JAN_Disapproval", Janitor_Pie_some_advocate, am);
        Janitor_Pie_some_cash_sound = new Audio_Node("JAN_Approval", Janitor_Pie_some_cash2, am);
        Janitor_Pie_some_split_sound = new Audio_Node("JAN_Question", janitor_Pie_some_split, am);
        Janitor_Pie_some_split_details_sound = new Audio_Node("JAN_Disapproval", janitor_Pie_some_split_details, am);
        Janitor_Pie_some_split_no_sound = new Audio_Node("JAN_Disapproval", janitor_Pie_some_split_no1, am);
        Janitor_Pie_some_split_yes_sound = new Audio_Node("JAN_Approval", janitor_Pie_some_split_yes, am);

        Janitor_Exit3 = new Destructor_Node("Janitor", null);
        Janitor_Pie_some_fight_janitor = new Destructor_Node("Janitor", janitor_Pie_some_fight3);
        Janitor_Pie_some_fight_pie2 = new Destructor_Node("Pie Major", Janitor_Pie_some_fight_janitor);
        Janitor_Pie_some_fight_pie1 = new Destructor_Node("Pie_Slice", Janitor_Pie_some_fight_pie2);


        Fight_Fade = new FadeNode(fadePannel, 0.3f, Janitor_Pie_some_fight_pie1);

        Janitor_Exit2 = new Movement_Node(janitor_Mesh, new Vector3(4.5f, 0, 9f), 5, janitor_Animator, "Walking", Janitor_Exit3);
        Janitor_Exit1 = new Movement_Node(janitor_Mesh, new Vector3(4.5f, 0, 7.5f), 5, janitor_Animator, "Walking", Janitor_Exit2);

        Player_fall = new Movement_Node(player_Mesh, new Vector3(4, 0.2f, 5f), 10, player_Animator, "Knock", Fight_Fade);
        Janitor_Puch = new Movement_Node(janitor_Mesh, new Vector3(4, 0, 7.5f), 5, janitor_Animator, "KungFoo", Player_fall);
        Player_MoveToFIght = new Movement_Node(player_Mesh, new Vector3(4, 0.2f, 6f), 8, player_Animator, "walking", janitor_Pie_some_fight2);
        Janitor_MoveToFight = new Movement_Node(janitor_Mesh, new Vector3(4, 0, 8), 5, janitor_Animator, "Walking", Player_MoveToFIght);

        Janitor_Beard_Like_Yes_PieDestruction = new Destructor_Node("Pie Major", Janitor_Exit1);
        Janitor_Pie_some_cash_pieDestruction = new Destructor_Node("Pie_Slice", Janitor_Pie_some_cash3);

        Janitor_Pie_some_split_no_pie_Destruction2 = new Destructor_Node("Pie_Slice", janitor_Pie_some_split_no2);
        Janitor_Pie_some_split_no_pie_Destruction1 = new Destructor_Node("Pie Major", Janitor_Pie_some_split_no_pie_Destruction2);

        Janitor_Pie_some_split_yes_pie_Destruction = new Destructor_Node("Pie Major", Janitor_Exit1);

        Janitor_intro.setNextNodes(Janitor_description, Janitor_Talk_1);
        Janitor_Talk_1.setNextNodes(Janitor_Talk_sound);
        Janitor_Talk_2.setNextNodes(Janitor_Beard_sound, Janitor_Cake_sound, Janitor_Pie_sound, null);
        Janitor_Beard.setNextNodes(Janitor_Beard_dislike_sound, Janitor_Beard_like_sound, Janitor_Beard_NoComment_sound);
        Janitor_Beard_Dislike.setNextNodes(toLooseState);
        Janitor_Beard_Like.setNextNodes(Janitor_Beard_Like_Later_sound,Janitor_Beard_Like_Yes);
        Janitor_Beard_Like_Yes.setNextNodes(Janitor_Beard_Like_Yes_PieDestruction);
        Janitor_Beard_Like_Later.setNextNodes(toLooseState);
        Janitor_Cake.setNextNodes(Janitor_Cake_Birthday_sound);
        Janitor_Pie.setNextNodes(Janitor_Pie_some1);
        Janitor_Pie_some1.setNextNodes(Janitor_Pie_some_sound);
        Janitor_Pie_some2.setNextNodes(Janitor_Pie_some_advocate_sound, Janitor_Pie_some_cash1, janitor_Pie_some_fight1, Janitor_Pie_some_split_sound);
        Janitor_Pie_some_advocate.setNextNodes(Janitor_Pie_some2);
        Janitor_Pie_some_cash1.setNextNodes(Janitor_Pie_some_cash_sound);
        Janitor_Pie_some_cash2.setNextNodes(Janitor_Pie_some_cash_pieDestruction);
        Janitor_Pie_some_cash3.setNextNodes(Janitor_Exit1);
        janitor_Pie_some_fight1.setNextNodes(Janitor_MoveToFight);
        janitor_Pie_some_fight2.setNextNodes(Janitor_Puch);
        janitor_Pie_some_fight3.setNextNodes(toLooseState);
        janitor_Pie_some_split.setNextNodes(Janitor_Pie_some_split_details_sound, Janitor_Pie_some_split_no_sound, Janitor_Pie_some_split_yes_sound);
        janitor_Pie_some_split_details.setNextNodes(janitor_Pie_some_split);
        janitor_Pie_some_split_no1.setNextNodes(Janitor_Pie_some_split_no_pie_Destruction1);
        janitor_Pie_some_split_no2.setNextNodes(toLooseState);
        janitor_Pie_some_split_yes.setNextNodes(Janitor_Pie_some_split_yes_pie_Destruction);

        //________________________________________Pie Side________________________________________________________//

        Pie_description1 = new Dialog_Node(Pie_text[0], null, false, dm);
        Pie_description2 = new Dialog_Node(Pie_text[1], pie_options[0], true, dm);
        Pie_description3 = new Dialog_Node(Pie_text[2], pie_options[0], true, dm);
        Pie_eat = new Dialog_Node(Pie_text[3], null, false, dm);
        Pie_Leave = new Dialog_Node(Pie_text[4], null, false, dm);

        atePie = new stateChager(this, first_screen_state.eat_pie);

        Pie_description_heatCondition = new Conditional_Node(heatedThePie, Pie_description2, Pie_description3);

        Pie_eat_pie = new Destructor_Node("Pie", atePie);

        Pie_description1.setNextNodes(Pie_description_heatCondition);
        Pie_description2.setNextNodes(Pie_eat, Pie_Leave);
        Pie_description3.setNextNodes(Pie_eat, Pie_Leave);
        Pie_eat.setNextNodes(Pie_eat_pie);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current_Node == null)
        {
            switch (current_state)
            {
                case first_screen_state.intro:
                    current_Node = intro_dialog_node;
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
                    doorInteractionRoutine();
                    break;
                case first_screen_state.table_interaction:
                    current_Node = table_description_dialog_node;
                    current_state = first_screen_state.free_roaming;
                    break;
                case first_screen_state.janitor_interaction:
                    current_Node = Janitor_intro;
                    current_state = first_screen_state.free_roaming;
                    break;
                case first_screen_state.LooseGame:
                    SceneManager.LoadScene(3);
                    break;
                case first_screen_state.WinGame:
                    SceneManager.LoadScene(2);
                    break;
                case first_screen_state.eat_pie:
                    hasAteThePie = true;
                    current_state = first_screen_state.free_roaming;
                    current_Node = null;
                    break;
                case first_screen_state.pie_interaction:
                    current_Node = Pie_description1;
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

    private void doorInteractionRoutine()
    {
        if (entering_state)
        {
            Long_dialog_index = 0;
            dm.SetDialogText(door_text[Long_dialog_index++]);
            dm.ShowDialogText();
            dm.DisableOptions();
            entering_state = false;
            during_state = true;
        }
        else if (during_state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Long_dialog_index == 1)
                {
                    if (hasAteThePie)
                    {
                        dm.SetDialogText(door_text[1]);
                    }
                    else
                    {
                        dm.SetDialogText(door_text[2]);
                    }
                    Long_dialog_index++;
                }
                else if (Long_dialog_index >= 2)
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
            if (hasAteThePie)
            {
                current_state = first_screen_state.WinGame;
            }
        }
        else
        {
            Debug.Log("Error, door_interaction in unknown state");
        }
    }

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
}

public class Movement_Node: behavioural_Node
{
    NavMeshAgent Moved;
    Vector3 destination;
    Animator movementAnimation;
    string moveTrigger;
    behavioural_Node NextNode;
    float speed;
    static bool start = true;
    static bool during = false;
    static bool end = false;
    private bool hasAnimation;

    public Movement_Node(NavMeshAgent target, Vector3 dest, float speed, behavioural_Node nxt)
    {
        Moved = target;
        destination = dest;
        this.speed = speed;
        hasAnimation = false;
        NextNode = nxt;
    }

    public Movement_Node(NavMeshAgent target, Vector3 dest, float speed, Animator mov, string movBooleanTrigger, behavioural_Node nxt)
    {
        Moved = target;
        destination = dest;
        this.speed = speed;
        hasAnimation = true;
        movementAnimation = mov;
        moveTrigger = movBooleanTrigger;
        NextNode = nxt;
    }

    public behavioural_Node Run()
    {
        if (start)
        {
            if (!Moved.enabled)
            {
                Moved.enabled = true;
            }
            Moved.speed = speed;
            Moved.SetDestination(destination);
            if (hasAnimation)
            {
                movementAnimation.SetBool(moveTrigger, true);
            }
            start = false;
            during = true;
        }
        else if (during)
        {
            if (!(Moved.remainingDistance > Moved.stoppingDistance))
            {
                during = false;
                end = true;
            }
        }
        else if (end)
        {
            if (hasAnimation)
            {
                movementAnimation.SetBool(moveTrigger, false);
            }
            resetflags();
            return NextNode;
        }
        return this;
    }

    private void resetflags()
    {
        start = true;
        during = false;
        end = false;

    }

    public override string ToString()
    {
        return "next destination: " + destination;
    }
}

public class Dialog_Node : behavioural_Node{
    private string[] dialogSequence;
    private string[] options;
    private bool hasOptions;
    private List<behavioural_Node> nextDialog;
    private Dialog_manager dm;
    private UnityAction[] chosenOption = new UnityAction[]{ option0, option1, option2, option3};

    private static int dialog_index = 0;
    private static int optionSelection = -1;
    private static bool start = true;
    private static bool during = false;
    private static bool end = false;

    public Dialog_Node(string[] ds, string[] opt, bool hasopt, Dialog_manager dm)
    {
        dialogSequence = ds;
        options = opt;
        hasOptions = hasopt;
        this.dm = dm;
        nextDialog = new List<behavioural_Node>();
        if (hasopt) { 
            Button[] buts = dm.SendButtonReference();
            for (int i=0; i<4; i++)
            {
                buts[i].onClick.AddListener(chosenOption[i]);
            }
        }
        else
        {
            nextDialog.Add(null);
        }
    }

    public behavioural_Node Run()
    {
        if(optionSelection == -1) { 
            if (start)
            {
                dialog_index = 0;
                dm.DisableOptions();
                dm.SetDialogText(dialogSequence[dialog_index++]);
                if (dialogSequence.Length == 1 && hasOptions)
                {
                    dm.setOptions(options);
                }
                start = false;
                during = true;
                dm.ShowDialogText();
            }
            else if (during)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (dialog_index < dialogSequence.Length)
                    {
                        dm.SetDialogText(dialogSequence[dialog_index++]);
                        if(dialog_index==dialogSequence.Length && hasOptions)
                        {
                            dm.setOptions(options);
                        }
                    }
                    else if (!hasOptions)
                    {
                        during = false;
                        end = true;
                    }
                }
            }
            else if (end)
            {

                dm.HideDialogBox();
                dm.DisableOptions();
                resetStaticVariables();
                return nextDialog[0];
            }
            return this;
        }
        else
        {
            int selectedIndex = optionSelection;
            dm.HideDialogBox();
            dm.DisableOptions();
            resetStaticVariables();
            return nextDialog[selectedIndex];
        }

    }

    public void setNextNodes(behavioural_Node option1)
    {
        if (nextDialog.Count == 0) {
            nextDialog.Add(option1);
        }
        if(nextDialog.Count == 1 && nextDialog[0]==null)
        {
            nextDialog[0] = option1;
        }
    }

    public void setNextNodes(behavioural_Node option1, behavioural_Node option2)
    {
        if (nextDialog.Count == 0)
        {
            nextDialog.Add(option1);
            nextDialog.Add(option2);
        }
    }

    public void setNextNodes(behavioural_Node option1, behavioural_Node option2, behavioural_Node option3)
    {
        if (nextDialog.Count == 0)
        {
            nextDialog.Add(option1);
            nextDialog.Add(option2);
            nextDialog.Add(option3);
        }
    }

    public void setNextNodes(behavioural_Node option1, behavioural_Node option2, behavioural_Node option3, behavioural_Node option4)
    {
        if (nextDialog.Count == 0)
        {
            nextDialog.Add(option1);
            nextDialog.Add(option2);
            nextDialog.Add(option3);
            nextDialog.Add(option4);
        }
    }

    private void resetStaticVariables()
    {
        dialog_index = 0;
        optionSelection = -1;
        start = true;
        during = false;
        end = false;
    }


    private static void option0() { optionSelection = 0; }
    private static void option1() { optionSelection = 1; }
    private static void option2() { optionSelection = 2; }
    private static void option3() { optionSelection = 3; }

    public override string ToString()
    {
        return dialogSequence[0];
    }
}

public class Audio_Node: behavioural_Node
{
    private string audio_name;
    private audio_Manager am;
    private behavioural_Node nextNode;

    public Audio_Node(string audio_name, behavioural_Node nxt, audio_Manager am)
    {
        this.audio_name = audio_name;
        this.am = am;
        nextNode = nxt;
    }

    public behavioural_Node Run()
    {
        am.Play(audio_name);
        return nextNode;
    }
}

public class Destructor_Node: behavioural_Node
{
    private string toDestroyName;
    behavioural_Node next_Node;

    public Destructor_Node(string name, behavioural_Node nxt)
    {
        Debug.Log(nxt);
        toDestroyName = name;
        next_Node = nxt;
    }

    public behavioural_Node Run()
    {
        Object.Destroy(GameObject.Find(toDestroyName));
        return next_Node;
    }
}

public class FadeNode: behavioural_Node
{
    Image fadePannel;
    float fadeTime;
    behavioural_Node nextNode;

    private static bool start = true;
    private static bool during = false;
    private static bool end = false;
    private static float fadeTimeCounter = 0f;

    public FadeNode(Image fadeP, float fadeT, behavioural_Node nxt)
    {
        fadePannel = fadeP;
        fadeTime = fadeT;
        nextNode = nxt;
    }

    public behavioural_Node Run()
    {
        if (start)
        {
            Color col = fadePannel.color;
            col.a = 1;
            fadePannel.color = col;
            start = false;
            during = true;
            fadeTimeCounter = 0;
        }
        else if (during)
        {
            fadeTimeCounter = fadeTimeCounter + Time.deltaTime;
            if (fadeTimeCounter >= fadeTime)
            {
                during = false;
                end = true;
            }
        }
        else if (end)
        {
            Color col = fadePannel.color;
            col.a = 0;
            fadePannel.color = col;
            resetStaticVariables();
            return nextNode;
        }
        return this;
    }

    private void resetStaticVariables()
    {
        start = true;
        during = false;
        end = false;
        fadeTimeCounter = 0f;
    }
}

public class Conditional_Node: behavioural_Node
{
    bool condition;
    behavioural_Node falsecondition;
    behavioural_Node trueCondition;

    public Conditional_Node(in bool cond, behavioural_Node truec, behavioural_Node falsec)
    {
        condition = cond;
        falsecondition = falsec;
        trueCondition = truec;
    }

    public behavioural_Node Run()
    {
        if (condition)
        {
            return trueCondition;
        }
        return falsecondition;
    }
}

public interface behavioural_Node
{
    behavioural_Node Run();
}

