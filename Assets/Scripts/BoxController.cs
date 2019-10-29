using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public int amount = 1;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("amount", amount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate() {
        if (amount > 0) {
            amount--;
            animator.Play("CoinAnimation");
        }

        animator.SetInteger("amount", amount);
        
    }
}
