using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject creditsMenu, settingsMenu;
    [SerializeField] EventSystem eventSystem;
    private bool settingsActive, creditsActive;

    public void ToggleCredits()
    {
        creditsActive = !creditsActive;
        SetCreditsMenuActive(creditsActive);
    }

    public void ToggleSettings()
    {
        settingsActive = !settingsActive;
        SetSettingsMenuActive(settingsActive);
    }

    private void SetCreditsMenuActive(bool active)
    {
        eventSystem.SetSelectedGameObject(active ? creditsMenu.GetComponentInChildren<Button>().gameObject : playButton);
        creditsMenu.SetActive(active);
        settingsMenu.SetActive(false);
    }

    private void SetSettingsMenuActive(bool active)
    {
        eventSystem.SetSelectedGameObject(active ? settingsMenu.GetComponentInChildren<Button>().gameObject : playButton);
        settingsMenu.SetActive(active);
        creditsMenu.SetActive(false);
    }
}
