using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game3Manager : MonoBehaviour
{
    public KeyboardManager keyboardmanager;

    public WordGenerator wordgenerator;
    public string currentword;
    public string typedword = "";
    public int comMin;
    public int comMax;
    public List<string> usedWords;
    private int cindex = 0;

    public TMP_Text robotText;
    public Image robot;
    public Image textbox;
    public Image keyboard;

    int introcount = 1;
    string intro1 = "Velkommen til rumstationens kantine. Vores astronauter skal have en god frokost, inden de bliver sendt på mission. Desværre er vores kok blevet syg med rum-kopper. Vi skal bruge din hjælp til at lave mad til astronauterne.";
    string intro2 = "Maden bliver lavet på vores mad-printer. Hver gang maskinen skal printe en frokost skal den bruge et ord for at starte produktionen. Desværre er maskinen er gammel og kan kun tage et bogstav ad gangen.";
    string intro3 = "På skærmen kan du se, hvilket bogstav maskinen skal bruge. Tryk på bogstavet på dit keyboard. Når alle bogstaverne er sendt til maskinen, kan den printe en lækker rum frokost.";
    string intro4 = "Inden du starter, skal vi lige indstille maskinen. Tryk på 1, 2 eller 3 for at vælge en sværhedgrad at sætte maskinen på.";
    string intro5 = "Nu er maskinen klar til at printe frokost. Husk at bruge 10-finger systemet, og prøv ikke at kigge på dit keyboard. De bedste astronauter har et viskestykke over deres hænder. Tryk på mellemrum, når du er klar til at starte.";

    bool gameRunning;

    void Start()
    {
        gameRunning = false;
        keyboardmanager.ResetKeys();
        keyboard.gameObject.SetActive(false);
        robotText.text = intro1;
    }


    void Update()
    {
        if (!gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Space) && introcount != 4)
            {
                introcount++;
                switch (introcount)
                {
                    case 1:
                        robotText.text = intro1;
                        break;
                    case 2:
                        robotText.text = intro2;
                        break;
                    case 3:
                        robotText.text = intro3;
                        break;
                    case 4:
                        robotText.text = intro4;
                        break;
                    case 5:
                        robotText.text = intro5;
                        break;
                    case 6:
                        keyboard.gameObject.SetActive(true);
                        robot.gameObject.SetActive(false);
                        textbox.gameObject.SetActive(false);
                        currentword = wordgenerator.GenerateWord(comMin, comMax, usedWords);
                        keyboardmanager.HighlightKey(currentword[cindex].ToString());
                        gameRunning = true;
                        break;
                }
            }

            if (introcount == 4)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    comMin = 10;
                    comMax = 25;
                    introcount = 5;
                    robotText.text = intro5;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    comMin = 25;
                    comMax = 40;
                    introcount = 5;
                    robotText.text = intro5;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    comMin = 40;
                    comMax = 70;
                    introcount = 5;
                    robotText.text = intro5;
                }
            }
        }

        if (gameRunning)
        {
            string input = Input.inputString;

            foreach (char c in input)
            {
                if (cindex < currentword.Length && char.ToUpper(c) == char.ToUpper(currentword[cindex]))
                {
                    typedword += c;
                    cindex++;
                    keyboardmanager.ResetKeys();
                    if (cindex < currentword.Length)
                    {
                        keyboardmanager.HighlightKey(currentword[cindex].ToString());
                    }
                }
            }

            if (typedword.ToLower() == currentword.ToLower())
            {
                cindex = 0;
                typedword = "";
                keyboardmanager.ResetKeys();
                currentword = wordgenerator.GenerateWord(comMin, comMax, usedWords);
                keyboardmanager.HighlightKey(currentword[cindex].ToString());
            }
        }
    }
}
