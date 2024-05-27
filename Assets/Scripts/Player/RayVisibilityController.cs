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

        // ��������� Line Renderer �� ���������
        xRInteractorLineVisual.enabled = false;

        // ������������� �� �������
        rayInteractor.hoverEntered.AddListener(OnHoverEntered);
        rayInteractor.hoverExited.AddListener(OnHoverExited);
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        // �������� Line Renderer ��� ��������� �� UI �������
        xRInteractorLineVisual.enabled = true;
    }

    void OnHoverExited(HoverExitEventArgs args)
    {
        // ��������� Line Renderer, ����� ��� ������ � UI ��������
        xRInteractorLineVisual.enabled = false;
    }

    void OnDestroy()
    {
        // ������������ �� �������, ����� �������� ������ ������
        rayInteractor.hoverEntered.RemoveListener(OnHoverEntered);
        rayInteractor.hoverExited.RemoveListener(OnHoverExited);
    }
}