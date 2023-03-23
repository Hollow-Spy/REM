using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchBodyController : MonoBehaviour
{
    [SerializeField] Material[] GlitchShaders;
    [SerializeField] float GlitchSpeed,MaxTime;
    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip[] Clips;
    public void PlayGlitchSound(int num)
    {
        audiosource.clip = Clips[num];
        audiosource.Play();
    }
    private void OnDisable()
    {
        for (int i = 0; i < GlitchShaders.Length; i++)
        {
            GlitchShaders[i].SetFloat("_GlitchSpeed", 0);
            GlitchShaders[i].SetFloat("_MaxTime", 10);

        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<GlitchShaders.Length;i++)
        {
            GlitchShaders[i].SetFloat("_GlitchSpeed", GlitchSpeed);
            GlitchShaders[i].SetFloat("_MaxTime", MaxTime);

        }
    }
}
