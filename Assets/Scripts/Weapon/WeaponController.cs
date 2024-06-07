using Fusion;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : NetworkBehaviour
{
    private const int MinAmmo = 0;

    private XRIDefaultInputActions _inputActions;
    private bool _canShoot = true;

    [SerializeField] private NetworkPrefabRef _bulletPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private XRSocketInteractor _socketInteractor;
    [SerializeField] private XRSimpleInteractable _simpleInteractor;
    [SerializeField] private Animator _animator;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private NetworkPosition _networkPosition;
    [SerializeField] private float _delay = 0.5f;

    public Ammo Ammo = null;
    public float Damage = 10;
    public Transform SpawnBulletPoint;

    public void Shooting(ActivateEventArgs args)
    {
        if (!CheckHand(args)) { return; }
        if (HasStateAuthority == false || Ammo == null || Ammo.AmmoInMagazine <= MinAmmo) { return; }
        if (_canShoot)
        {
            _canShoot = false;
            CreateBullet();
            StartCoroutine(DelayCoroutine());
            _animator.SetTrigger("Fire");
            Ammo.AmmoInMagazine--;
            if (muzzleFlashPrefab)
            {
                GameObject tempFlash;
                tempFlash = Instantiate(muzzleFlashPrefab, _gunBarrel.position, _gunBarrel.rotation);
                Destroy(tempFlash, destroyTimer);
            }
        }
    }
    private bool CheckHand(ActivateEventArgs args)
    {
        if(args.interactorObject.transform.gameObject.GetComponent<RightHand>()&& gameObject.GetComponent<InHand>().RightHandBool)
        {
            return true;
        }
        if (args.interactorObject.transform.gameObject.GetComponent<LeftHand>() && gameObject.GetComponent<InHand>().LeftHandBool)
        {
            return true;
        }
        return false;
    }
    IEnumerator DelayCoroutine()
    {
      yield return new WaitForSeconds(_delay);
        _canShoot = true;
    }
    public void CreateBullet()
    {
        Runner.Spawn(_bulletPrefab,
         SpawnBulletPoint.position,
         SpawnBulletPoint.rotation,
         Object.InputAuthority,
         (runner, o) =>
         {
             o.GetComponent<Bullet>().Init();
             o.GetComponent<DealDamage>().PlayerID = Runner.LocalPlayer.PlayerId;
         });
    }
    public void Reload()
    {
        IXRSelectInteractable objectInSocet = _socketInteractor.GetOldestInteractableSelected();
        Ammo = objectInSocet.transform.gameObject.GetComponent<Ammo>();
        objectInSocet.transform.gameObject.GetComponent<Ammo>().DisableGrabLayer();
    }
    public void DropMagazine()
    {
        Ammo = null;
    }
    public void Drop(InputAction.CallbackContext context)
    {
        if (gameObject.GetComponent<InHand>().RightHandBool&& gameObject.GetComponent<InHand>().InHandBool)
        {
            if (context.action == _inputActions.XRIRightHand.AButton)
            { Ammo.gameObject.GetComponent<XRGrabInteractable>().enabled = false; }
        }
        if (gameObject.GetComponent<InHand>().LeftHandBool && gameObject.GetComponent<InHand>().InHandBool)
        {
            if (context.action == _inputActions.XRILeftHand.XButton)
            { Ammo.gameObject.GetComponent<XRGrabInteractable>().enabled = false; }
        }
    }


    private void Awake()
    {
        _inputActions = new XRIDefaultInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.XRILeftHand.XButton.performed  += Drop;
        _inputActions.XRIRightHand.AButton.performed += Drop;
    }

    private void OnDisable()
    {
        _inputActions.XRILeftHand.XButton.performed -= Drop;
        _inputActions.XRIRightHand.AButton.performed -= Drop;
        _inputActions.Disable();
    }
}
