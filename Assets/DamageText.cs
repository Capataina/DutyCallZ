using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] Vector2 dir;
    [SerializeField] float initialSpeed;
    [SerializeField] float decel;
    [SerializeField] float lifeTime;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Transform parent;

    float currentSpeed;
    float time;
    Vector2 originCoord;
    Vector2 sphericalDelta;
    [HideInInspector] public Transform targetTransform;

    private void Start()
    {
        currentSpeed = initialSpeed;
        Vector3 pos = (targetTransform.position - Camera.main.transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(pos);
        transform.rotation = rot;
        dir.x *= Random.Range(0, 2) * 2 - 1;
        dir.y = Random.Range(-dir.y, dir.y);
        parent.localPosition = pos;
        originCoord.x = pos.z / Mathf.Abs(pos.z) * Mathf.Acos(pos.x / Mathf.Sqrt(pos.x * pos.x + pos.z * pos.z));
        originCoord.y = Mathf.Acos(pos.y);
    }

    private void Update()
    {
        Vector3 pos = (targetTransform.position - Camera.main.transform.position).normalized;
        originCoord.x = pos.z / Mathf.Abs(pos.z) * Mathf.Acos(pos.x / Mathf.Sqrt(pos.x * pos.x + pos.z * pos.z));
        originCoord.y = Mathf.Acos(pos.y);

        time += Time.deltaTime;
        sphericalDelta += dir.normalized * currentSpeed * Time.deltaTime;
        Vector2 curSphericalPos = sphericalDelta + originCoord;

        Vector3 newPos = new Vector3(
            Mathf.Sin(curSphericalPos.y) * Mathf.Cos(curSphericalPos.x),
            Mathf.Cos(curSphericalPos.y),
            Mathf.Sin(curSphericalPos.y) * Mathf.Sin(curSphericalPos.x)
        );
        transform.localPosition = newPos;

        currentSpeed = Mathf.Lerp(currentSpeed, 0, decel * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation((transform.position - Camera.main.transform.position).normalized);

        if (time >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

}
