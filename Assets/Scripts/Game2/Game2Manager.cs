using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game2Manager : MonoBehaviour
{
    string[] temp = new string[15] {"hej", "mor", "far", "nej", "skole", "job", "cykel", "hund", "kat", "hest", "ged", "gris", "får", "kanin", "fugl"};
    colorWord[] colorwords;

    public TMP_Text yellowText;
    public TMP_Text blueText;
    public TMP_Text greenText;
    public TMP_InputField input;

    colorWord yellow;
    colorWord blue;
    colorWord green;

    //int currentWaypoint = 0;

    public class colorWord
    {
        public string word;
        public TMP_Text textBox;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (input.text == yellow.word)
            {
                Debug.Log("yellow word was correct");
            }
            else if (input.text == blue.word)
            {
                Debug.Log("Blue word was correct");
            }
            else if (input.text == green.word)
            {
                Debug.Log("Green word was correct");
            }
            else
            {
                Debug.Log("The word was not correct");
            }
            
            GetNewWords(temp);
        }
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
