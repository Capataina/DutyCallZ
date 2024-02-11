using UnityEngine;

public class WeaponRock : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform weapon;
    [SerializeField] float returnSpeed;
    [SerializeField] float xStrength;
    [SerializeField] float yStrength;
    [SerializeField] float speed;

    float time = 0;

    void Update()
    {

        if (playerMovement.isMoving())
        {
            float deltaX = Mathf.Sin(time * speed) * xStrength;
            float deltaY = Mathf.Abs(Mathf.Sin(time * speed) * yStrength) - yStrength;

            Vector3 displacement = new Vector3(deltaX, deltaY, 0);

            weapon.localPosition = displacement;
            print(weapon.localPosition);
        }
        else if (weapon.localPosition.sqrMagnitude > 0.01f)
        {
            weapon.localPosition = Vector3.MoveTowards(weapon.localPosition, Vector3.zero, returnSpeed * Time.deltaTime);
        }

        time = (time + Time.deltaTime) % (Mathf.PI * 2);
    }
}
