using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvInteractable : Interactable
{
    [SerializeField] GameObject bamniWorld;
    [SerializeField] CanvasGroup transitionGroup;
    private PlayerInventory playerInventory;
    private ShowManager showManager;

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        showManager = FindObjectOfType<ShowManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (showManager) showManager.gameObject.SetActive(false);
    }

    public override void Interacte()
    {
        base.Interacte();
        if (playerInventory.GetCurrentItem != null)
        {
            if (playerInventory.GetCurrentItem.TryGetComponent<Tape>(out Tape tape))
            {
                SetBamniWorldActive(true);
                showManager.SetTape(tape);
            }
        }
    }

    private void SetBamniWorldActive(bool active)
    {
        StartCoroutine(TransitionToWorld(active));
    }

    private IEnumerator TransitionToWorld(bool isBamniWorld)
    {
        float t = 0f;
        while (t <= 1f)
        {
            transitionGroup.alpha = Mathf.Lerp(transitionGroup.alpha, 1f, t);
            t += Time.deltaTime * 2f;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        
        bamniWorld.SetActive(isBamniWorld);
        Camera.main.gameObject.SetActive(!isBamniWorld);

        float t2 = 0f;
        while (t2 <= 1f)
        {
            transitionGroup.alpha = Mathf.Lerp(transitionGroup.alpha, 0f, t2);
            t2 += Time.deltaTime * 2f;
            yield return null;
        }


    }

}
