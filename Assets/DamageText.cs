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

    float currentSpeed;
    float time;
    Vector3 trueDir;
    Vector2 sphericalCoord;
    [HideInInspector] public Vector3 targetPos;

    private void Start()
    {
        currentSpeed = initialSpeed;
        Vector3 pos = (targetPos - Camera.main.transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(pos);
        transform.rotation = rot;
        dir.x *= Random.Range(0, 2) * 2 - 1;
        dir.y = Random.Range(-dir.y, dir.y);
        transform.localPosition = pos;
        sphericalCoord.x = pos.z / Mathf.Abs(pos.z) * Mathf.Acos(pos.x / Mathf.Sqrt(pos.x * pos.x + pos.z * pos.z));
        sphericalCoord.y = Mathf.Acos(pos.y);
        print(sphericalCoord);
    }

    private void Update()
    {
        time += Time.deltaTime;
        sphericalCoord += dir.normalized * currentSpeed * Time.deltaTime;
        Vector3 newPos = new Vector3(
            Mathf.Sin(sphericalCoord.y) * Mathf.Cos(sphericalCoord.x),
            Mathf.Cos(sphericalCoord.y),
            Mathf.Sin(sphericalCoord.y) * Mathf.Sin(sphericalCoord.x)
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
