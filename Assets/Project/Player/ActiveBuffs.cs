using System.Collections;
using UnityEngine;

public class ActiveBuffs : MonoBehaviour
{

    private GameObject player;
    public float buffDuration;
    
    public bool speedBuff;
    private bool hasSpeedBuff;
    
    public bool regenBuff;
    private bool resistanceBuff;
    private PlayerMovement playerSpeed;
    private float holdDownTimer;

    private GameObject speedBuffStation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpeed = player.GetComponent<PlayerMovement>();

        speedBuffStation = GameObject.FindGameObjectWithTag("SpeedBuffStation");
    }

    //Update is called once per frame
    void Update()
    {

        if (speedBuff && !hasSpeedBuff)
        {
            StartCoroutine(AddSpeedBuff());
            speedBuff = false;
        }
        
        if (regenBuff)
        {
            StartCoroutine(AddRegenBuff());
            regenBuff = false;
        }
        
        CheckDistance(speedBuffStation);
    }
    
    private IEnumerator AddSpeedBuff()
    {
        hasSpeedBuff = true;
        playerSpeed.speed = playerSpeed.baseSpeed * 1.2f;
        yield return new WaitForSeconds(buffDuration);
        //Debug.Log("buff ended");
        playerSpeed.speed = playerSpeed.baseSpeed;
        hasSpeedBuff = false;
    }

    private IEnumerator AddRegenBuff()
    {
        yield break;
    }

    void CheckDistance(GameObject buffStation)
    {
        if (Vector3.Distance(player.transform.position, buffStation.transform.position) <= 5)
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdDownTimer += Time.deltaTime;
                
                if (holdDownTimer >= 1)
                {
                    switch (buffStation.GetComponent<StationBuff>().buff)
                    {
                        case StationBuff.BuffType.Speed:
                            if (!hasSpeedBuff)
                            {
                                speedBuff = true;
                                // print("gave speed buff" + speedBuff);
                                holdDownTimer = 0;
                            }
                            break;
                        case StationBuff.BuffType.Regen:
                            regenBuff = true;
                            holdDownTimer = 0;
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
