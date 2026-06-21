using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Game3Manager : MonoBehaviour
{
    public KeyboardManager keyboardmanager;
    public FoodDispenser fooddispenser;
    public WordGenerator wordgenerator;

    string currentword;
    string typedword = "";
    int comMin;
    int comMax;
    public List<string> usedWords;
    int cindex = 0;

    //Keyboard Interface
    public TMP_Text robotText;
    public UnityEngine.UI.Image robot;
    public UnityEngine.UI.Image textbox;
    public UnityEngine.UI.Image keyboard;
    public TMP_Text display;
    public UnityEngine.UI.Image displayback;
    public UnityEngine.UI.Image diffPanel;
    public TMP_Text continuetext;
    public TMP_Text BigDisplaytext;
    public UnityEngine.UI.Image BigDisplayback;
    public TMP_Text TimeDisplay;

    //Sound
    public AudioSource BackgroundSource;
    public AudioSource SFXSource;
    public AudioClip backgroundMusic;
    public AudioClip menuMusic;
    public AudioClip keyWhosh;
    public AudioClip bellDing;
    bool musicPlaying = false;

    //LetterAnimation
    public GameObject flyingLetterPrefab;
    public Transform flyingLetterParent;
    public Transform targetFoodDispensor;

    //Intro
    int introcount = 1;
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

    bool gameRunning = false;
    bool isTransitioning = false;

    void Start()
    {
        SFXSource.volume = 0.8f;
        BackgroundSource.volume = 0.05f;
        BackgroundSource.loop = true;
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
                        BigDisplaytext.text = currentword;
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
                    setDiff1();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    setDiff2();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    setDiff3();
                }
            }
        }

        if (gameRunning)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            TimeDisplay.text = $"{minutes:00}:{seconds:00}";
        }

        if (gameRunning && !musicPlaying)
        {
            BackgroundSource.volume = 0.2f;
            BackgroundSource.clip = backgroundMusic;
            BackgroundSource.Play();
            musicPlaying = true;
        }

        if (!gameRunning && musicPlaying)
        {
            BackgroundSource.volume = 0.05f;
            BackgroundSource.clip = menuMusic;
            BackgroundSource.Play();
            musicPlaying = false;
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
                    SpawnFlyingLetter(c, keyboardmanager.GetKeyTransform(pressedkey));
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
                SFXSource.PlayOneShot(bellDing);
                StartCoroutine(GreenFlash(displayback));
                StartCoroutine(GreenFlash(BigDisplayback));
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
        BigDisplaytext.text = currentword;
        keyboardmanager.HighlightKey(currentword[cindex].ToString());
        fooddispenser.addTray();

        isTransitioning = false;
    }

    public IEnumerator GreenFlash(UnityEngine.UI.Image img)
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

        //Debug.Log("Accuracy: " + acc);
    }

    public void setDiff1()
    {
        comMin = 10;
        comMax = 25;
        introcount = 5;
        continueFromDiff();
    }

    public void setDiff2()
    {
        comMin = 25;
        comMax = 40;
        introcount = 5;
        continueFromDiff();
    }

    public void setDiff3()
    {
        comMin = 40;
        comMax = 70;
        introcount = 5;
        continueFromDiff();
    }

    void continueFromDiff()
    {
        diffPanel.gameObject.SetActive(false);
        continuetext.text = normalCon;
        robotText.text = intro5;
    }


    public void SpawnFlyingLetter(char letter, Transform keyTransform)
    {
        GameObject obj = Instantiate(flyingLetterPrefab, keyTransform.position, Quaternion.identity, flyingLetterParent);
        LetterThrower throwScript = obj.GetComponent<LetterThrower>();
        throwScript.Init(letter, targetFoodDispensor);
        SFXSource.PlayOneShot(keyWhosh);
    }
    void EndGame()
    {
        gameRunning = false;
        CalculateAcc();
        keyboard.gameObject.SetActive(false);
        robot.gameObject.SetActive(true);
        textbox.gameObject.SetActive(true);

        HighScoreTracker.HSTInstance.Minigame3Acc.LastScore = Mathf.RoundToInt(acc);
        if (HighScoreTracker.HSTInstance.Minigame3Acc.LastScore > HighScoreTracker.HSTInstance.Minigame3Acc.Highscore)
        {
            HighScoreTracker.HSTInstance.Minigame3Acc.Highscore = HighScoreTracker.HSTInstance.Minigame3Acc.LastScore;
        }
        
        HighScoreTracker.HSTInstance.Minigame3Words.LastScore = totalwords;
        if (HighScoreTracker.HSTInstance.Minigame3Words.LastScore > HighScoreTracker.HSTInstance.Minigame3Words.Highscore)
        {
            HighScoreTracker.HSTInstance.Minigame3Words.Highscore = HighScoreTracker.HSTInstance.Minigame3Words.LastScore;
        }

        robotText.text = end1 + $"{acc:F0}% accuracy og " + totalwords + " ord skrevet. Tryk på ENTER for at gå tilbage til centralen.";
    }
}
