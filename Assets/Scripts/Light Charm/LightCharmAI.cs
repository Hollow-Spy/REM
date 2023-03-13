using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCharmAI : MonoBehaviour
{
    [SerializeField] Transform ShoulderPos;
    [SerializeField] float Speed,RotSpeed;
    [SerializeField] AudioSource HoverSound,VoiceSounds;
    [SerializeField] float PitchDiv;
    [SerializeField] AudioClip[] VoiceLines;
    [SerializeField] GameObject DeathCharm;

    private void Start()
    {
        transform.position = ShoulderPos.position;
        StartCoroutine(RandomSounds());
      
    }

    public void Death()
    {
        Instantiate(DeathCharm, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    IEnumerator RandomSounds()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(10, 20));
            VoiceSounds.clip = VoiceLines[Random.Range(0, VoiceLines.Length)];
            VoiceSounds.Play();
        }
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, ShoulderPos.position, Speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, ShoulderPos.rotation, RotSpeed * Time.deltaTime);

        float pitchCalc = Vector3.Distance(transform.position, ShoulderPos.position);
        HoverSound.pitch = .9f + (pitchCalc / PitchDiv);
        HoverSound.volume = .2f + (pitchCalc / (PitchDiv * 1.2f ));

        
    }
}
