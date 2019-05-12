using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Point_Click_Move : MonoBehaviour
{
    NavMeshAgent player;
    [SerializeField]
    First_Screen_States gameManager;
    Animator anim;
    AudioSource footsteps;
    float step_counter = 1;
    float step_delay = 0.3f;
    GameObject lastClicked = null;
    bool hasInteracted = true;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        footsteps = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameManager.MayRoam())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                lastClicked = hit.collider.gameObject;
                Debug.Log(lastClicked.tag);
                if(lastClicked.tag == "Player")
                {
                    //Debug.Log("clicked on Player");
                    gameManager.ToSelf();
                }
                if(lastClicked.tag == "Interactable")
                {
                    player.SetDestination(hit.point);
                    hasInteracted = false;
                }
                else
                {
                    player.SetDestination(hit.point);
                    lastClicked = null;
                }
            }
        }

        if (player.remainingDistance > player.stoppingDistance)
        {
            anim.SetBool("walking", true);
            if (!footsteps.isPlaying && step_counter > step_delay)
            {
                footsteps.Play();
                step_counter = 0;
            }

            else if (footsteps.isPlaying)
            {
                step_counter = 0;
            }

            else
            {
                step_counter = step_counter + Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("walking", false);
            footsteps.Stop();
            step_counter = 1;
            if(!hasInteracted && lastClicked != null)
            {
                lastClicked.GetComponent<Interactible_Script>().Interact();
                hasInteracted = true;
                lastClicked = null;
            }
        }
    }
}
