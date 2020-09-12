using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{

    DialogueTrigger dt;
    public Dialogue dialogue;
    private bool touchPlayer;

    public PlayerController player; 


    // Start is called before the first frame update
    void Start()
    {
        dt = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && touchPlayer && player.GetGrounded())
        {
            Debug.Log("trigger");
            dt.TriggerDialogue();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        touchPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        touchPlayer = false;
    }

}
