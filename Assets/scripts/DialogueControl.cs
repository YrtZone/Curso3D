using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Text actorNameText;
    public Text SpeetchText;

    [Header("Variables")]
    public float TypingSpeed;
    private string[] sentences;
    private int index;
    private Coroutine typingCoroutine;

    void Update()
    {
        
    }

    public void Speech(string[] txt, string actorName)
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        dialogueObj.SetActive(true);
        SpeetchText.text = "";
        actorNameText.text = actorName;
        sentences = txt;
        index = 0;
        typingCoroutine = StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        SpeetchText.text = "";
        foreach(char letter in sentences[index].ToCharArray())
        {
            SpeetchText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
        typingCoroutine = null;
    }
    public void NextSentence()
    {
        if(SpeetchText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                SpeetchText.text ="";
                if(typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeSentence());
            }
            else
            {
                EndDialogue();
            }

        }

    }
    
    public void HidePanel()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        SpeetchText.text ="";
        actorNameText.text ="";
        index = 0;
        dialogueObj.SetActive(false);
    }
    
    public void EndDialogue()
    {

    }


}
