using UnityEngine;
using TMPro;

public class MenuScoreUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text game1ScoreTx;
    //[SerializeField] private TMP_Text game2ScoreTx;
    [SerializeField] private TMP_Text game3ScoreTx;

    void Start()
    {
        UpdateScores();
    }

    void Update()
    {

    }

    void UpdateScores()
    {
        game1ScoreTx.text = $"<color=#6EC6FF>Rum Ræs: </color>{HighScoreTracker.HSTInstance.Minigame1.Highscore}";
        //game2ScoreTx.text = $"<color=#6EC6FF>Strøm Dirigering: </color>{HighScoreTracker.HSTInstance.Minigame2.Highscore}";
        game3ScoreTx.text = $"<color=#6EC6FF>Kantine Kaos:</color> \n" +
                            $"<color=#7DFFB3>Præsition: </color>{HighScoreTracker.HSTInstance.Minigame3Acc.Highscore}%\n" +
                            $"<color=#7DFFB3>Antal Ord: </color>{HighScoreTracker.HSTInstance.Minigame3Words.Highscore}";
    }
}
