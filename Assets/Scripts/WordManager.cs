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
        int finger = algo.getFinger("a");
        int row = algo.getRow("a");
        float basep = algo.getBasePenalty("a");
        int fingerp = algo.getFingerPenalty("a");
        int rowP = algo.getRowPenalty("a");
        Debug.Log("hand: " + hand);
        Debug.Log("finger: " +  finger);
        Debug.Log("row: " +  row);
        Debug.Log("Base: " + basep);
        Debug.Log("FingerPe: " + fingerp);
        Debug.Log("rowP: " + rowP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
