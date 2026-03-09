using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game2Manager : MonoBehaviour
{
    public WordGenerator wordgenerator;
    colorWord[] colorwords;
    public int minCom;
    public int maxCom;
    public List<string> usedwords;

    public bool gameRunning;
    public TMP_Text endText;
    public TMP_Text instruks;

    public TMP_Text yellowText;
    public TMP_Text blueText;
    public TMP_Text greenText;
    public TMP_InputField inputField;

    colorWord yellow;
    colorWord blue;
    colorWord green;

    private string typedword = "";

    ObstacleManager[] obstacles;

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
        endText.gameObject.SetActive(false);
        gameRunning = true;

        colorwords = new colorWord[3] { yellow, blue, green };
        foreach (var cw in colorwords)
        {
            cw.word = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
            cw.textBox.text = cw.word;
        }

        obstacles = FindObjectsByType<ObstacleManager>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inputField.isFocused)
        {
            FocusInputField();
        }

        if (gameRunning == false)
        {
            yellowText.gameObject.SetActive(false);
            blueText.gameObject.SetActive(false);
            greenText.gameObject.SetActive(false);
            inputField.gameObject.SetActive(false);
            instruks.gameObject.SetActive(false);
            endText.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && gameRunning == false)
        {
            SceneManager.LoadSceneAsync(1);
        }

        if (EventSystem.current.currentSelectedGameObject == inputField.gameObject && Input.GetKeyDown(KeyCode.Space))
        {
            SubmitInput();
            if (typedword == yellow.word)
            {
                MoveObstacles(ObstacleManager.GroupColor.Yellow);
                yellow.word = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
                yellow.textBox.text = yellow.word;
            }
            else if (typedword == blue.word)
            {
                MoveObstacles(ObstacleManager.GroupColor.Blue);
                blue.word = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
                blue.textBox.text = blue.word;
            }
            else if (typedword == green.word)
            {
                MoveObstacles(ObstacleManager.GroupColor.Green);
                green.word = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
                green.textBox.text = green.word;
            }
            else
            {
                Debug.Log("The word was not correct");
            }
        }
    }

    public void MoveObstacles(ObstacleManager.GroupColor color)
    {
        foreach (ObstacleManager obj in obstacles)
        {
            if (obj.group == color)
            {
                obj.ObsMoveStart();
            }
        }
    }

    void FocusInputField()
    {
        inputField.ActivateInputField();
        inputField.Select();
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

    /*void GetSingleWord(colorWord cw, string[] words)
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
    }*/
}
