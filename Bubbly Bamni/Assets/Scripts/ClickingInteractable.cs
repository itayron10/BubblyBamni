using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClickingInteractable : MonoBehaviour
{
    [SerializeField] DialogLine mistakeLine;
    private ShowManager showManager;
    

    private void Start()
    {
        showManager = FindObjectOfType<ShowManager>();
    }

    public void ClickedOn()
    {
        /// debug to the show manager that this interactable was clicked on
        if (showManager.currentTargetClickedInterctable == this)
        {
            showManager.MoveToNextSection();
            showManager.SetEpisodeRunning(true);
        }
        else
        {
            showManager.StopCoroutine(showManager.writingCoroutine);
            showManager.writingCoroutine = StartCoroutine(showManager.WriteLine(mistakeLine));
        }
    }

}
