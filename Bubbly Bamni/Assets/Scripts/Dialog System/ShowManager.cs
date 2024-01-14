using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowManager : MonoBehaviour
{
    [SerializeField] CanvasGroup transitionGroup;
    [SerializeField] GameObject showCanavs;
    [SerializeField] TextMeshProUGUI episodeNameText, speakerNameText, contentText;
    [SerializeField] GameObject atticScene;
    [SerializeField] Color bamniAmbientColor;
    [SerializeField] Animator[] bamniAnimator, jammyAnimator;
    [SerializeField] GameObject jumpscare;
    private LevelManager levelManager;
    public bool episodeRunning { get; private set; }
    public bool writingLine { get; private set; }
    public ClickingInteractable currentTargetClickedInterctable;
    public Coroutine writingCoroutine; // can access through the clicking interactable when writing the mistake line
    private int currentSectionIndex, currentLineIndex;
    private float textTimer;
    private DialogSection[] currentEpisodeDialog;
    private Tape currentTape;
    private Camera mainCam;
    private PlayerInventory playerInventory;
    public delegate void OnEpisodeEvent();
    public OnEpisodeEvent onEndEpisode;
    public OnEpisodeEvent onStartEpisode;

    public void SetEpisodeRunning(bool active) => episodeRunning = active;

    private void Start()
    {
        mainCam = Camera.main;
        showCanavs.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        //StartCoroutine(ActiveJumpscare(4f));
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
        if (dialogLine.GetAnimationName != string.Empty)
        {
            if (dialogLine.GetSpeakerName == "Bamni")
            {
                foreach (var animator in bamniAnimator)
                {
                    animator.Play(dialogLine.GetAnimationName);
                }
            }
            else if (dialogLine.GetSpeakerName == "Jammy")
            {
                foreach (var animator in jammyAnimator)
                {
                    animator.Play(dialogLine.GetAnimationName);
                }
            }
        }


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
        onEndEpisode?.Invoke();
        if (playerInventory.GetCurrentItem.nameId == "End Tape")
        {
            StartCoroutine(ActiveJumpscare(4f));
        }
    }

    public void StartEpisode(Tape tape)
    {
        StartCoroutine(StartNewTape(tape));
    }

    private IEnumerator ActiveJumpscare(float jumpscareDuration)
    {
        mainCam.gameObject.SetActive(false);
        jumpscare.SetActive(true);
        yield return new WaitForSeconds(jumpscareDuration);
        levelManager.LoadLevelByIndex(0);
    }

    private IEnumerator StartNewTape(Tape tape)
    {
        onStartEpisode?.Invoke();
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
        showCanavs.SetActive(isBamniWorld);
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
