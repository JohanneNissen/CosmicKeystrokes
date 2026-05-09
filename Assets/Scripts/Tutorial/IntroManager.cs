using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Image robot;
    [SerializeField] Image background;
    [SerializeField] TMP_Text introtext;

    [SerializeField] GameObject keys;
    [SerializeField] Image key1;
    [SerializeField] Image key2;
    [SerializeField] Image key3;
    [SerializeField] Image key4;
    [SerializeField] Image key1Back;
    [SerializeField] Image key2Back;
    [SerializeField] Image key3Back;
    [SerializeField] Image key4Back;
    [SerializeField] TMP_Text letter1;
    [SerializeField] TMP_Text letter2;
    [SerializeField] TMP_Text letter3;
    [SerializeField] TMP_Text letter4;
    [SerializeField] Color pressedColor;

    string intro1 = "Velkommen til Cosmic Keystrokes. Jeg hedder Astro, og jeg er din Robo-Guide. Som du kan se, er der ikke så meget liv i dit rumskrib lige nu. Før vi kan komme videre skal vi have startet det op. Tryk ENTER for at gå i gang med opstarten";
    string intro2 = "Først skal vi have tændt for navigations systemet. Til det skal du bruge SHIFT, CTRL, ENTER og MELLEMRUM. På dit keyboard, tryk på de fire taster for at tænde navigationen.";
    string intro3 = "Du styrer sit rumskib med 10-fingersystemet. Hver finger har en farve, som svarer til en af farverne på tastaturet. Dine fingre må kun trykke på bogstaverne som har den samme farve, mens dine tommelfingre skal trykke på mellemrum.";
    string intro4 = "Din venstre hånd skal have fingerene sådan her: lillefinger på A, ringfinger på S, langfinger på D og pegefinger på F. Læg dine venstre fingre på tastaturet og tryk på A, S, D og F for at starte det venstre kontrolpanel.";
    string intro5 = "Din højre hånd skal have fingerne sådan her: lillefinger på Æ, ringfinger på L, langfinger på K og pegefinger på J. Læg dine højre fingre på tastaturet og tryk på J, K, L og Æ for at starte det højre kontrolpanel.";
    string intro6 = "Yay, nu er der gang i rumskibet igen. Hvis du glemmer hvordan du bruger 10-finger systemet kan du altid få introduktionen igen. Lad os se at komme hen til din første mission og se hvad du har lært. Tryk ENTER for at komme i gang!";

    public Sprite robotHappy;
    public Sprite robotNormal;
    public Sprite robotHello;
    public Sprite robotWarning;

    public Sprite backgroundS1;
    public Sprite backgroundS2;
    public Sprite backgroundS3;
    public Sprite backgroundS4;

    int introCount = 1;

    bool isPaused = true;
    bool key1Pressed = false;
    bool key2Pressed = false;
    bool key3Pressed = false;
    bool key4Pressed = false;

    bool justChangedState = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keys.gameObject.SetActive(false);
        ChangeState();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused && Input.GetKeyDown(KeyCode.Return))
        {
            introCount++;
            ChangeState();
            justChangedState = true;
        }

        if (justChangedState)
        {
            justChangedState = false;
            return;
        }

        if (!isPaused && introCount == 2)
        {
            if (key1Pressed == true &&  key2Pressed == true && key3Pressed == true && key4Pressed == true)
            {
                key1Pressed = false;
                key2Pressed = false;
                key3Pressed = false;
                key4Pressed = false;
                key1Back.color = Color.white;
                key2Back.color = Color.white;
                key3Back.color = Color.white;
                key4Back.color = Color.white;
                introCount++;
                ChangeState();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                { key1Pressed = true; key1Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.Return))
                { key2Pressed = true; key2Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.Space))
                { key3Pressed = true; key3Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                { key4Pressed = true; key4Back.color = pressedColor; }
        }

        if (!isPaused && introCount == 4)
        {
            if (key1Pressed == true && key2Pressed == true && key3Pressed == true && key4Pressed == true)
            {
                key1Pressed = false;
                key2Pressed = false;
                key3Pressed = false;
                key4Pressed = false;
                key1Back.color = Color.white;
                key2Back.color = Color.white;
                key3Back.color = Color.white;
                key4Back.color = Color.white;
                introCount++;
                ChangeState();
            }

            if (Input.GetKeyDown(KeyCode.A))
            { key1Pressed = true; key1Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.S))
            { key2Pressed = true; key2Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.D))
            { key3Pressed = true; key3Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.F))
            { key4Pressed = true; key4Back.color = pressedColor; }
        }

        if (!isPaused && introCount == 5)
        {
            if (key1Pressed == true && key2Pressed == true && key3Pressed == true && key4Pressed == true)
            {
                key1Pressed = false;
                key2Pressed = false;
                key3Pressed = false;
                key4Pressed = false;
                key1Back.color = Color.white;
                key2Back.color = Color.white;
                key3Back.color = Color.white;
                key4Back.color = Color.white;
                introCount++;
                ChangeState();
            }

            string input = Input.inputString;

            if (Input.GetKeyDown(KeyCode.J))
            { key1Pressed = true; key1Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.K))
            { key2Pressed = true; key2Back.color = pressedColor; }
            if (Input.GetKeyDown(KeyCode.L))
            { key3Pressed = true; key3Back.color = pressedColor; }
            if (input.Contains("æ") || input.Contains("Æ"))
            { key4Pressed = true; key4Back.color = pressedColor; }
        }
    }

    void ChangeState()
    {
        switch (introCount)
        {
            case 1:
                introtext.text = intro1;
                robot.sprite = robotHello;
                break;
            case 2:
                isPaused = false;
                introtext.text = intro2;
                robot.sprite = robotNormal;
                keys.gameObject.SetActive(true);
                SetKeysSpecial();
                break;
            case 3:
                isPaused = true;
                introtext.text = intro3;
                background.sprite = backgroundS2;
                keys.gameObject.SetActive(false);
                break;
            case 4:
                isPaused = false;
                introtext.text = intro4;
                keys.gameObject.SetActive(true);
                SetKeysLetter1();
                break;
            case 5:
                isPaused = false;
                background.sprite = backgroundS3;
                introtext.text = intro5;
                keys.gameObject.SetActive(true);
                SetKeysLetter2();
                break;
            case 6:
                isPaused = true;
                background.sprite = backgroundS4;
                introtext.text = intro6;
                keys.gameObject.SetActive(false);
                break;
            case 7:
                introCount = 0;
                SceneManager.LoadSceneAsync(1);
                break;
        }
    }

    void SetKeysSpecial()
    {
        key1.gameObject.SetActive(true);
        key2.gameObject.SetActive(true);
        key3.gameObject.SetActive(true);
        key4.gameObject.SetActive(true);
        letter1.gameObject.SetActive(false);
        letter2.gameObject.SetActive(false);
        letter3.gameObject.SetActive(false);
        letter4.gameObject.SetActive(false);
    }

    void SetKeysLetter1()
    {
        letter1.gameObject.SetActive(true);
        letter2.gameObject.SetActive(true);
        letter3.gameObject.SetActive(true);
        letter4.gameObject.SetActive(true);
        key1.gameObject.SetActive(false);
        key2.gameObject.SetActive(false);
        key3.gameObject.SetActive(false);
        key4.gameObject.SetActive(false);
        letter1.text = "A";
        letter2.text = "S";
        letter3.text = "D";
        letter4.text = "F";
    }

    void SetKeysLetter2()
    {
        letter1.gameObject.SetActive(true);
        letter2.gameObject.SetActive(true);
        letter3.gameObject.SetActive(true);
        letter4.gameObject.SetActive(true);
        key1.gameObject.SetActive(false);
        key2.gameObject.SetActive(false);
        key3.gameObject.SetActive(false);
        key4.gameObject.SetActive(false);
        letter1.text = "J";
        letter2.text = "K";
        letter3.text = "L";
        letter4.text = "Æ";
    }
}
