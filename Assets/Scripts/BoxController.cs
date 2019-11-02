using UnityEngine;

public class BoxController : MonoBehaviour
{
    public IBoxItem item;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Activate() {
        if (item != null)
        {
            if (item.HasMore())
            {
                animator.Play("Hit");
                item.Activate();
            }
            animator.SetBool("active", item.HasMore());
        }
        else {
            animator.Play("Hit");
        }
        
    }
}
