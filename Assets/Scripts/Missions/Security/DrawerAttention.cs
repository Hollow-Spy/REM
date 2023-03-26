using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerAttention : MonoBehaviour
{
    [SerializeField] AudioSource SoundPlayer;
    [SerializeField] Animator ShakerAnimator;
    public bool PlayerGotIt;
    private void Start()
    {
        StartCoroutine(GrabbingAttention());
    }

    IEnumerator GrabbingAttention()
    {
        yield return new WaitForSeconds(3);
        while(!PlayerGotIt)
        {
            ShakerAnimator.Play("DrawShake");
            SoundPlayer.Play();
            yield return new WaitForSeconds(Random.Range(5, 10));

        }
     
    }
}
