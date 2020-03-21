using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CodeWindowAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }

    public void SetCodeUp()
    {
        anim.SetBool("CodeUp", true);
    }
    public void SetCodeDown()
    {
        anim.SetBool("CodeUp", false);
    }
}
