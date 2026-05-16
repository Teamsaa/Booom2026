using System.Collections;
using System.Collections.Generic;
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

    

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(ArrorwAnimationWait());
    }

    IEnumerator ArrorwAnimationWait()
    {
        var rollAnmator = rollAnmation.GetComponent<Animator>();
        rollAnmation.SetActive(true);
        this.gameObject.SetActive(false);
        Destroy(fz);
        Destroy(qhsr);
        Destroy(qhfr);
        Destroy(jdjuan);

        while (true)
        {
            AnimatorStateInfo stateInfo = rollAnmator.GetCurrentAnimatorStateInfo(0);
            Debug.Log(stateInfo.IsName(name));
            if (stateInfo.IsName(name))
            {
                if (stateInfo.normalizedTime >= 1.0f)
                    break;
            }

            yield return null;
        }

        rollAnmation.SetActive(true);
        Destroy(this.gameObject);
    }
}
