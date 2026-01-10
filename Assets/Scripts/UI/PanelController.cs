using UnityEngine;

public class PanelController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject cityPanel;
    [SerializeField] private GameObject garagePanel;
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject globalHUDPanel;

    [Header("Startup")]
    [SerializeField] private PanelType startPanel = PanelType.Intro;

    // Controls which panel is active; no button logic lives here.
    public void Show(PanelType panel)
    {
        introPanel.SetActive(panel == PanelType.Intro);
        cityPanel.SetActive(panel == PanelType.City);
        garagePanel.SetActive(panel == PanelType.Garage);
        profilePanel.SetActive(panel == PanelType.Profile);
        globalHUDPanel.SetActive(panel != PanelType.Intro);
    }

    private void Start()
    {
        Show(startPanel);
    }
}
