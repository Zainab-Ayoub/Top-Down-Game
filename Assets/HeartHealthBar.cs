using UnityEngine;

public class HeartHealthBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public PlayerHealth playerHealth;

    private GameObject[] hearts;

    void Start()
    {
        hearts = new GameObject[playerHealth.maxHealth];

        for (int i = 0; i < hearts.Length; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.transform.localPosition = new Vector3(i * 0.4f, 0f, 0f); // space between hearts
            hearts[i] = heart;
        }
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            SpriteRenderer sr = hearts[i].GetComponent<SpriteRenderer>();
            if (i < playerHealth.currentHealth)
            {
                sr.sprite = fullHeart;
            }
            else
            {
                sr.sprite = emptyHeart;
            }
        }
    }
}
