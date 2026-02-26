using TMPro;
using UnityEngine;

public class G1ManagerScript : MonoBehaviour
{
    [Header("Word Audio")]
    public AudioSource audioSource;
    public AudioClip[] wordClips;
    public string[] words;
    public float wordInterval = 10f;

    [Header("UI")]
    public TMP_InputField inputField;

    [Header("Counter")]
    public G1DistanceCounter counter;      
    public float speedIncrease = 0.5f;
    public float speedDecrease = 0.5f;

    int currentIndex = 0;
    float timer = 0f;

    void Start()
    {
        PlayWord();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wordInterval)
        {
            CheckAnswer();
            NextWord();
        }
    }

    void PlayWord()
    {
        audioSource.PlayOneShot(wordClips[currentIndex]);
        inputField.text = "";
        inputField.ActivateInputField();
    }

    void CheckAnswer()
    {
        if (inputField.text.Trim().ToLower() == words[currentIndex].ToLower())
            counter.countSpeed += speedIncrease;
        else
            counter.countSpeed = Mathf.Max(0, counter.countSpeed - speedDecrease);
    }

    void NextWord()
    {
        timer = 0f;
        currentIndex = (currentIndex + 1) % words.Length;
        PlayWord();
    }
}
