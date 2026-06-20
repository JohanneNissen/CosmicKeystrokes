using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G1Timer : MonoBehaviour
{
    public TMP_Text text;          // Assign in Inspector
    public float startTime = 300f; // 5 minutes in seconds

    private float timeLeft;

    public G1DistanceCounter distanceCounter;
    public G1ManagerScript managerScript;

    //Text boxes for game finish
    public TMP_Text endScreen;
    public TMP_Text backtext;
    public TMP_Text instruksText;

    void Start()
    {
        timeLeft = startTime;
        endScreen.gameObject.SetActive(false);
        backtext.gameObject.SetActive(false);
    }

    void Update()
    {
        if (managerScript.gameRunning)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(timeLeft / 60);
                int seconds = Mathf.FloorToInt(timeLeft % 60);
                text.text = $"{minutes:00}:{seconds:00}";
            }
            else
            {
                text.text = "00:00";
            }

            if (timeLeft <= 0)
            {
                distanceCounter.gameRunning = false;
                managerScript.gameRunning = false;
                endScreen.gameObject.SetActive(true);
                backtext.gameObject.SetActive(true);
                instruksText.gameObject.SetActive(false);
                //managerScript.Robot.gameObject.SetActive(true);
                endScreen.text = ("Tillykke, du har rejst: " + Mathf.FloorToInt(distanceCounter.currentValue).ToString() + " lysår!");
                HighScoreTracker.HSTInstance.Minigame1.LastScore = Mathf.FloorToInt(distanceCounter.currentValue);
                if (HighScoreTracker.HSTInstance.Minigame1.LastScore > HighScoreTracker.HSTInstance.Minigame1.Highscore)
                {
                    HighScoreTracker.HSTInstance.Minigame1.Highscore = HighScoreTracker.HSTInstance.Minigame1.LastScore;
                }
            }
        }

        if (timeLeft <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
