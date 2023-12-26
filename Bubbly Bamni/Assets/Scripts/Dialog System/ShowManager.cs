using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowManager : MonoBehaviour
{
    [SerializeField] GameObject bamniWorld;
    [SerializeField] CanvasGroup transitionGroup;
    [SerializeField] TextMeshProUGUI episodeNameText, speakerNameText, contentText;
    private int currentSectionIndex, currentLineIndex;
    private bool episodeRunning, writingLine;
    private float textTimer;
    private DialogSection[] currentEpisodeDialog;
    private Tape currentTape;
    private Camera mainCam;
    private Coroutine writingCoroutine;

    private void Start()
    {
        mainCam = Camera.main;
        bamniWorld.SetActive(false);
    }

    private void Update()
    {
        if (episodeRunning)
        {
            UpdateEpisode();
        }
        else Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateEpisode()
    {
        Cursor.lockState = CursorLockMode.None;
        episodeNameText.text = currentTape.GetName;
        UpdateDialog();
    }

    private void UpdateDialog()
    {
        DialogSection currentSection = currentEpisodeDialog[currentSectionIndex];
        DialogLine currentLine = currentSection.GetDialogLines[currentLineIndex];

        if (textTimer >= currentLine.GetTextDuration)
        {
            if (currentSection.GetDialogLines.Length - 1 == currentLineIndex)
            {
                if (currentEpisodeDialog.Length - 1 == currentSectionIndex)
                {
                    // move to the next line
                    EndEpisode();
                }
                else
                {
                    // move to the next section
                    currentSectionIndex++;
                    currentLineIndex = 0;
                    writingCoroutine = StartCoroutine(WriteText(0.01f));
                }
            }
            else
            {
                currentLineIndex++;
                // move to the next line
                writingCoroutine = StartCoroutine(WriteText(0.01f));
            }
        
            textTimer = 0f;
        }

        if (!writingLine) textTimer += Time.deltaTime;
    }

    private IEnumerator WriteText(float delay)
    {
        DialogLine dialogLine = currentEpisodeDialog[currentSectionIndex].GetDialogLines[currentLineIndex];
        writingLine = true;
        Debug.Log($"Starting to write with line index: {currentLineIndex} and section index: {currentSectionIndex} and speaker: {currentEpisodeDialog[currentSectionIndex].GetDialogLines[currentLineIndex].GetSpeakerName}");
        speakerNameText.text = contentText.text = string.Empty;
        for (int i = 0; i < dialogLine.GetSpeakerName.Length; i++)
        {
            speakerNameText.text += dialogLine.GetSpeakerName[i];
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i < dialogLine.GetLineText.Length; i++)
        {
            contentText.text += dialogLine.GetLineText[i];
            yield return new WaitForSeconds(delay);
        }

        writingLine = false;
    }


    public void EndEpisode()
    {
        SetBamniWorldActive(episodeRunning = false);
        currentLineIndex = currentSectionIndex = 0;
    }

    public void StartEpisode(Tape tape)
    {
        StartCoroutine(StartNewTape(tape));
    }

    private IEnumerator StartNewTape(Tape tape)
    {
        SetBamniWorldActive(true);
        yield return new WaitForSeconds(tape.GetTapeStartDelay);
        episodeRunning = true;
        currentTape = tape;
        currentEpisodeDialog = currentTape.episodeDialog;
        writingCoroutine = StartCoroutine(WriteText(0.01f));
    }

    public void SetBamniWorldActive(bool active)
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
        mainCam.gameObject.SetActive(!isBamniWorld);

        float t2 = 0f;
        while (t2 <= 1f)
        {
            transitionGroup.alpha = Mathf.Lerp(transitionGroup.alpha, 0f, t2);
            t2 += Time.deltaTime * 2f;
            yield return null;
        }
    }

}
