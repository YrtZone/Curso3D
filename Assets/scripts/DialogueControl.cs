using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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



    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
}
