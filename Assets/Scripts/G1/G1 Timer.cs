using TMPro;
using UnityEngine;

public class G1Timer : MonoBehaviour
{
    public TMP_Text text;          // Assign in Inspector
    public float startTime = 300f; // 5 minutes in seconds

    private float timeLeft;

    void Start()
    {
        timeLeft = startTime;
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
    }

}
