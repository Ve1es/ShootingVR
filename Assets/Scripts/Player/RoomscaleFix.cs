using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(XROrigin))]
public class RoomscaleFix : MonoBehaviour
{
    private CharacterController _characterController;
    private XROrigin _xrOrigin;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _xrOrigin = GetComponent<XROrigin>();
    }
    void Update()
    {
        _characterController.height = _xrOrigin.CameraInOriginSpaceHeight + 0.15f;

        var centerPoint = transform.InverseTransformPoint(_xrOrigin.Camera.transform.position);
        _characterController.center = new Vector3(
            centerPoint.x,
            _characterController.height / 2 + _characterController.skinWidth,
            centerPoint.z);
        _characterController.Move(new Vector3(0.001f, -0.001f, 0.001f));
        _characterController.Move(new Vector3(-0.001f, -0.001f, -0.001f));
    }
}
