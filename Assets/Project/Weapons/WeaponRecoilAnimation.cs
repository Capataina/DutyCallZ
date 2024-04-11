using UnityEngine;

/*
    Handles the procedural animation of the weapon while shooting
*/
public class WeaponRecoilAnimation : MonoBehaviour
{
    [SerializeField] Transform recoilParent;
    [SerializeField] float returnSpeedPos;
    [SerializeField] float recoilSpeedPos;
    [SerializeField] float returnSpeedRot;
    [SerializeField] float recoilSpeedRot;

    Vector3 targetPosition;
    Vector3 targetRotation;
    Vector3 currentRotation;

    // Change the position of the weapon model
    public void PlayRecoilAnimationPos(float xRecoil, float yRecoil, float zRecoil)
    {
        float xVal = Random.Range(-xRecoil, xRecoil);
        float yVal = Random.Range(-yRecoil, yRecoil);
        float zVal = Random.Range(zRecoil / 2, zRecoil);
        targetPosition += new Vector3(xVal, yVal, -zVal);
    }

    // Change the rotation of the weapon model
    public void PlayRecoilAnimationRot(float xMaxRecoil, float xMinRecoil, float yRecoil, float zRecoil)
    {
        float xVal = Random.Range(xMinRecoil, xMaxRecoil);
        float yVal = Random.Range(-yRecoil, yRecoil);
        float zVal = Random.Range(-zRecoil, zRecoil);
        targetRotation += new Vector3(-xVal, yVal, zVal);
    }

    void Update()
    {
        // Smoothly rotate/move the weapon model while constantly trying to pull it back
        targetPosition = Vector3.Lerp(targetPosition, Vector3.zero, returnSpeedPos * Time.deltaTime);
        recoilParent.localPosition = Vector3.Lerp(recoilParent.localPosition, targetPosition, recoilSpeedPos * Time.deltaTime);

        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeedRot * Time.deltaTime);
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, recoilSpeedRot * Time.deltaTime);
        recoilParent.localRotation = Quaternion.Euler(currentRotation);
    }
}
