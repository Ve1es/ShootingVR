using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayVisibilityController : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    //public LineRenderer lineRenderer;
    public XRInteractorLineVisual xRInteractorLineVisual;

    void Start()
    {
        if (rayInteractor == null)
        {
            rayInteractor = GetComponent<XRRayInteractor>();
        }
        if (xRInteractorLineVisual == null)
        {
            xRInteractorLineVisual = GetComponent<XRInteractorLineVisual>();
        }

        // Отключаем Line Renderer по умолчанию
        xRInteractorLineVisual.enabled = false;

        // Подписываемся на события
        rayInteractor.hoverEntered.AddListener(OnHoverEntered);
        rayInteractor.hoverExited.AddListener(OnHoverExited);
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Включаем Line Renderer при наведении на UI элемент
        xRInteractorLineVisual.enabled = true;
    }

    void OnHoverExited(HoverExitEventArgs args)
    {
        // Отключаем Line Renderer, когда луч уходит с UI элемента
        xRInteractorLineVisual.enabled = false;
    }

    void OnDestroy()
    {
        // Отписываемся от событий, чтобы избежать утечек памяти
        rayInteractor.hoverEntered.RemoveListener(OnHoverEntered);
        rayInteractor.hoverExited.RemoveListener(OnHoverExited);
    }
}