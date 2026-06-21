using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    [SerializeField] Image robot;
    [SerializeField] TMP_Text introtext;

    int introCount = 1;

    string intro1 = "Velkommen til Strøm Dirigering. Rumstationen har haft strømafbrydelse, og vi har brug for din hjælp til at tænde strømmen igen. For at gøre det skal du dirigerer elektrisiteten hen til det grønne felt.";
    string intro2 = "For at styre strømmen, skal du skrive ordene som passer til den retning, du vil flytte strømmen i. Tryk MELLEMRUM når du har skrevet ordet, for at flytte strømmen.";
    string intro3 = "Når du flytter strømmen fortsætter den i den retning du har valgt ind til den rammer noget, som stopper den. De røde bokse er vægge, som stopper strømmen foran sig. De orange felter stopper strømmen ovenpå sig.";
    string intro4 = "Skriv ordene for at flytte strømmen og før den hen til det grønne felt. Tryk ENTER for at starte!";
    string intro6 = "Godt klaret! Strømmen er tilbage i rumstationen. Tryk på ENTER for at gå tilbage";

    public bool gameRunning = false;
    public bool goalReached = false;
    void Start()
    {
        introtext.text = intro1;
    }

    void Update()
    {
        if (!gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Return) && !goalReached)
            {
                introCount++;
                switch (introCount)
                {
                    case 1:
                        introtext.text = intro1;
                        break;
                    case 2:
                        introtext.text = intro2;
                        break;
                    case 3:
                        introtext.text = intro3;
                        break;
                    case 4:
                        introtext.text = intro4;
                        break;
                    case 5:
                        robot.gameObject.SetActive(false);
                        gameRunning = true;
                        break;
                }
            }

            if (goalReached == true)
            {
                robot.gameObject.SetActive(true);
                introtext.text = intro6;

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    introCount = 1;
                    SceneManager.LoadSceneAsync(1);
                }
                
            }

        }
    }
}
