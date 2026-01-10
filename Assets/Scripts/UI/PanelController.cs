using UnityEngine;

public class PanelController : MonoBehaviour
{
    [System.Serializable]
    private class PanelEntry
    {
        [SerializeField] private PanelType type;
        [SerializeField] private GameObject panel;
        [SerializeField] private bool isOverlay;

        public PanelType Type => type;
        public GameObject Panel => panel;
        public bool IsOverlay => isOverlay;
    }

    [Header("Panels")]
    [SerializeField] private PanelEntry[] panels;

    [Header("Startup")]
    [SerializeField] private PanelType startPanel = PanelType.Intro;

    // Controls which panel is active; no button logic lives here.
    public void Show(PanelType panel)
    {
        PanelEntry target = null;
        if (panels != null)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                PanelEntry entry = panels[i];
                if (entry != null && entry.Type == panel)
                {
                    target = entry;
                    break;
                }
            }
        }

        // If the panel type isn't registered, bail out to avoid toggling the wrong objects.
        if (target == null)
        {
            Debug.LogWarning($"[PanelController] No panel entry found for {panel}.");
            return;
        }

        // Overlay panels stack on top of the current main panel, so only enable the overlay itself.
        if (target.IsOverlay)
        {
            if (target.Panel != null)
            {
                target.Panel.SetActive(true);
            }
            return;
        }

        // For main panels, enable the requested one and disable all other main panels.
        if (panels != null)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                PanelEntry entry = panels[i];
                if (entry == null || entry.Panel == null || entry.IsOverlay)
                {
                    continue;
                }

                entry.Panel.SetActive(entry == target);
            }
        }
    }

    private void Start()
    {
        Show(startPanel);
    }
}
