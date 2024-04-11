using TMPro;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] GameObject damageText;
    [SerializeField] Transform cam;

    private void Start()
    {
        CustomEventSystem.current.onSpawnDamageText += SpawnDamageText;
    }

    private void SpawnDamageText(float damage, Transform transform, Color color)
    {
        GameObject newText = Instantiate(damageText.gameObject);
        newText.transform.SetParent(base.transform);
        newText.GetComponent<DamageText>().targetTransform = transform;
        TMP_Text textComponent = newText.GetComponent<TMP_Text>();
        textComponent.text = damage.ToString();
        float originalAlpha = textComponent.alpha;
        textComponent.color = color;
        textComponent.alpha = originalAlpha;
    }

    private void Update()
    {
        transform.position = cam.position;
    }
}
