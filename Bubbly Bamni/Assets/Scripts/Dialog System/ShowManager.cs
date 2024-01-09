using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowManager : MonoBehaviour
{
    [SerializeField] CanvasGroup transitionGroup;
    [SerializeField] TextMeshProUGUI episodeNameText, speakerNameText, contentText;
    [SerializeField] GameObject atticScene;
    [SerializeField] Color bamniAmbientColor;
    private bool episodeRunning, writingLine;
    public ClickingInteractable currentTargetClickedInterctable;
    public Coroutine writingCoroutine; // can access through the clicking interactable when writing the mistake line
    private int currentSectionIndex, currentLineIndex;
    private float textTimer;
    private DialogSection[] currentEpisodeDialog;
    private Tape currentTape;
    private Camera mainCam;
    public void SetEpisodeRunning(bool active) => episodeRunning = active;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (episodeRunning)
        {
            UpdateEpisode();
        }
    }

    private void UpdateEpisode()
    {
        CameraPlayerController.SetCursorStateLocked();
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
                if (currentEpisodeDialog.Length - 1 == currentSectionIndex) EndEpisode();
                else if (!currentSection.IsBreakAfterPlay) MoveToNextSection();
                else
                {
                    currentTargetClickedInterctable = currentSection.GetTargetClickingInteractable;
                    Cursor.lockState = CursorLockMode.None;
                    episodeRunning = false;
                }
            }
            else
            {
                MoveToNextLine();
            }

            textTimer = 0f;
        }

        if (!writingLine) textTimer += Time.deltaTime;
    }

    private void MoveToNextLine()
    {
        // move to the next line
        currentLineIndex++;
        StopCoroutine(writingCoroutine);
        writingCoroutine = StartCoroutine(WriteLine(currentEpisodeDialog[currentSectionIndex].GetDialogLines[currentLineIndex]));
    }

    public void MoveToNextSection()
    {
        // move to the next section
        currentSectionIndex++;
        currentLineIndex = 0;
        StopCoroutine(writingCoroutine);
        writingCoroutine = StartCoroutine(WriteLine(currentEpisodeDialog[currentSectionIndex].GetDialogLines[currentLineIndex]));
    }

    public IEnumerator WriteLine(DialogLine dialogLine)
    {
        writingLine = true;
        Debug.Log($"Starting to write with line index: {currentLineIndex} and section index: {currentSectionIndex} and speaker: {dialogLine.GetSpeakerName}");
        speakerNameText.text = contentText.text = string.Empty;
        for (int i = 0; i < dialogLine.GetSpeakerName.Length; i++)
        {
            contentText.text += dialogLine.GetSpeakerName[i];
            yield return new WaitForSeconds(dialogLine.GetLineWritingLetterDelay);
        }
        contentText.text += ":";
        contentText.text += " ";

        for (int i = 0; i < dialogLine.GetLineText.Length; i++)
        {
            contentText.text += dialogLine.GetLineText[i];
            yield return new WaitForSeconds(dialogLine.GetLineWritingLetterDelay);
        }

        writingLine = false;
    }


    public void EndEpisode()
    {
        SetBamniWorldActive(episodeRunning = false, currentTape.GetWorldToActivate);
        currentLineIndex = currentSectionIndex = 0;
    }

    public void StartEpisode(Tape tape)
    {
        StartCoroutine(StartNewTape(tape));
    }

    private IEnumerator StartNewTape(Tape tape)
    {
        SetBamniWorldActive(true, tape.GetWorldToActivate);
        yield return new WaitForSeconds(tape.GetTapeStartDelay);
        episodeRunning = true;
        currentTape = tape;
        currentEpisodeDialog = currentTape.episodeDialog;
        if (writingCoroutine != null) StopCoroutine(writingCoroutine);
        writingCoroutine = StartCoroutine(WriteLine(currentEpisodeDialog[currentSectionIndex].GetDialogLines[currentLineIndex]));
    }

    public void SetBamniWorldActive(bool active, GameObject world)
    {
        StartCoroutine(TransitionToWorld(active, world));
    }

    private IEnumerator TransitionToWorld(bool isBamniWorld, GameObject world)
    {
        float t = 0f;
        while (t <= 1f)
        {
            transitionGroup.alpha = Mathf.Lerp(transitionGroup.alpha, 1f, t);
            t += Time.deltaTime * 2f;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        atticScene.SetActive(!isBamniWorld);
        mainCam.gameObject.SetActive(!isBamniWorld);
        world.SetActive(isBamniWorld);
        RenderSettings.ambientLight = isBamniWorld ? bamniAmbientColor : Color.black;
        float t2 = 0f;
        while (t2 <= 1f)
        {
            transitionGroup.alpha = Mathf.Lerp(transitionGroup.alpha, 0f, t2);
            t2 += Time.deltaTime * 2f;
            yield return null;
        }
    }

}
