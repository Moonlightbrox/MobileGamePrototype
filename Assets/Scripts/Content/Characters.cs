using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Content/Characters")]
public class Characters : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField] private string unknownName;
    [SerializeField] private Sprite portrait;
    [SerializeField] private Sprite unknownPortrait;

    public string Id => id;
    public string DisplayName => displayName;
    public string UnknownName => unknownName;
    public Sprite Portrait => portrait;
    public Sprite UnknownPortrait => unknownPortrait;
}
