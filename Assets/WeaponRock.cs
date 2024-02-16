using UnityEngine;

public class WeaponRock : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform weapon;
    [SerializeField] float returnSpeed;
    [SerializeField] float xStrengthMoving;
    [SerializeField] float yStrengthMoving;
    [SerializeField] float yStrengthIdle;
    [SerializeField] float speed;
    [SerializeField] float speedIdle;

    float time = 0;

    void Update()
    {

        if (playerMovement.isMoving())
        {
            float deltaX = Mathf.Sin(time * speed) * xStrengthMoving;
            float deltaY = Mathf.Abs(Mathf.Sin(time * speed) * yStrengthMoving) - yStrengthMoving;

            Vector3 displacement = new Vector3(deltaX, deltaY, 0);

            weapon.localPosition = displacement;
        }
        else
        {
            float deltaY = Mathf.Sin(time * speedIdle) * yStrengthIdle;
            Vector3 curPos = weapon.localPosition;
            curPos.x -= Mathf.Lerp(curPos.x, 0, returnSpeed * Time.deltaTime);
            curPos.y = deltaY;
            weapon.localPosition = curPos;
        }



        //        else if (weapon.localPosition.sqrMagnitude > 0.01f)
        //        {
        //            weapon.localPosition = Vector3.MoveTowards(weapon.localPosition, Vector3.zero, returnSpeed * Time.deltaTime);
        //        }
        //
        time = (time + Time.deltaTime) % (Mathf.PI * 2);
    }
}
