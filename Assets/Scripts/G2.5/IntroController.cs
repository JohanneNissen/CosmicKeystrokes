using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    [SerializeField] Image robot;
    [SerializeField] TMP_Text introtext;

    int introCount = 1;

    string intro1 = "Velkommen til Strøm Dirigering.";
    string intro2 = "intro 2";
    string intro3 = "intro 3";

    public bool gameRunning = false;
    void Start()
    {
        introtext.text = intro1;
    }

    void Update()
    {
        if (!gameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Return))
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
                        robot.gameObject.SetActive(false);
                        gameRunning = true;
                        break;
                }
            }
        }
    }
}
