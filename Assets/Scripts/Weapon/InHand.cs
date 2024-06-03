using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InHand : NetworkBehaviour
{
    private Transform _currentPosition;
   // private bool _stateAuthority;
    [Networked] private bool _rightHandBool { get; set; }
    
    [SerializeField] private XRSimpleInteractable _interactable;
    [SerializeField] private float _moveSpeedInHand = 10;
    [SerializeField] private Transform _currentPositionOnBelt;
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _leftHand;
    [SerializeField] private NetworkClone _clone;
    [SerializeField] private MeshRenderer[] _model;
    public GameObject TestObject;

    public override void Spawned()
    {
        _interactable = GetComponent<XRSimpleInteractable>();
        _interactable.selectEntered.AddListener((args) => OnSelectEnter(args));
        _currentPosition = _currentPositionOnBelt;
        if (!Object.HasStateAuthority)
        {
            foreach (var model in _model)
            {
                model.enabled = false;
            }
        }
    }
    private void OnSelectEnter(SelectEnterEventArgs interactor)
    {
        TestObject = interactor.interactorObject.transform.gameObject;
        if (Object.HasStateAuthority)
        {
            if(TestObject.GetComponent<RightHand>())
            {
                _rightHandBool = true;
            }
            else
            {
                _rightHandBool = false;
            }
        }
            Object.transform.parent = TestObject.transform;
        _currentPosition = TestObject.transform;
        if(!Object.HasStateAuthority)
        {
            if(_rightHandBool)
            _clone.Parent = _rightHand;
            else
            _clone.Parent = _leftHand;

           
        }
        //if (TestObject == null)
        //{
        //    Object.transform.parent = _rightHand.transform;
        //    _currentPosition = _rightHand.transform;
        //}

        //Runner.Spawn(_clonePrefab, Object.transform.position, Object.transform.rotation);

    }
    public override void FixedUpdateNetwork()
    {
        Object.transform.position = _currentPosition.position;
        Object.transform.rotation = _currentPosition.rotation;
    }
    public void ReturnOnBelt()
    {
        transform.parent = _parent;
        _currentPosition = _currentPositionOnBelt;
        Object.transform.position = _currentPosition.position;
        Object.transform.rotation = _currentPosition.rotation;
    }
}
