using TMPro;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;

    private void Start()
    {
        CustomEventSystem.current.onSpawnDamageText += SpawnDamageText;
    }

    private void SpawnDamageText(float damage, Vector3 position, Color color)
    {
        GameObject newText = Instantiate(damageText.gameObject);
        newText.transform.SetParent(transform);
        newText.transform.position = position;
        TMP_Text textComponent = newText.GetComponent<TMP_Text>();
        textComponent.text = damage.ToString();
        float originalAlpha = textComponent.alpha;
        textComponent.color = color;
        textComponent.alpha = originalAlpha;
    }
}
