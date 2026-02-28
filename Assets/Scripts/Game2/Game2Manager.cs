using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Game2Manager : MonoBehaviour
{
    string[] temp = new string[15] {"hej", "mor", "far", "nej", "skole", "job", "cykel", "hund", "kat", "hest", "ged", "gris", "får", "kanin", "fugl"};
    colorWord[] colorwords;

    public TMP_Text yellowText;
    public TMP_Text blueText;
    public TMP_Text greenText;
    public TMP_InputField inputField;

    colorWord yellow;
    colorWord blue;
    colorWord green;

    private string typedword = "";

    public class colorWord
    {
        public string word;
        public TMP_Text textBox;
    }

    void Awake()
    {
        inputField.onValidateInput += ValidateChar;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yellow = new colorWord();
        blue = new colorWord();
        green = new colorWord();

        yellow.textBox = yellowText;
        blue.textBox = blueText;
        green.textBox = greenText;

        colorwords = new colorWord[3] { yellow, blue, green };

        GetNewWords(temp);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == inputField.gameObject && Input.GetKeyDown(KeyCode.Space))
        {
            SubmitInput();
            if (typedword == yellow.word)
            {
                Debug.Log("yellow word was correct");
                GetSingleWord(yellow, temp);
            }
            else if (typedword == blue.word)
            {
                Debug.Log("Blue word was correct");
                GetSingleWord(blue, temp);
            }
            else if (typedword == green.word)
            {
                Debug.Log("Green word was correct");
                GetSingleWord(green, temp);
            }
            else
            {
                Debug.Log("The word was not correct");
            }
        }
    }

    private char ValidateChar(string text, int charindex, char addchar)
    {
        if (addchar == ' ')
        {
            return '\0';
        }
        
        return addchar;
    }
    void SubmitInput()
    {
        typedword = inputField.text;
        inputField.text = "";
    }

    void GetSingleWord(colorWord cw, string[] words)
    {
        string chosenWord;
        do
        {
            int num = UnityEngine.Random.Range(0, words.Length);
            chosenWord = words[num];
        } while (chosenWord == yellow.word || chosenWord == blue.word || chosenWord == green.word);

        cw.textBox.text = chosenWord;
        cw.word = chosenWord;
    }
    void GetNewWords(string[] words)
    {
        List<string> usedwords = new List<string>();
        foreach (colorWord cw in colorwords) {
            string chosenWord;
            do
            {
                int num = UnityEngine.Random.Range(0, words.Length);
                chosenWord = words[num];
            }
            while (usedwords.Contains(chosenWord));

            cw.textBox.text = chosenWord;
            cw.word = chosenWord;
            usedwords.Add(chosenWord);
        }
    }
}
