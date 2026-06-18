using UnityEngine;

public class WordManager : MonoBehaviour
{
    private WordComplexityAlgorithm algo;
    public TextAsset wordjson;
    
    void Start()
    {
        string[] words = new string[25] {"bogreol", "kuglepen", "skoletaske", "vækkeur", "køleskab", "kalender", "mikroovn", "drikkedunk", "tandbørste", "tandpasta", "håndklæde", "brusebad", "badekar", "støvler", "håndvask", "bamse", "skraldespand", "brætspil", "nøgle", "lommelygte", "kost", "ledning", "sæbe", "skål", "gryde"};
        algo = new WordComplexityAlgorithm();
        algo.KeyDataStore(wordjson.text);

        foreach (string word in words)
        {
            double result = algo.calculateComplexity(word);
            Debug.Log(word + " complexity: " + result);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
