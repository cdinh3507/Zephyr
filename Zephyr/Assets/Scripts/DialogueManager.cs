using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    public Animator playerAnimator;

    private Queue<string>sentences;

    const float DEFAULTTYPINGSPEED = 0.05f;

    public float typingSpeed = DEFAULTTYPINGSPEED;

    private bool isTyping;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        isTyping = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                typingSpeed = 0;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Time.timeScale = 0;
        playerAnimator.SetBool("cutscene", true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        typingSpeed = DEFAULTTYPINGSPEED;
        foreach (char letter in sentence.ToCharArray())
        {
            isTyping = true;
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    } 

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Time.timeScale = 1;
        playerAnimator.SetBool("cutscene", false);
    }
}
