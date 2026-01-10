using UnityEngine;

public class IntroPanel : MonoBehaviour
{
    [Header("Slides")]
    [SerializeField] private GameObject[] slideBackgrounds;
    [SerializeField] private GameObject[] slideTextObjects;
    [SerializeField] private float slideDuration = 5f;
    [SerializeField] private PanelController panelController;

    private int slideIndex;
    private Coroutine autoAdvanceRoutine;
    private bool hasCompleted;

    private void OnEnable()
    {
        // Reset to the first slide and kick off the auto-advance timer.
        slideIndex = 0;
        hasCompleted = false;
        ShowSlide(slideIndex);
        StartAutoAdvance();
    }

    private void OnDisable()
    {
        // Stop any timers when the panel is hidden.
        StopAutoAdvance();
    }

    private void Update()
    {
        // Advance immediately on mouse click or touch.
        if (Input.GetMouseButtonDown(0))
        {
            AdvanceSlide();
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            AdvanceSlide();
        }
    }

    public void ShowSlide(int index)
    {
        // Only the current slide's background and text should be visible.
        if (slideBackgrounds != null)
        {
            for (int i = 0; i < slideBackgrounds.Length; i++)
            {
                if (slideBackgrounds[i] != null)
                {
                    slideBackgrounds[i].SetActive(i == index);
                }
            }
        }

        if (slideTextObjects != null)
        {
            for (int i = 0; i < slideTextObjects.Length; i++)
            {
                if (slideTextObjects[i] != null)
                {
                    slideTextObjects[i].SetActive(i == index);
                }
            }
        }
    }

    public void AdvanceSlide()
    {
        // Move to the next slide, or finish the intro if at the end.
        int slideCount = GetSlideCount();
        if (slideCount == 0)
        {
            return;
        }

        if (slideIndex >= slideCount - 1)
        {
            if (!hasCompleted)
            {
                hasCompleted = true;
                // Intro complete: move to the garage panel.
                panelController.Show(PanelType.Garage);
            }
            return;
        }

        slideIndex++;
        ShowSlide(slideIndex);
    }

    private void StartAutoAdvance()
    {
        // Restart the auto-advance coroutine whenever the panel is shown.
        StopAutoAdvance();
        autoAdvanceRoutine = StartCoroutine(AutoAdvanceSlides());
    }

    private void StopAutoAdvance()
    {
        // Safely stop the coroutine to avoid running in the background.
        if (autoAdvanceRoutine != null)
        {
            StopCoroutine(autoAdvanceRoutine);
            autoAdvanceRoutine = null;
        }
    }

    private System.Collections.IEnumerator AutoAdvanceSlides()
    {
        // Wait between slides and advance until the last slide is reached.
        int slideCount = GetSlideCount();
        while (slideIndex < slideCount - 1)
        {
            yield return new WaitForSeconds(slideDuration);
            AdvanceSlide();
        }
    }

    private int GetSlideCount()
    {
        // Use the shortest list to avoid out-of-range errors.
        if (slideBackgrounds == null || slideTextObjects == null)
        {
            return 0;
        }

        return Mathf.Min(slideBackgrounds.Length, slideTextObjects.Length);
    }
}
