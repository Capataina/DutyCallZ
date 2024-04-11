using UnityEngine;

public class UnlockableWall : Interactable
{
    [SerializeField] float cost;

    public override void DisplayPrompt()
    {
        UIManager.instance.DisplayText($"Unlock wall for {cost} points.");
    }

    public override void Interact(GameObject other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats.currentScore >= cost)
        {
            stats.currentScore -= cost;
            Destroy(gameObject);
        }
    }
}
