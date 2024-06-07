using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InHand : NetworkBehaviour
{
    private Transform _currentPosition;

    [SerializeField] private float _moveSpeedInHand = 10;
    [SerializeField] private XRSimpleInteractable _interactable;
    [SerializeField] private Transform _currentPositionOnBelt;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private NetworkClone _clone;
    [SerializeField] private MeshRenderer[] _model;

    [Networked] public bool RightHandBool { get; set; }
    [Networked] public bool LeftHandBool { get; set; }

    public bool InHandBool = false;
    public GameObject TestObject;

    public override void Spawned()
    {
        _currentPosition = _currentPositionOnBelt;
        if (!Object.HasStateAuthority)
        {
            foreach (var model in _model)
            {
                model.enabled = false;
            }
        }
    }
    public void OnSelectEnter(SelectEnterEventArgs interactor)
    {
        if (!InHandBool)
        {
            InHandBool = true;
            TestObject = interactor.interactorObject.transform.gameObject;
            if (Object.HasStateAuthority)
            {
                if (TestObject.GetComponent<RightHand>())
                {
                    Debug.LogError("Right");
                    RightHandBool = true;
                    LeftHandBool = false;
                }
                if (TestObject.GetComponent<LeftHand>())
                {
                    Debug.LogError("Left");
                    LeftHandBool = true;
                    RightHandBool = false;
                }
            }
        }
    }
    public override void FixedUpdateNetwork()
    {
        if (!InHandBool)
        {
            if (Object.transform.position != _currentPosition.position)
                Object.transform.position = _currentPosition.position;
            if (Object.transform.rotation != _currentPosition.rotation)
                Object.transform.rotation = _currentPosition.rotation;
        }
    }
    public void ReturnOnBelt()
    {
        InHandBool = false;
        _currentPosition = _currentPositionOnBelt;
        Object.transform.position = _currentPosition.position;
        Object.transform.rotation = _currentPosition.rotation;
    }
}
