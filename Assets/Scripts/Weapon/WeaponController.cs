using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : NetworkBehaviour
{
    private const int Aim_Range = 10000;
    [SerializeField] private XRSocketInteractor _socketInteractor;
    [SerializeField] private XRSimpleInteractable _simpleInteractor;
    [SerializeField] private Animator _animator;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private GameObject muzzleFlashPrefab;
    public Ammo Ammo = null;
    public float Damage = 10;
    //public int Ammo
    //{
    //    get { return Ammo; }
    //    set { Ammo = value; }
    //}

    public override void Spawned()
    {
        //_socketInteractor.hoverEntered.AddListener(Reload());
        //_simpleInteractor.selectEntered.AddListener((args) => OnSelectEnter(args));
        //Ammo = 0;
    }
    public void Shooting()
    {
        if (HasStateAuthority == false || Ammo == null || Ammo.AmmoInMagazine <= 0) { return; }
        _animator.SetTrigger("Fire");
        Ray ray = new Ray(_gunBarrel.position, _gunBarrel.forward);
        if (Runner.GetPhysicsScene().Raycast(ray.origin, ray.direction, out var hit))
        {
            if (hit.transform.TryGetComponent<Health>(out var health))
            {
                health.DealDamageRpc(Damage);
            }
        }
        // Ammo--;
        Ammo.AmmoInMagazine --;
        if (muzzleFlashPrefab)
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, _gunBarrel.position, _gunBarrel.rotation);
            Destroy(tempFlash, destroyTimer);
        }

    }
    public void Update()
    {
        if (HasStateAuthority == false) return;
        _lineRenderer.SetPosition(0, _gunBarrel.position);
        Vector3 newPosition = _gunBarrel.position + transform.forward * Aim_Range;
        _lineRenderer.SetPosition(1, newPosition);
    }
    public void Reload()
    {
        IXRSelectInteractable objectInSocet = _socketInteractor.GetOldestInteractableSelected();
        Ammo = objectInSocet.transform.gameObject.GetComponent<Ammo>();
        // Ammo = _ammoInMagazine.AmmoInMagazine;
    }


    public void DropMagazine()
    {
        Ammo = null;
       // Ammo = 0;
    }
}
