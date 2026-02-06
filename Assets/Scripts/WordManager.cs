using UnityEngine;

public class WordManager : MonoBehaviour
{
    private WordComplexityAlgorithm algo;
    public TextAsset wordjson;
    
    void Start()
    {
        algo = new WordComplexityAlgorithm();
        algo.KeyDataStore(wordjson.text);

        int hand = algo.getHand("a");
        Debug.Log("hand: " + hand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
