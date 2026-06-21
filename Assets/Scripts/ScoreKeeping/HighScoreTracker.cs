using UnityEngine;

public class ScoreData
{
    public int LastScore;
    public int Highscore;
}

public class HighScoreTracker : MonoBehaviour
{
    public static HighScoreTracker HSTInstance;

    public ScoreData Minigame1 = new();
    public ScoreData Minigame2 = new();
    public ScoreData Minigame3Acc = new();
    public ScoreData Minigame3Words = new();

    void Awake()
    {
        //Singleton
        if (HSTInstance == null)
        {
            HSTInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
