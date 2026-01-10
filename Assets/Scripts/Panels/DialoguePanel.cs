using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    [Header("Test")]
    [SerializeField] private Dialogues testDialogue;
    

    private Dialogues currentDialogue;
    private int currentLineIndex;
    private readonly Dictionary<Characters, bool> revealState = new Dictionary<Characters, bool>();

    private void OnEnable()
    {
        Debug.Log($"[DialoguePanel] OnEnable -> activeInHierarchy={gameObject.activeInHierarchy} testDialogue={(testDialogue != null ? testDialogue.name : "null")}");
        StartDialogue(testDialogue);
    }

    public void StartDialogue(Dialogues dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        revealState.Clear();

        if (currentDialogue != null && currentDialogue.Participants != null)
        {
            foreach (var participant in currentDialogue.Participants)
            {
                if (participant != null && participant.Character != null && !revealState.ContainsKey(participant.Character))
                {
                    revealState.Add(participant.Character, !participant.StartHidden);
                }
            }
        }

        ShowCurrentLine();
    }

    private void Update()
    {
        if (currentDialogue == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            AdvanceDialogue();
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            AdvanceDialogue();
        }
    }

    private void ShowCurrentLine()
    {
        if (currentDialogue == null || currentDialogue.Lines == null || currentDialogue.Lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (currentLineIndex < 0 || currentLineIndex >= currentDialogue.Lines.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentDialogue.Lines[currentLineIndex];
        Characters speaker = line.Speaker;

        if (line.RevealSpeaker && speaker != null)
        {
            revealState[speaker] = true;
        }

        bool isRevealed = speaker != null && revealState.ContainsKey(speaker) && revealState[speaker];
        string resolvedName = ResolveSpeakerName(speaker, isRevealed);
        Sprite resolvedPortrait = ResolveSpeakerPortrait(speaker, isRevealed);

        SetText(nameText, resolvedName);
        SetText(dialogueText, line.Text);

        if (portraitImage != null)
        {
            portraitImage.sprite = resolvedPortrait;
            portraitImage.enabled = resolvedPortrait != null;
        }
    }

    private void AdvanceDialogue()
    {
        if (currentDialogue == null || currentDialogue.Lines == null)
        {
            EndDialogue();
            return;
        }

        if (currentLineIndex >= currentDialogue.Lines.Count - 1)
        {
            EndDialogue();
            return;
        }

        currentLineIndex++;
        ShowCurrentLine();
    }

    private void EndDialogue()
    {
        currentDialogue = null;
        currentLineIndex = 0;
        revealState.Clear();

        // TODO: Decide which panel should be shown after dialogue ends.
        Debug.Log("[DialoguePanel] EndDialogue -> disabling DialoguePanel");
        gameObject.SetActive(false);
    }

    private static void SetText(TMP_Text text, string value)
    {
        if (text != null)
        {
            text.text = value;
        }
    }

    private static string ResolveSpeakerName(Characters speaker, bool isRevealed)
    {
        if (speaker == null)
        {
            return string.Empty;
        }

        if (!isRevealed && !string.IsNullOrEmpty(speaker.UnknownName))
        {
            return speaker.UnknownName;
        }

        return speaker.DisplayName;
    }

    private static Sprite ResolveSpeakerPortrait(Characters speaker, bool isRevealed)
    {
        if (speaker == null)
        {
            return null;
        }

        if (!isRevealed && speaker.UnknownPortrait != null)
        {
            return speaker.UnknownPortrait;
        }

        return speaker.Portrait;
    }
}
