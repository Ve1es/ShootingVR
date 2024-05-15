using UnityEngine;
using UnityEngine.InputSystem;

public class HardwareRig : MonoBehaviour
{
    private XRIDefaultInputActions _inputActions;
    [SerializeField] private Transform _playerTransform;
    public Transform GetTransform() { return _playerTransform; }

    //private void Awake()
    //{
    //    _inputActions = new XRIDefaultInputActions();
    //    _inputActions.Enable();
    //}

    //private void OnEnable()
    //{
    //    _inputActions.XRILeftHand.XButton.performed += ReloadX;
    //    _inputActions.XRIRightHand.AButton.performed += ReloadA;
    //}

    //private void ReloadA(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("Нажата кнопка A");
    //}
    //private void ReloadX(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("Нажата кнопка X");
    //}
}
