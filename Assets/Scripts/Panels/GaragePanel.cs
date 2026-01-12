using UnityEngine;

public class GaragePanel : MonoBehaviour
{
    [SerializeField] private PanelController panelController;

    private void OnEnable()
    {
        if (panelController != null)
        {
            Debug.Log("[GaragePanel] OnEnable -> queue Dialogue panel");
            StartCoroutine(ShowDialogueNextFrame());
        }
    }

    private System.Collections.IEnumerator ShowDialogueNextFrame()
    {
        // Wait a frame so PanelController.Show(Garage) finishes toggling panels.
        yield return null;
        Debug.Log("[GaragePanel] ShowDialogueNextFrame -> show Dialogue panel");
        panelController.Show(PanelType.Dialogue);
    }
}
