using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


    public interface behavioural_Node
    {
        behavioural_Node Run();
    }



    //Class list:
    // 1. Conditional_Node
    // 2. Fade_In_Node
    // 3. Fade_Out_Node
    // 4. Destructor Node


    //------------------------------------------
    //Class Conditional Node
    //------------------------------------------


    public class Conditional_Node : behavioural_Node
    {
        public delegate bool conditional_Callback();
      
        private bool condition;
        conditional_Callback getBoolValue;
        private behavioural_Node falsecondition;
        private behavioural_Node trueCondition;
        private bool usingCallback = false;

        public Conditional_Node(in bool cond, behavioural_Node truec, behavioural_Node falsec)
        {
            condition = cond;
            falsecondition = falsec;
            trueCondition = truec;
            usingCallback = false;
        }

        public Conditional_Node(conditional_Callback getValue, behavioural_Node truec, behavioural_Node falsec)
        {
            getBoolValue = getValue;
            falsecondition = falsec;
            trueCondition = truec;
            usingCallback = true;
        }

        public behavioural_Node Run()
        {
            if (usingCallback)
            {
                if (getBoolValue())
                {
                    return trueCondition;
                }
                return falsecondition;
            }
            if (condition)
            {
                return trueCondition;
            }
            return falsecondition;
        }
    }



    //------------------------------------------
    //Class Fade_In_Node
    //------------------------------------------

    public class Fade_In_Node : behavioural_Node
    {
        private Image fadePannel;
        private float fadeTime;
        private behavioural_Node nextNode;

        private static bool start = true;
        private static bool during = false;
        private static bool end = false;
        private static float fadeTimeCounter = 0f;

        public Fade_In_Node(Image fadeP, float fadeT, behavioural_Node nxt)
        {
            fadePannel = fadeP;
            fadeTime = fadeT;
            nextNode = nxt;
        }

        public behavioural_Node Run()
        {
            Color col = fadePannel.color;
            if (start)
            {
                col.a = 0;
                fadePannel.color = col;
                start = false;
                during = true;
                fadeTimeCounter = 0;
            }
            else if (during)
            {
                fadeTimeCounter = fadeTimeCounter + Time.deltaTime;
                col.a = fadeTimeCounter / fadeTime;
                fadePannel.color = col;
                if (fadeTimeCounter >= fadeTime)
                {
                    during = false;
                    end = true;
                }
            }
            else if (end)
            {
                col.a = 1;
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

//------------------------------------------
//Class Fade_Out_Node
//------------------------------------------

public class Fade_Out_Node : behavioural_Node
    {
        private Image fadePannel;
        private float fadeTime;
        private behavioural_Node nextNode;

        private static bool start = true;
        private static bool during = false;
        private static bool end = false;
        private static float fadeTimeCounter = 0f;

        public Fade_Out_Node(Image fadeP, float fadeT, behavioural_Node nxt)
        {
            fadePannel = fadeP;
            fadeTime = fadeT;
            nextNode = nxt;
        }

        public behavioural_Node Run()
        {
            
            Color col = fadePannel.color;
            if (start)
            {
                col.a = 1f;
                fadePannel.color = col;
                start = false;
                during = true;
                fadeTimeCounter = 0;
            }
            else if (during)
            {
                fadeTimeCounter = fadeTimeCounter + Time.deltaTime;
                col.a = 1f - fadeTimeCounter / fadeTime;
                fadePannel.color = col;
                if (fadeTimeCounter >= fadeTime)
                {
                    during = false;
                    end = true;
                }
            }
            else if (end)
            {
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

    //------------------------------------------
    //Class Destructor Node
    //------------------------------------------

    public class Destructor_Node : behavioural_Node
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


    //------------------------------------------
    //Class Audio Node
    //------------------------------------------

    public class Audio_Node : behavioural_Node
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




    //------------------------------------------
    //Class Dialog Node
    //------------------------------------------

    public class Dialog_Node : behavioural_Node
    {
        private string[] dialogSequence;
        private string[] options;
        private bool hasOptions;
        private List<behavioural_Node> nextDialog;
        private Dialog_manager dm;
        private UnityAction[] chosenOption = new UnityAction[] { option0, option1, option2, option3 };

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
            if (hasopt)
            {
                Button[] buts = dm.SendButtonReference();
                for (int i = 0; i < 4; i++)
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
            if (optionSelection == -1)
            {
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
                            if (dialog_index == dialogSequence.Length && hasOptions)
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
            if (nextDialog.Count == 0)
            {
                nextDialog.Add(option1);
            }
            if (nextDialog.Count == 1 && nextDialog[0] == null)
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




    //------------------------------------------
    //Class Movement Node
    //------------------------------------------

    public class Movement_Node : behavioural_Node
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

public class SceneChanger_Node : behavioural_Node
{
    private int sceneIndex = 0;
    private string sceneName;
    private bool nextSceneByIndex = false;

    public SceneChanger_Node(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
        nextSceneByIndex = true;
    }

    public SceneChanger_Node(string sceneName)
    {
        this.sceneName = sceneName;
        nextSceneByIndex = false;
    }

    public behavioural_Node Run()
    {
        if (nextSceneByIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        return null;
    }
}

public class ExecuteFunction_Node : behavioural_Node
{
    public delegate void externalFunction();

    behavioural_Node nextNode;
    externalFunction toExecute;

    public ExecuteFunction_Node(externalFunction func, behavioural_Node next)
    {
        toExecute = func;
        nextNode = next;
    }

    public behavioural_Node Run()
    {
        toExecute();
        return nextNode;
    }
}


