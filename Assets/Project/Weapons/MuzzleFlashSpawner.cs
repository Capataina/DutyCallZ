using System.Collections;
using UnityEngine;

/*
    Spawns and kills a muzzle flash really quickly
*/
public class MuzzleFlashSpawner : MonoBehaviour
{
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] float time;

    float timer = 0;
    bool willSpawnFlash;
    public void SpawnMuzzleFlash()
    {
        willSpawnFlash = true;

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= time)
        {
            muzzleFlash.SetActive(false);
            timer = 0;
        }

        if (willSpawnFlash)
        {
            timer = 0;
            muzzleFlash.SetActive(true);
            willSpawnFlash = false;
        }
    }


    private IEnumerator DeleteParticle()
    {
        yield return time;
        muzzleFlash.SetActive(false);
    }
}

