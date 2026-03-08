using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        currentWord = wordgenerator.GenerateWord(minCom, maxCom, usedwords);
        FocusInputField();
        StartCoroutine(PlayNextWord());
        gameRunning = true;
    }

    private void Update()
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
        if (gameRunning == false)
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
}
