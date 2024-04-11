using Unity.VisualScripting;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] StateMachine playerMovementSM;
    [SerializeField] public Transform weapon;
    [SerializeField] float returnSpeed;
    [SerializeField] float xStrengthMoving;
    [SerializeField] float yStrengthMoving;
    [SerializeField] float yStrengthIdle;
    [SerializeField] float speed;
    [SerializeField] float speedIdle;

    float time = 0;

    // Move the weapon in the lower half of a semi-circle
    public void AnimateWeaponSway(float xStrength, float yStrength)
    {
        if (!weapon) return;

        float deltaX = Mathf.Sin(time * speed) * xStrength;
        float deltaY = Mathf.Abs(Mathf.Sin(time * speed) * yStrengthMoving) - yStrength;

        Vector3 displacement = new Vector3(deltaX, deltaY, 0);

        weapon.localPosition = displacement;
    }

    public void WeaponSwayMoving()
    {
        AnimateWeaponSway(xStrengthMoving, yStrengthMoving);
    }

    // Move the weapon model up and down
    public void WeaponSwayIdle()
    {
        if (!weapon) return;

        float deltaY = Mathf.Sin(time * speedIdle) * yStrengthIdle;
        Vector3 curPos = weapon.localPosition;
        curPos.x -= Mathf.Lerp(curPos.x, 0, returnSpeed * Time.deltaTime);
        curPos.y = deltaY;
        weapon.localPosition = curPos;
    }

    void Update()
    {
        time = (time + Time.deltaTime) % (Mathf.PI * 2);
    }
}
