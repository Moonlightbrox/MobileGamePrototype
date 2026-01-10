using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogues", menuName = "Content/Dialogues")]
public class Dialogues : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private List<DialogueParticipant> participants = new();
    [SerializeField] private List<DialogueLine> lines = new();
    [SerializeField] private bool isOneTime;

    public string Id => id;
    public IReadOnlyList<DialogueParticipant> Participants => participants;
    public IReadOnlyList<DialogueLine> Lines => lines;
    public bool IsOneTime => isOneTime;
}

[System.Serializable]
public class DialogueParticipant
{
    [SerializeField] private Characters character;
    [SerializeField] private bool startHidden;

    public Characters Character => character;
    public bool StartHidden => startHidden;
}

[System.Serializable]
public class DialogueLine
{
    [SerializeField] private Characters speaker;
    [SerializeField] private string text;
    [SerializeField] private bool revealSpeaker;

    public Characters Speaker => speaker;
    public string Text => text;
    public bool RevealSpeaker => revealSpeaker;
}
