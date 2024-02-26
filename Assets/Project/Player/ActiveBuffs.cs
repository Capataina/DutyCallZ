using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBuffs : MonoBehaviour
{

    private GameObject player;
    public float buffDuration;

    private List<StationBuff.BuffType> activeBuffs = new List<StationBuff.BuffType>();

    public bool speedBuff;
    public bool regenBuff;
    private bool resistanceBuff;

    private PlayerMovement playerSpeed;
    private PlayerStats playerStats;
    private float holdDownTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpeed = player.GetComponent<PlayerMovement>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    //Update is called once per frame
    void Update()
    {

        if (speedBuff)
        {
            StartCoroutine(AddSpeedBuff());
            speedBuff = false;
        }

        if (regenBuff)
        {
            StartCoroutine(AddRegenBuff());
            regenBuff = false;
        }

        CheckDistance();
    }

    private IEnumerator AddSpeedBuff()
    {
        activeBuffs.Add(StationBuff.BuffType.Speed);
        playerSpeed.speed = playerSpeed.walkSpeed * 1.2f;
        yield return new WaitForSeconds(buffDuration);
        playerSpeed.speed = playerSpeed.walkSpeed;
        activeBuffs.Remove(StationBuff.BuffType.Speed);
    }

    private IEnumerator AddRegenBuff()
    {
        activeBuffs.Add(StationBuff.BuffType.Regen);
        playerStats.healthRegen = playerStats.baseHealthRegen + 3;
        yield return new WaitForSeconds(buffDuration);
        playerStats.healthRegen = playerStats.baseHealthRegen;
        activeBuffs.Remove(StationBuff.BuffType.Regen);
    }

    private void CheckDistance()
    {
        Collider[] buffStations = Physics.OverlapSphere(player.transform.position, 2f, LayerMask.GetMask("Buff Stations"));

        GameObject closestStation = null;

        if (buffStations.Length > 0)
        {
            closestStation = buffStations[0].gameObject;
        }

        foreach (var station in buffStations)
        {
            if (Vector3.Distance(player.transform.position, station.gameObject.transform.position) <=
                Vector3.Distance(player.transform.position, closestStation.transform.position))
            {
                closestStation = station.gameObject;
            }
        }

        if (closestStation)
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdDownTimer += Time.deltaTime;

                if (holdDownTimer >= 1)
                {
                    var closestStationBuff = closestStation.GetComponent<StationBuff>();

                    switch (closestStationBuff.buff)
                    {
                        case StationBuff.BuffType.Speed:
                            if (!activeBuffs.Contains(StationBuff.BuffType.Speed) && PlayerStats.current.currentScore >= closestStationBuff.buffCost)
                            {
                                print("gave speed buff");
                                PlayerStats.current.currentScore -= closestStationBuff.buffCost;
                                // UIManager.instance.UpdateScore(PlayerStats.current.currentScore);
                                speedBuff = true;
                                holdDownTimer = 0;
                            }
                            break;
                        case StationBuff.BuffType.Regen:
                            if (!activeBuffs.Contains(StationBuff.BuffType.Regen) && PlayerStats.current.currentScore >= closestStationBuff.buffCost)
                            {
                                print("gave regen buff");
                                PlayerStats.current.currentScore -= closestStationBuff.buffCost;
                                // UIManager.instance.UpdateScore(PlayerStats.current.currentScore);
                                regenBuff = true;
                                holdDownTimer = 0;
                            }
                            break;
                    }
                }
            }
            else
            {
                holdDownTimer = 0;
            }
        }
    }
}
