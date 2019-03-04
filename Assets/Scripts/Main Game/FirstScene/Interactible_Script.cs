using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Script : MonoBehaviour
{
    First_Screen_States gameManager;
    [SerializeField]
    private string interaction;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<First_Screen_States>();
    }

    public void Interact()
    {
        gameManager.SetState(interaction);
    }
}
