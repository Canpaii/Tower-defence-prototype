using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraAndPanelMove : MonoBehaviour
{
    public Camera cameraToAdjust;
    public Rect startViewportRect;
    public Rect targetViewportRect;
    public RectTransform panel;
    public Vector3 panelStartPosition;
    public Vector3 panelTargetPosition;
    public float cameraTransitionSpeed = 2f;
    public float panelMoveSpeed = 500f;
    public KeyCode triggerKey = KeyCode.Tab;
    public Button triggerButton;

    private bool isTransitioning = false;
    private Coroutine viewportCoroutine;
    private Coroutine panelCoroutine;

    void Start()
    {
        cameraToAdjust.rect = startViewportRect;
        panel.anchoredPosition = panelStartPosition;
        triggerButton.onClick.AddListener(ToggleCameraAndPanel);
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            ToggleCameraAndPanel();
        }
    }

    void ToggleCameraAndPanel()
    {
        if (viewportCoroutine != null)
        {
            StopCoroutine(viewportCoroutine);
        }

        if (panelCoroutine != null)
        {
            StopCoroutine(panelCoroutine);
        }

        if (isTransitioning)
        {
            viewportCoroutine = StartCoroutine(AdjustViewport(startViewportRect));
            panelCoroutine = StartCoroutine(MovePanel(panelStartPosition));
        }
        else
        {
            viewportCoroutine = StartCoroutine(AdjustViewport(targetViewportRect));
            panelCoroutine = StartCoroutine(MovePanel(panelTargetPosition));
        }

        isTransitioning = !isTransitioning;
    }

    IEnumerator AdjustViewport(Rect destination)
    {
        Rect currentRect = cameraToAdjust.rect;

        while (Mathf.Abs(currentRect.x - destination.x) > 0.01f || Mathf.Abs(currentRect.y - destination.y) > 0.01f)
        {
            currentRect = new Rect(
                Mathf.Lerp(cameraToAdjust.rect.x, destination.x, cameraTransitionSpeed * Time.deltaTime),
                Mathf.Lerp(cameraToAdjust.rect.y, destination.y, cameraTransitionSpeed * Time.deltaTime),
                Mathf.Lerp(cameraToAdjust.rect.width, destination.width, cameraTransitionSpeed * Time.deltaTime),
                Mathf.Lerp(cameraToAdjust.rect.height, destination.height, cameraTransitionSpeed * Time.deltaTime)
            );

            cameraToAdjust.rect = currentRect;
            yield return null;
        }

        cameraToAdjust.rect = destination;
        viewportCoroutine = null;
    }

    IEnumerator MovePanel(Vector3 destination)
    {
        while (Vector3.Distance(panel.anchoredPosition, destination) > 0.1f)
        {
            panel.anchoredPosition = Vector3.MoveTowards(panel.anchoredPosition, destination, panelMoveSpeed * Time.deltaTime);
            yield return null;
        }

        panel.anchoredPosition = destination;
        panelCoroutine = null;
    }
}
