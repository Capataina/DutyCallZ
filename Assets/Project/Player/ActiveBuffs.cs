using System.Collections;
using UnityEngine;

public class ActiveBuffs : MonoBehaviour
{

    private GameObject player;
    public float buffDuration;
    public bool speedBuff;
    public bool regenBuff;
    private bool resistanceBuff;
    private PlayerMovement playerSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpeed = player.GetComponent<PlayerMovement>();
    }

    //Update is called once per frame
    void Update()
    {
        if (speedBuff)
        {
            StartCoroutine(AddSpeedBuff());
            // Debug.Log("This should be true:" + speedBuff);
            speedBuff = false;
            // Debug.Log("This should be false:" + speedBuff);
        }
        
        if (regenBuff)
        {
            StartCoroutine(AddRegenBuff());
            regenBuff = false;
        }
    }
    
    private IEnumerator AddSpeedBuff()
    {
        playerSpeed.speed = playerSpeed.baseSpeed * 1.2f;
        yield return new WaitForSeconds(buffDuration);
        playerSpeed.speed = playerSpeed.baseSpeed;
    }

    private IEnumerator AddRegenBuff()
    {
        yield break;
    }
    
}
