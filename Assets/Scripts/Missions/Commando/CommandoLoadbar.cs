using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoLoadbar : MonoBehaviour
{

    [SerializeField] GameObject Pivot;
    [SerializeField] GameObject Engine;
    [SerializeField] GameObject TvTrigger;

    private void Start()
    {
        StartCoroutine(Increase());
    }
    IEnumerator Increase()
    {
        while(Pivot.transform.localScale.x < 1)
        {
            yield return new WaitForSeconds(1f);
            Pivot.transform.localScale = new Vector3(Pivot.transform.localScale.x + 0.01666666666f, Pivot.transform.localScale.y, Pivot.transform.localScale.z); 

            if (TvTrigger && Pivot.transform.localScale.x > .2f)
            {
                TvTrigger.SetActive(true);
            }

        }
        Engine.SetActive(true);
        gameObject.SetActive(false);
    }
}
