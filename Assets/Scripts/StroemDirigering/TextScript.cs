using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public WordGenerator generator;
    public SparkController sparkcontroller;

    public List<string> usedWords;

    public TMP_Text textUP;
    public TMP_Text textDown;
    public TMP_Text textLeft;
    public TMP_Text textRight;

    public class DirectionWord { public string word; public TMP_Text textbox;}

    public DirectionWord Up;
    public DirectionWord Down;
    public DirectionWord Left;
    public DirectionWord Right;

    public TMP_InputField input;

    string typedWord; 

    private void Awake()
    {
        input.onValidateInput += ValidateChar;
    }


    void Start()
    {
        Up = new DirectionWord();
        Down = new DirectionWord();
        Left = new DirectionWord();
        Right = new DirectionWord();

        Up.textbox = textUP;
        Down.textbox = textDown;
        Left.textbox = textLeft;
        Right.textbox = textRight;

        GenerateWord();


    }

    
    void Update()
    {
     
        if(!input.isFocused)
        {
            FocusInputField();
        }

        if (EventSystem.current.currentSelectedGameObject == input.gameObject && Input.GetKeyDown(KeyCode.Space))
        {
            SubmiteInput();
            if (typedWord == Up.word)
            {
                sparkcontroller.MoveUp();
                GenerateWord();
            }
            else if (typedWord == Down.word)
            {
                sparkcontroller.MoveDown();
                GenerateWord();
            }
            else if (typedWord == Left.word)
            {
                sparkcontroller.MoveLeft();
                GenerateWord();
            }
            else if (typedWord == Right.word)
            {
                sparkcontroller.MoveRight();
                GenerateWord();
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

    void SubmiteInput()
    {
        typedWord = input.text;
        input.text = "";
    }


    void FocusInputField()
    {
        input.ActivateInputField();
        input.Select();
    }

    public void GenerateWord()
    {
        Up.word = generator.GenerateWord(10, 70, usedWords);
        Down.word = generator.GenerateWord(10, 70, usedWords);
        Left.word = generator.GenerateWord(10, 70, usedWords);
        Right.word = generator.GenerateWord(10, 70, usedWords);

        Up.textbox.text = Up.word;
        Down.textbox.text = Down.word;
        Left.textbox.text = Left.word;
        Right.textbox.text = Right.word;

    }
}
