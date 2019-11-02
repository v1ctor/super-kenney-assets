using UnityEngine.Events;

public class CoinItem : IBoxItem
{
    public int amount;
    public UnityEvent OnCoinCollected;

    public override void Activate()
    {
        amount--;
        OnCoinCollected.Invoke();
    }

    public override bool HasMore()
    {
        return amount > 0;
    }
}
