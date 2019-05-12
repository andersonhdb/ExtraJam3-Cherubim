using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    string[][] door_text =
    {
        new string []
        {
            "This door is the only known way to this office’s break room."
        },
        new string[]
        {
            "Now that you have eaten pie, this door is the only known exit to this office’s break room.  You are convinced that today was a good day to pie.",
        },
        new string[]
        {
            "You’re here to eat, not quit!"
        }
    };

    behavioural_Node currentNode;

    First_Screen_States fss;
    Dialog_manager dm;

    Dialog_Node doorIntro;
    Dialog_Node doorNoPie;
    Dialog_Node doorAtePie;
    Conditional_Node Ate_The_Pie;

    SceneChanger_Node toWin;

    // Start is called before the first frame update
    void Start()
    {
        fss = gameObject.GetComponent<First_Screen_States>();
        dm = gameObject.GetComponent<Dialog_manager>();
        doorIntro = new Dialog_Node(door_text[0], null, false, dm);
        doorAtePie = new Dialog_Node(door_text[1], null, false, dm);
        doorNoPie = new Dialog_Node(door_text[2], null, false, dm);

        Ate_The_Pie = new Conditional_Node(fss.HasAtePie, doorAtePie, doorNoPie);

        toWin = new SceneChanger_Node(2);

        doorIntro.setNextNodes(Ate_The_Pie);
        doorAtePie.setNextNodes(toWin);
        doorNoPie.setNextNodes(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public behavioural_Node GetCurrentNode()
    {
        return doorIntro;
    }
}
