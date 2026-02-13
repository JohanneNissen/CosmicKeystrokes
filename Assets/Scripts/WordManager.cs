using UnityEngine;

public class WordManager : MonoBehaviour
{
    private WordComplexityAlgorithm algo;
    public TextAsset wordjson;
    
    void Start()
    {
        algo = new WordComplexityAlgorithm();
        algo.KeyDataStore(wordjson.text);

        double result = algo.calculateComplexity("bih");
        Debug.Log("complexity: " + result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
