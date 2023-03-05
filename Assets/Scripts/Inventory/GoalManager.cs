using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GoalManager : MonoBehaviour
{
    [SerializeField] string[] Goals;
    int CurrentGoal=0;
    [SerializeField] TextMeshProUGUI GoalText;
    [SerializeField] Animator GoalUpdatedAnim;
    [SerializeField] FontTyper GoalUpdatedTyper;
    [SerializeField] GameObject jingle;
    private void Start()
    {
        UpdateGoal(CurrentGoal);
    }

    public void UpdateGoal(int num)
    {
        GoalText.text = Goals[num];
        GoalUpdatedAnim.Play("Updated");
        GoalUpdatedTyper.Type("Goal Updated");
        Instantiate(jingle, transform.position, Quaternion.identity);
    }

    
}
