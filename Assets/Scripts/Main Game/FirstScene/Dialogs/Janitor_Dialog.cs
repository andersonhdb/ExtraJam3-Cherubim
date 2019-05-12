using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Janitor_Dialog : MonoBehaviour
{
    First_Screen_States fss;
    audio_Manager am;
    Dialog_manager dm;
    [SerializeField]
    Image fadePannel;

    [SerializeField]
    NavMeshAgent janitor_Mesh;
    [SerializeField]
    Animator janitor_Animator;
    [SerializeField]
    NavMeshAgent player_Mesh;
    [SerializeField]
    Animator player_Animator;

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

    Fade_In_Node Fight_Fade_In;
    Fade_Out_Node Fight_Fade_Out;

    Destructor_Node Janitor_Exit3;
    Destructor_Node Janitor_Beard_Like_Yes_PieDestruction;
    Destructor_Node Janitor_Pie_some_cash_pieDestruction;

    Destructor_Node Janitor_Pie_some_fight_pie1;
    Destructor_Node Janitor_Pie_some_fight_pie2;
    Destructor_Node Janitor_Pie_some_fight_janitor;

    Destructor_Node Janitor_Pie_some_split_no_pie_Destruction1;
    Destructor_Node Janitor_Pie_some_split_no_pie_Destruction2;

    Destructor_Node Janitor_Pie_some_split_yes_pie_Destruction;

    SceneChanger_Node toLoose;

    ExecuteFunction_Node Janitor_leave_room;


    // Start is called before the first frame update
    void Start()
    {
        fss = gameObject.GetComponent<First_Screen_States>();
        am = gameObject.GetComponent<audio_Manager>();
        dm = gameObject.GetComponent<Dialog_manager>();

        Janitor_leave_room = new ExecuteFunction_Node(fss.DealWithJanitor, null);

        Janitor_intro = new Dialog_Node(janitor_text[0], janitor_options[0], true, dm);
        Janitor_Talk_1 = new Dialog_Node(janitor_text[2], null, false, dm);
        Janitor_Talk_2 = new Dialog_Node(janitor_text[3], janitor_options[1], true, dm);
        Janitor_description = new Dialog_Node(janitor_text[1], null, false, dm);
        Janitor_Beard = new Dialog_Node(janitor_text[4], janitor_options[2], true, dm);
        Janitor_Beard_Dislike = new Dialog_Node(janitor_text[5], null, false, dm);
        Janitor_Beard_Like = new Dialog_Node(janitor_text[6], janitor_options[3], true, dm);
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

        Fight_Fade_Out = new Fade_Out_Node(fadePannel, 0.3f, janitor_Pie_some_fight3);

        Janitor_Exit3 = new Destructor_Node("Janitor", Janitor_leave_room);
        Janitor_Pie_some_fight_janitor = new Destructor_Node("Janitor", Fight_Fade_Out);
        Janitor_Pie_some_fight_pie2 = new Destructor_Node("Pie Major", Janitor_Pie_some_fight_janitor);
        Janitor_Pie_some_fight_pie1 = new Destructor_Node("Pie_Slice", Janitor_Pie_some_fight_pie2);

        Fight_Fade_In = new Fade_In_Node(fadePannel, 0.3f, Janitor_Pie_some_fight_pie1);

        Janitor_Exit2 = new Movement_Node(janitor_Mesh, new Vector3(4.5f, 0, 9f), 5, janitor_Animator, "Walking", Janitor_Exit3);
        Janitor_Exit1 = new Movement_Node(janitor_Mesh, new Vector3(4.5f, 0, 7.5f), 5, janitor_Animator, "Walking", Janitor_Exit2);

        Player_fall = new Movement_Node(player_Mesh, new Vector3(4, 0.2f, 5f), 10, player_Animator, "Knock", Fight_Fade_In);
        Janitor_Puch = new Movement_Node(janitor_Mesh, new Vector3(4, 0, 7.5f), 5, janitor_Animator, "KungFoo", Player_fall);
        Player_MoveToFIght = new Movement_Node(player_Mesh, new Vector3(4, 0.2f, 6f), 8, player_Animator, "walking", janitor_Pie_some_fight2);
        Janitor_MoveToFight = new Movement_Node(janitor_Mesh, new Vector3(4, 0, 8), 5, janitor_Animator, "Walking", Player_MoveToFIght);

        Janitor_Beard_Like_Yes_PieDestruction = new Destructor_Node("Pie Major", Janitor_Exit1);
        Janitor_Pie_some_cash_pieDestruction = new Destructor_Node("Pie_Slice", Janitor_Pie_some_cash3);

        Janitor_Pie_some_split_no_pie_Destruction2 = new Destructor_Node("Pie_Slice", janitor_Pie_some_split_no2);
        Janitor_Pie_some_split_no_pie_Destruction1 = new Destructor_Node("Pie Major", Janitor_Pie_some_split_no_pie_Destruction2);

        Janitor_Pie_some_split_yes_pie_Destruction = new Destructor_Node("Pie Major", Janitor_Exit1);

        toLoose = new SceneChanger_Node(3);

        Janitor_intro.setNextNodes(Janitor_description, Janitor_Talk_1);
        Janitor_Talk_1.setNextNodes(Janitor_Talk_sound);
        Janitor_Talk_2.setNextNodes(Janitor_Beard_sound, Janitor_Cake_sound, Janitor_Pie_sound, null);
        Janitor_Beard.setNextNodes(Janitor_Beard_dislike_sound, Janitor_Beard_like_sound, Janitor_Beard_NoComment_sound);
        Janitor_Beard_Dislike.setNextNodes(toLoose);
        Janitor_Beard_Like.setNextNodes(Janitor_Beard_Like_Later_sound, Janitor_Beard_Like_Yes);
        Janitor_Beard_Like_Yes.setNextNodes(Janitor_Beard_Like_Yes_PieDestruction);
        Janitor_Beard_Like_Later.setNextNodes(toLoose);
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
        janitor_Pie_some_fight3.setNextNodes(toLoose);
        janitor_Pie_some_split.setNextNodes(Janitor_Pie_some_split_details_sound, Janitor_Pie_some_split_no_sound, Janitor_Pie_some_split_yes_sound);
        janitor_Pie_some_split_details.setNextNodes(janitor_Pie_some_split);
        janitor_Pie_some_split_no1.setNextNodes(Janitor_Pie_some_split_no_pie_Destruction1);
        janitor_Pie_some_split_no2.setNextNodes(toLoose);
        janitor_Pie_some_split_yes.setNextNodes(Janitor_Pie_some_split_yes_pie_Destruction);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public behavioural_Node GetCurrentNode()
    {
        return Janitor_intro;
    }
    
}
