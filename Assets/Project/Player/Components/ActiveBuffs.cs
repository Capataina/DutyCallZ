using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBuffs : MonoBehaviour
{

    private GameObject player;
    public float buffDuration;

    // all active buffs
    private List<BuffType> activeBuffs = new List<BuffType>();

    private PlayerStats playerStats;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Adds provided active buff to player
    public void AddBuff(BuffType buff)
    {
        switch (buff)
        {
            case BuffType.Speed:
                //StartCoroutine(AddSpeedBuff());
                print("got speed buff");
                break;
            case BuffType.Regen:
                //StartCoroutine(AddRegenBuff());
                print("got regen buff");
                break;
            default:
                break;
        }
    }

    // private IEnumerator AddSpeedBuff()
    // {
    //     activeBuffs.Add(BuffType.Speed);
    //     playerSpeed.speed = playerSpeed.walkSpeed * 1.2f;
    //     yield return new WaitForSeconds(buffDuration);
    //     playerSpeed.speed = playerSpeed.walkSpeed;
    //     activeBuffs.Remove(BuffType.Speed);
    // }

    private IEnumerator AddRegenBuff()
    {
        activeBuffs.Add(BuffType.Regen);
        playerStats.healthRegen = playerStats.baseHealthRegen + 3;
        yield return new WaitForSeconds(buffDuration);
        playerStats.healthRegen = playerStats.baseHealthRegen;
        activeBuffs.Remove(BuffType.Regen);
    }
}