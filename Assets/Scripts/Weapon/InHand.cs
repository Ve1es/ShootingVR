using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InHand : NetworkBehaviour
{
    private Transform _currentPosition;

    [SerializeField] private XRSimpleInteractable _interactable;
    [SerializeField] private float _moveSpeedInHand = 10;
    [SerializeField] private Transform _currentPositionOnBelt;

    public override void Spawned()
    {
        _interactable = GetComponent<XRSimpleInteractable>();
        _interactable.selectEntered.AddListener((args) => OnSelectEnter(args));
        _currentPosition = _currentPositionOnBelt;
    }
    private void OnSelectEnter(SelectEnterEventArgs interactor)
    {
        _currentPosition = interactor.interactorObject.transform;

    }
    public override void FixedUpdateNetwork()
    {
        if (_currentPosition != null)
        {
            if (!(Object.transform.position == _currentPosition.position && Object.transform.rotation == _currentPosition.rotation))
            {
               // Object.transform.position = Vector3.Lerp(transform.position, _currentPosition.position, Time.deltaTime * _moveSpeedInHand);
                Object.transform.position = new Vector3(_currentPosition.position.x, _currentPosition.position.y, _currentPosition.position.z);
                Object.transform.rotation = _currentPosition.rotation;
            }
        }
    }
    public void ReturnOnBelt()
    {
        _currentPosition = _currentPositionOnBelt;
    }
}
