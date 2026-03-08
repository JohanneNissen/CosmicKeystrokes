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

    void Start()
    {
        timeLeft = startTime;
        endScreen.gameObject.SetActive(false);
        backtext.gameObject.SetActive(false);
    }

    void Update()
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
            endScreen.text = ("Tillykke, du har rejst: " + Mathf.FloorToInt(distanceCounter.currentValue).ToString() + " lysår!");

        }

        if (timeLeft <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
