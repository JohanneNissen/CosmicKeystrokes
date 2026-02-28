using UnityEngine;

public class WordManager : MonoBehaviour
{
    private WordComplexityAlgorithm algo;
    public TextAsset wordjson;
    
    void Start()
    {
        string[] words = new string[5] {"prøve", "", "", "", ""};
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
