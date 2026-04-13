using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game3Manager : MonoBehaviour
{
    public KeyboardManager keyboardmanager;
    public FoodDispenser fooddispenser;
    public WordGenerator wordgenerator;

    public string currentword;
    private string typedword = "";
    private int comMin;
    private int comMax;
    public List<string> usedWords;
    private int cindex = 0;

    public TMP_Text robotText;
    public Image robot;
    public Image textbox;
    public Image keyboard;
    public TMP_Text display;
    public Image displayback;
    public Image diffPanel;
    public TMP_Text continuetext;

    public int introcount = 1;
    string intro1 = "Velkommen til rumstationens kantine. Vores astronauter skal have en god frokost, inden de bliver sendt på mission. Desværre er vores kok blevet syg med rum-kopper. Vi skal bruge din hjælp til at lave mad til astronauterne.";
    string intro2 = "Maden bliver lavet på vores mad-printer. Hver gang maskinen skal printe en frokost skal den bruge et ord for at starte produktionen. Desværre er maskinen gammel og kan kun tage et bogstav ad gangen.";
    string intro3 = "På skærmen kan du se, hvilket bogstav maskinen skal bruge. Tryk på bogstavet på dit keyboard. Når alle bogstaverne er sendt til maskinen, kan den printe en lækker rum frokost.";
    string intro4 = "Inden du starter, skal vi lige indstille maskinen. Tryk på 1, 2 eller 3 for at vælge en sværhedgrad at sætte maskinen på.";
    string intro5 = "Nu er maskinen klar til at printe frokost. Husk at bruge 10-finger systemet, og prøv ikke at kigge på dit keyboard. De bedste astronauter har et viskestykke over deres hænder. Tryk på ENTER, når du er klar til at starte.";
    string end1 = "Wow, sikke meget mad. Godt klaret! Her er hvordan du klarede dig: ";

    string normalCon = "Tryk på ENTER for at fortsætte";
    string diffCon = "Tryk på 1, 2 eller 3 for at fortsætte";

    int hit = 0;
    int miss = 0;
    float acc;
    int totalwords = 0;

    public float gameTime = 120f;
    public float currentTime;

    bool gameRunning;
    bool isTransitioning = false;

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
            if (Input.GetKeyDown(KeyCode.Return) && introcount != 4)
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
                        currentTime = gameTime;
                        gameRunning = true;
                        break;
                    case 7:
                        SceneManager.LoadSceneAsync(1);
                        break;
                }
            }

            if (introcount == 4)
            {
                diffPanel.gameObject.SetActive(true);
                continuetext.text = diffCon;
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    comMin = 10;
                    comMax = 25;
                    introcount = 5;
                    diffPanel.gameObject.SetActive(false);
                    continuetext.text = normalCon;
                    robotText.text = intro5;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    comMin = 25;
                    comMax = 40;
                    introcount = 5;
                    diffPanel.gameObject.SetActive(false);
                    continuetext.text = normalCon;
                    robotText.text = intro5;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    comMin = 40;
                    comMax = 70;
                    introcount = 5;
                    diffPanel.gameObject.SetActive(false);
                    continuetext.text = normalCon;
                    robotText.text = intro5;
                }
            }
        }

        if (gameRunning && !isTransitioning)
        {
            string input = Input.inputString;

            foreach (char c in input)
            {
                if (cindex < currentword.Length && char.ToUpper(c) == char.ToUpper(currentword[cindex]))
                {
                    string pressedkey = c.ToString().ToUpper();
                    hit++;

                    typedword += c;
                    display.text = typedword;
                    cindex++;
                    keyboardmanager.ResetKeys();

                    if (cindex < currentword.Length)
                    {
                        keyboardmanager.HighlightKey(currentword[cindex].ToString());
                    }

                    StartCoroutine(keyboardmanager.PulseKey(pressedkey));
                } else
                {
                    if (c == ' ')
                        continue;

                    miss++;
                }
            }

            if (typedword.ToLower() == currentword.ToLower())
            {
                totalwords++;
                StartCoroutine(GreenFlash(displayback));
                StartCoroutine(nextWord());
            }
        }

        if (gameRunning && !isTransitioning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f){
                EndGame();
            }
        }
    }

    IEnumerator nextWord()
    {
        isTransitioning = true;
        yield return new WaitForSeconds(0.12f);

        cindex = 0;
        typedword = "";
        display.text = typedword;
        keyboardmanager.ResetKeys();
        currentword = wordgenerator.GenerateWord(comMin, comMax, usedWords);
        keyboardmanager.HighlightKey(currentword[cindex].ToString());
        fooddispenser.addTray();

        isTransitioning = false;
    }

    public IEnumerator GreenFlash(Image img)
    {
            Color original = img.color;
            img.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            img.color = original;
    }

    void CalculateAcc()
    {
        int total = hit + miss;

        if (total == 0)
        {
            Debug.Log("no hits yet");
        }

        acc = ((float)hit / total) * 100f;

        Debug.Log("Accuracy: " + acc);
    }

    void EndGame()
    {
        gameRunning = false;
        CalculateAcc();
        keyboard.gameObject.SetActive(false);
        robot.gameObject.SetActive(true);
        textbox.gameObject.SetActive(true);
        robotText.text = end1 + acc + "% accuracy og " + totalwords + " ord skrevet. Tryk på ENTER for at gå tilbage til centralen.";
    }
}
