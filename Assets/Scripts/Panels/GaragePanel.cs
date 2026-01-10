using UnityEngine;

public class GaragePanel : MonoBehaviour
{
    [SerializeField] private PanelController panelController;

    private void OnEnable()
    {
        if (panelController != null)
        {
            StartCoroutine(ShowDialogueNextFrame());
        }
    }

    private System.Collections.IEnumerator ShowDialogueNextFrame()
    {
        // Wait a frame so PanelController.Show(Garage) finishes toggling panels.
        yield return null;
        panelController.Show(PanelType.Dialogue);
    }
}
