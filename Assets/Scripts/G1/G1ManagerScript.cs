using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class G1ManagerScript : MonoBehaviour
{
    
    public AudioSource source;
    public AudioClip nextwordAU;
    public AudioClip currentwordAU;

    public ScrollUV starfieldFront;
    public G1Timer timer;
    public G1DistanceCounter DistanceCounter;
    public ParticleSystem flameEffect;
    private ParticleSystem.MainModule flameMain;

    public WordGenerator wordgenerator;
    public HashTest hashtest;
    public string currentWord;
    private string typedword;
    public int minCom;
    public int maxCom;
    public List<string> usedwords;

    public Image Robot;
    public TMP_Text introText;
    public Sprite RoboHello;
    public Sprite RoboNormal;
    public Sprite RoboHappy;
    int introCount = 1;
    string intro1 = "Velkommen! Kommandocentralen har inviteret dig til Rum-Kapflyvning! Det går ud på at flyve så mange lysår ud i rummet, som du kan, inden tiden løber ud.";
    string intro2 = "For at flyve hurtigere, skal du booste dit rumskibs motor ved at de ord, som rumskibet beder dig om. Ordene vil blive læst højt og så er det dit job at stave og sende dem til motoren.";
    string intro3 = "Når du har skrevet ordet, som er blevet læst højt, så tryk på MELLEMRUM for at sende det til motoren. Hvis ordet er rigtigt får du er boost!";
    string intro4 = "Hvis du ikke hørte ordet, kan du trykke på SHIFT for at høre det igen. Hvis du vil have et andet ord så tryk på CONTROL, så sender rumskibet et nyt ord til dig.";
    string intro5 = "Husk at have lyd på, ellers bliver det svært at høre, hvilket ord, du skal skrive. Held og lykke pilot! Tryk på MELLEMRUM, når du er klar til at starte og lad os se, hvor langt du kan flyve!";

    public TMP_InputField inputField;
    public bool gameRunning;

    private void Awake()
    {
        hashtest = gameObject.GetComponent<HashTest>();
        wordgenerator = gameObject.GetComponent<WordGenerator>();
        source = gameObject.GetComponent<AudioSource>();

        inputField.onValidateInput += ValidateChar;

        if (flameEffect != null)
            flameMain = flameEffect.main;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!gameRunning && Input.GetKeyDown(KeyCode.Space))
        {
            introCount++;
            switch (introCount)
            {
                case 1:
                    introText.text = intro1;
                    break;
                case 2:
                    Robot.GetComponent<Image>().sprite = RoboNormal;
                    introText.text = intro2;
                    break;
                case 3:
                    introText.text = intro3;
                    break;
                case 4:
                    introText.text = intro4;
                    break;
                case 5:
                    introText.text = intro5;
                    Robot.GetComponent<Image>().sprite = RoboHappy;
                    break;
                case 6:
                    StartGame();
                    break;
            }
        }

        if (gameRunning)
        {
            if (!inputField.isFocused)
            {
                FocusInputField();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (gameRunning == false)
                {
                    return;
                }
                ReplayCurrentWord();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                if (gameRunning == false)
                {
                    return;
                }
                currentWord = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
                StartCoroutine(PlayNextWord());
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SubmitInput();
                if (typedword == currentWord)
                {
                    Debug.Log("word was correct");
                    IncreaseSpeed();
                    if (maxCom < 60)
                    {
                        minCom++;
                        maxCom++;
                    }
                    currentWord = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
                    StartCoroutine(PlayNextWord());
                }
                else
                {
                    Debug.Log("incorrect word");
                    DecreaseSpeed();
                    if (minCom >= 10)
                    {
                        minCom--;
                        maxCom--;
                    }
                    currentWord = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
                    StartCoroutine(PlayNextWord());
                }
            }
        }
        if (!gameRunning)
        {
            inputField.gameObject.SetActive(false);
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
    void FocusInputField()
    {
        inputField.ActivateInputField();
        inputField.Select();
    }

    void IncreaseSpeed()
    {
        DistanceCounter.countSpeed = DistanceCounter.countSpeed + 11f;
        starfieldFront.parralax = starfieldFront.parralax - 0.2f;
        if (starfieldFront.parralax <= 2f)
        {
            starfieldFront.parralax = 2f;
        }

        if (starfieldFront.parralax >= 12f)
        {
            starfieldFront.parralax = 8;
        }

    }

    void DecreaseSpeed()
    {
        DistanceCounter.countSpeed = DistanceCounter.countSpeed - 11f;
        starfieldFront.parralax = starfieldFront.parralax + 0.2f;

        if (starfieldFront.parralax >= 12f)
        {
            starfieldFront.parralax = 12f;
        }
    }


    IEnumerator PlayNextWord()
    {
        /*source.clip = nextwordAU;
        source.Play();*/
        yield return new WaitForSeconds(.2f);
        currentwordAU = hashtest.GetAudio(currentWord);
        source.clip = currentwordAU;

        source.Play();
    }


    void ReplayCurrentWord()
    {
        source.clip = currentwordAU;
        source.Play();
    }

    void StartGame()
    {
        Robot.gameObject.SetActive(false);
        inputField.gameObject.SetActive(true);
        gameRunning = true;
        currentWord = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
        FocusInputField();
        StartCoroutine(PlayNextWord());
    }
}
