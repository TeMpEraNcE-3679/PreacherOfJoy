using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TypeWritter : MonoBehaviour
{
    public Text uiText; 
    [TextArea(1,10)]
    public string[] sentences;
    public float typingSpeed = 0.03f;

    private int currentSentenceIndex = 0;
    private bool isTyping = false; 
    private bool sentenceCompleted = false; 
    private Coroutine typingCoroutine;

    public bool isGameStart;
    public UnityEvent startTriggerEvent;
    
    void Start()
    {
        if (sentences.Length > 0)
        {
            typingCoroutine = StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                CompleteCurrentSentence();
            }
            else if (sentenceCompleted)
            {
                NextSentence();
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        uiText.text = "";
        isTyping = true;
        sentenceCompleted = false;

        foreach (char letter in sentence.ToCharArray())
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        sentenceCompleted = true;
    }

    void CompleteCurrentSentence()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        uiText.text = sentences[currentSentenceIndex];
        isTyping = false;
        sentenceCompleted = true;
    }

    
    void NextSentence()
    {
        sentenceCompleted = false;
        
        if (currentSentenceIndex < sentences.Length - 1)
        {
            currentSentenceIndex++;
            typingCoroutine = StartCoroutine(TypeSentence(sentences[currentSentenceIndex]));
        }
        else if(isGameStart)
        {
            GameManager.S.state = GameManager.GameState.InGame;
            GUIManager.S.UpdateState();
            GameManager.S.ActivatePlayer(); 
            startTriggerEvent?.Invoke();
        }
        else
        {
            startTriggerEvent?.Invoke();
        }
    }
}
