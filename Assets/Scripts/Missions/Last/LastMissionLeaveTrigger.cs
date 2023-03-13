using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LastMissionLeaveTrigger : MonoBehaviour
{
    bool busy;
    [SerializeField] GameObject FadeOut;
    [SerializeField] string SceneName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !busy)
        {
            busy = true;
            FadeOut.SetActive(true);
            

        }
    }

    IEnumerator LoadNext()
    {
        while(AudioListener.volume > 0)
        {
            AudioListener.volume -= .7f * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneName);
    }
}
