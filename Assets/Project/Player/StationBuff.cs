using UnityEngine;

public class StationBuff : Interactable
{
    public BuffType buff;
    public float buffCost;
    ActiveBuffs activeBuffs;

    private void Start()
    {
        activeBuffs = GameObject.FindGameObjectWithTag("Player").GetComponent<ActiveBuffs>();
    }

    public override void DisplayPrompt()
    {
        UIManager.instance.DisplayText($"Do you want to buy {buff} for {buffCost} points?");
    }

    public override void Interact(GameObject gameObject)
    {
        if (PlayerStats.current.currentScore >= buffCost)
        {
            activeBuffs.AddBuff(buff);
            PlayerStats.current.currentScore -= buffCost;
        }
    }


}
public enum BuffType
{
    Speed,
    Regen
}
