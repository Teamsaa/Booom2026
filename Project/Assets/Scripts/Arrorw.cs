using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Arrorw : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject qhsr;
    [SerializeField] private GameObject qhfr;
    [SerializeField] private GameObject jdjuan;
    [SerializeField] private GameObject roll;
    [SerializeField] private GameObject fz;
    [SerializeField] private GameObject rollAnmation;
    [SerializeField] private Animator rollAnimator;


    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ArrorwAnimationWait());
    }

    IEnumerator ArrorwAnimationWait()
    {
        rollAnmation.SetActive(true);
        var Image = this.gameObject.GetComponent<Image>();
        Image.enabled = false;
        rollAnimator.Play("roll");
        Destroy(fz);
        Destroy(qhsr);
        Destroy(qhfr);
        Destroy(jdjuan);

        
        while (true)
        {
            AnimatorStateInfo stateInfo = rollAnimator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("roll"))
            {
                yield return null;
                continue;
            }

            if (stateInfo.normalizedTime % 1.0f >= 0.98f && !rollAnimator.IsInTransition(0))
            {
                break;
            }
                    
            yield return null;
        }

        roll.SetActive(true);
        rollAnmation.SetActive(false);
        Destroy(this.gameObject);
    }
}
