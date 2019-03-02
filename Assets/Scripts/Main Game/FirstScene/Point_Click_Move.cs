using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Point_Click_Move : MonoBehaviour
{
    NavMeshAgent player;
    [SerializeField]
    First_Screen_States gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && gameManager.MayRoam())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    //Debug.Log("clicked on Player");
                    gameManager.ToSelf();
                }
                else
                {
                    player.SetDestination(hit.point);
                }
            }
        }
    }
}
