using UnityEngine;
using UnityEngine.Events;

public class CoinItem : IBoxItem
{
    public int amount;
    public UnityEvent OnCoinCollected;

    private Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        amount--;
        animator.Play("CoinItemJump");
        OnCoinCollected.Invoke();
    }

    public override bool HasMore()
    {
        return amount > 0;
    }
}
