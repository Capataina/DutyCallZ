using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        scoreText.text = "Score: 0";
    }

    public void UpdateScore(float score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateAmmo(float ammoInMagazine, float totalBullets)
    {
        ammoText.text = ammoInMagazine + "/" + totalBullets;
    }
}