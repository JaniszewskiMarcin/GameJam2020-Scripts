using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogeEditor : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index = 0;
    public static int howManySentences = 0;
    public float typingSpeed;

    public GameObject continueButton;
    public static bool inConversation = false;

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            if(index <= sentences.Length - 1)
            {
                index++;
            }
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

            if (index  < howManySentences)
            {
                textDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                textDisplay.text = "";
                inConversation = false; 
                continueButton.SetActive(false);
            }
    }

    public void StartAgain()
    {
        StartCoroutine(Type());
    }
}
