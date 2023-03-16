using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTranssition : MonoBehaviour
{
    [SerializeField] Animator VolumeAnimation;
    [SerializeField] GameObject MainScreen,SelectScreen,TransitionSound,LoadGameTransition;
    bool isBusy;
    private void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayClicked()
    {
        if(isBusy)
        {
            return;
        }
        isBusy = true;
        StartCoroutine(PlayNumerator());
        Transition();
    }
    public void BackClicked(GameObject CurrentScreen)
    {
        if (isBusy)
        {
            return;
        }
        isBusy = true;
        StartCoroutine(BackNumerator(CurrentScreen));
        Transition();
    }
    public void StartGameClicked(int mode)
    {
        if (isBusy)
        {
            return;
        }
        isBusy = true;
        StartCoroutine(StartGameNumerator(mode));
        Transition();
    }
    IEnumerator StartGameNumerator(int mode)
    {
        while (isBusy)
        {
            yield return null;
        }
        LoadGameTransition.SetActive(true);
        SelectScreen.SetActive(false);

        PlayerPrefs.SetInt("LevelMode", mode);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("StartCutscene");
         
    }
    IEnumerator BackNumerator(GameObject CurrentScreen)
    {
        while (isBusy)
        {
            yield return null;
        }
        CurrentScreen.SetActive(false);
        MainScreen.SetActive(true);
    }

    IEnumerator PlayNumerator()
    {
        while (isBusy)
        {
            yield return null;
        }
        MainScreen.SetActive(false);
        SelectScreen.SetActive(true);
    }


    public void TransitionOver()
    {
        isBusy = false;
    }

   void Transition()
    {
        Instantiate(TransitionSound, transform.position, Quaternion.identity);
        VolumeAnimation.Play("Loop", -1,  0.0f);

    }
}
