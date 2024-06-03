using Fusion;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : NetworkBehaviour
{
    private const int Aim_Range = 10000;
    private const int MinAmmo = 0;

    [SerializeField] private NetworkPrefabRef _bulletPrefab = NetworkPrefabRef.Empty;

    [SerializeField] private XRSocketInteractor _socketInteractor;
    [SerializeField] private XRSimpleInteractable _simpleInteractor;
    [SerializeField] private Animator _animator;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private NetworkPosition _networkPosition;

    public Ammo Ammo = null;
    public float Damage = 10;
    public Transform SpawnBulletPoint;

    public void Shooting()
    {
        if (HasStateAuthority == false || Ammo == null || Ammo.AmmoInMagazine <= MinAmmo) { return; }
        _animator.SetTrigger("Fire");
        CreateBullet();
        Ammo.AmmoInMagazine--;
        if (muzzleFlashPrefab)
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, _gunBarrel.position, _gunBarrel.rotation);
            Destroy(tempFlash, destroyTimer);
        }

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
    }
    public void DropMagazine()
    {
        Ammo = null;
    }
}
