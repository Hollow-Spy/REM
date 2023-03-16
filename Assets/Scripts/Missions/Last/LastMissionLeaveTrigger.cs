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
            StartCoroutine(LoadNext());

        }
    }

    IEnumerator LoadNext()
    {
       
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneName);
    }
}
