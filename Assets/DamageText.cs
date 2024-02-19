using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] Vector3 dir;
    [SerializeField] float initialSpeed;
    [SerializeField] float decel;
    [SerializeField] float lifeTime;
    [SerializeField] RectTransform rectTransform;

    float currentSpeed;
    float time;
    Vector3 trueDir;

    private void Start()
    {
        currentSpeed = initialSpeed;
        Quaternion rot = Quaternion.LookRotation((transform.position - Camera.main.transform.position).normalized);
        dir.x *= Random.Range(0, 2) * 2 - 1;
        dir.y = Random.Range(-dir.y, dir.y);
        trueDir = rot * dir.normalized;
    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.localPosition += trueDir * currentSpeed * Time.deltaTime;

        currentSpeed = Mathf.Lerp(currentSpeed, 0, decel * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation((transform.position - Camera.main.transform.position).normalized);

        if (time >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

}
