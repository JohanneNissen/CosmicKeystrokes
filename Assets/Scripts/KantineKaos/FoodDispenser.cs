using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public RectTransform dispenser;
    public GameObject trayPrefab;

    private int trayCount = 0;
    private float yOffset = 18f;

    private Vector2 baseOffset = new Vector2(-235f, 250f);

    public void addTray()
    {
        GameObject newTray = Instantiate(trayPrefab, dispenser);
        RectTransform rt = newTray.GetComponent<RectTransform>();

        rt.localScale = Vector3.one;

        rt.anchorMin = new Vector2(1, 0);
        rt.anchorMax = new Vector2(1, 0);
        rt.pivot = new Vector2(0.5f, 0.5f);

        rt.anchoredPosition = baseOffset + new Vector2(0, trayCount * yOffset);

        trayCount++;
    }
}
