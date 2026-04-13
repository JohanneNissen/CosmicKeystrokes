using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject Intro1;
    public GameObject Intro2;
    public GameObject Intro3;
    public GameObject Intro4;
    public GameObject Intro5;
    public GameObject Intro6;
    public GameObject Intro7;
    public GameObject Intro8;
    public GameObject Intro9;
    public GameObject Intro10;
    public GameObject Intro11;

    public int current = 1;

    void Start()
    {
        Intro1.SetActive(true);
        Intro2.SetActive(false);
        Intro3.SetActive(false);
        Intro4.SetActive(false);
        Intro5.SetActive(false);
        Intro6.SetActive(false);
        Intro7.SetActive(false);
        Intro8.SetActive(false);
        Intro9.SetActive(false);
        Intro10.SetActive(false);
        Intro11.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            current++;
            switch (current)
            {
                case 1:
                    Intro1.SetActive(true);
                    break;
                case 2:
                    Intro1.SetActive(false);
                    Intro2.SetActive(true);
                    break;
                case 3:
                    Intro2.SetActive(false);
                    Intro3.SetActive(true);
                    break;
                case 4:
                    Intro3.SetActive(false);
                    Intro4.SetActive(true);
                    break;
                case 5:
                    Intro4.SetActive(false);
                    Intro5.SetActive(true);
                    break;
                case 6:
                    Intro5.SetActive(false);
                    Intro6.SetActive(true);
                    break;
                case 7:
                    Intro6.SetActive(false);
                    Intro7.SetActive(true);
                    break;
                case 8:
                    Intro7.SetActive(false);
                    Intro8.SetActive(true);
                    break;
                case 9:
                    Intro8.SetActive(false);
                    Intro9.SetActive(true);
                    break;
                case 10:
                    Intro9.SetActive(false);
                    Intro10.SetActive(true);
                    break;
                case 11:
                    Intro10.SetActive(false);
                    Intro11.SetActive(true);
                    break;
                case 12:
                    current = 1;
                    SceneManager.LoadSceneAsync(1);
                    break;
            }
        }
    }
}
