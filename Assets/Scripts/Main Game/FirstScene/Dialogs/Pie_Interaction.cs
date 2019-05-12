using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie_Interaction : MonoBehaviour
{
    string[][] Pie_text = new string[][]
    {
        new string[]
        {
            "This is the Pie.",
            "But before you can do anything,",
            "The Janitor looks at you square in the eye."
        },
        new string[]
        {
            "Janito: Hey! Hands off!"
        },
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
            "Eat Pie!", "Leave Pie!"
        }
    };

    First_Screen_States fss;
    Dialog_manager dm;
    audio_Manager am;

    Dialog_Node Pie_janitor_is_here1;
    Dialog_Node Pie_janitor_is_here2;

    Audio_Node Pie_Janitor_Grunt;

    Dialog_Node Pie_description1;
    Dialog_Node Pie_description2;
    Dialog_Node Pie_description3;
    Dialog_Node Pie_eat;
    Dialog_Node Pie_Leave;

    Conditional_Node Pie_description_heatCondition;

    Destructor_Node Pie_eat_pie;

    ExecuteFunction_Node atePie;

    Conditional_Node Pie_dealt_with_janitor;

    // Start is called before the first frame update
    void Start()
    {
        fss = gameObject.GetComponent<First_Screen_States>();
        dm = gameObject.GetComponent<Dialog_manager>();
        am = gameObject.GetComponent<audio_Manager>();

        Pie_janitor_is_here1 = new Dialog_Node(Pie_text[0], null, false, dm);
        Pie_janitor_is_here2 = new Dialog_Node(Pie_text[1], null, false, dm);

        Pie_Janitor_Grunt = new Audio_Node("JAN_Disapproval", Pie_janitor_is_here2, am);

        Pie_description1 = new Dialog_Node(Pie_text[2], null, false, dm);
        Pie_description2 = new Dialog_Node(Pie_text[3], pie_options[0], true, dm);
        Pie_description3 = new Dialog_Node(Pie_text[4], pie_options[0], true, dm);
        Pie_eat = new Dialog_Node(Pie_text[5], null, false, dm);
        Pie_Leave = new Dialog_Node(Pie_text[6], null, false, dm);

        atePie = new ExecuteFunction_Node(fss.EatThePie, null);

        Pie_description_heatCondition = new Conditional_Node(fss.HasHeatedThePie, Pie_description2, Pie_description3);

        Pie_eat_pie = new Destructor_Node("Pie", atePie);

        Pie_janitor_is_here1.setNextNodes(Pie_Janitor_Grunt);
        Pie_description1.setNextNodes(Pie_description_heatCondition);
        Pie_description2.setNextNodes(Pie_eat, Pie_Leave);
        Pie_description3.setNextNodes(Pie_eat, Pie_Leave);
        Pie_eat.setNextNodes(Pie_eat_pie);

        Pie_dealt_with_janitor = new Conditional_Node(fss.HasDealtWithJanitor, Pie_description1, Pie_janitor_is_here1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public behavioural_Node getCurrentNode()
    {
        return Pie_dealt_with_janitor;
    }
}
