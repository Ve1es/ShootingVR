using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _ammo;
    public Health _health;
    public WeaponController _weaponController;
    void Update()
    {
        if (_health != null && _weaponController != null)
        {
            if (_weaponController.Ammo != null)
            { _ammo.text = "Ammo\r\n" + _weaponController.Ammo.AmmoInMagazine.ToString(); }
            else
            { _ammo.text = "Ammo\r\n" + 0; }
            _hp.text = "HP\r\n" + _health.NetworkedHealth.ToString();
        }
    }
}
