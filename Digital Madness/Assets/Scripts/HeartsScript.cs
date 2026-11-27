
using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    public GameObject heartPrefab;
    public int maxHearts = 5;
    private Image[] hearts;
    public float heartsSize = 48f;
    public float spacing = 10f;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    void Awake()
    {
        hearts = new Image[maxHearts];
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject h = Instantiate(heartPrefab, transform);
            hearts[i] = h.GetComponent<Image>();

            RectTransform rt = hearts[i].rectTransform;
            rt.anchorMin = rt.anchorMax = new Vector2(0f, 1f);
            rt.pivot = new Vector2(0f, 1f);
            rt.sizeDelta = new Vector2(heartsSize, heartsSize);
            rt.anchoredPosition = new Vector2(i*(heartsSize + spacing), 0f);
        }
        if (fullHeart == null) fullHeart = hearts[0].sprite;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetLives(int lives)
    {
        Debug.Log("Setlives " + lives);
        for(int i =0; i<hearts.Length;i++)
        {
            bool alive = i < lives;
            if (emptyHeart != null)
            {
                hearts[i].sprite = alive ? fullHeart : emptyHeart;
            }
            else
                hearts[i].enabled = alive;
        }
    }
}

