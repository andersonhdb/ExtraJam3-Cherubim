using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_Level_Intro : MonoBehaviour
{
    Dialog_manager dm;

    string[] intro_text = {"You were invited to your work friend’s birthday celebration, but, well, work happened.",
        "A meeting ran late.  The elevator was slow.  You had to wipe the toilet paper off your face.  And so on and so forth.",
        "You arrived in the break room where you found no cake, but you instead found a pie!",
        "It’s guarded by a gruff-looking janitor who seems ready to eat it all.",
        "What do you do?"};

    Dialog_Node introNode;
    // Start is called before the first frame update
    void Start()
    {
        dm = gameObject.GetComponent<Dialog_manager>();
        introNode = new Dialog_Node(intro_text, null, false, dm);
        introNode.setNextNodes(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public behavioural_Node GetCurrentNode()
    {
        return introNode;
    }
}
