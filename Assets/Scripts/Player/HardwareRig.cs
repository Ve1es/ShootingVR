using UnityEngine;

public class HardwareRig : MonoBehaviour
{
    public Transform GetTransform() { return gameObject.transform; }
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform[] _spawnPoints;
    //[SerializeField] public PlayerSpawner PlayerSpawner;

    private float tpTime = 1;
    private bool tp = false;

    public void Teleport(Transform spawnPoint)
    {
        _spawnPoint = spawnPoint;
        tp = true;
    }
    public void RespawnTeleport()
    {
        Debug.LogError(Random.Range(0, _spawnPoints.Length));
        _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Debug.LogError(_spawnPoint.transform.position);
        gameObject.transform.position = _spawnPoint.position;
    }
    public void Update()
    {
        if (tp)
        {
            tpTime = tpTime - 0.1f;
            if (tpTime<0)
            {
                gameObject.transform.position = _spawnPoint.position;
                tp = false;
            } 
        }
    }

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
