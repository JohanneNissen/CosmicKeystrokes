using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal.Internal;

public class LetterThrower : MonoBehaviour
{
    public float flightDuration = 0.5f;
    public float arcHeight = 100f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float time;

    private RectTransform rectTransform;
    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float t = time / flightDuration;
        t = Mathf.Clamp01(t);
        Vector3 pos = Vector3.Lerp(startPos, targetPos, t); //straight movement
        //adding Arc
        float arc = Mathf.Sin(t * Mathf.PI) * arcHeight;
        pos.y += arc;

        rectTransform.position = pos;

        if (t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    public void Init(char letter, Transform targetTransform)
    {
        textComponent.text = letter.ToString();
        startPos = rectTransform.position;
        targetPos = targetTransform.position;

        time = 0f;
    }
}
