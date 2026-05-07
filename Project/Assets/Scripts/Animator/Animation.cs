using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Animator customerAnimator;
    [SerializeField] private Animator orderAnimator;
    [SerializeField] private Animator orderButtonAnimator;


    // Update is called once per frame
    void Update()
    {
        AnimationController();
    }

    private void AnimationController()
    {
        AnimatorStateInfo info = customerAnimator.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("customer") && info.normalizedTime >= 1.0f)
        {
            orderAnimator.Play("Order");
            AnimatorStateInfo temp = orderAnimator.GetCurrentAnimatorStateInfo(0);
            if (temp.IsName("Order") && temp.normalizedTime >= 1.0f)
            {
                orderButtonAnimator.Play("OrderButton");
            }
        }
    }
}
