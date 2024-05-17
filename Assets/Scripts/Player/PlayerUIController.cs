using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    private XRIDefaultInputActions _inputActions;
    private bool _showroundStatistics=false;
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _ammo;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _kills;
    [SerializeField] private Canvas _roundStatistics;
    [SerializeField] private NetworkRoundData _networkRoundData;
    public Health _health;
    public WeaponController _weaponController;


    private void Awake()
    {
        _inputActions = new XRIDefaultInputActions();
        _inputActions.Enable();
    }
    void Update()
    {
        PlayerStatisticUI();
        if (_inputActions.XRILeftHand.YButtonTouched.IsPressed())
            _showroundStatistics = true;
        else
            _showroundStatistics = false;
        GameStatisticUI();
    }
    private void PlayerStatisticUI()
    {
        if (_health != null && _weaponController != null)
        {
            if (_weaponController.Ammo != null)
            {
                _ammo.text = "Ammo\r\n" + _weaponController.Ammo.AmmoInMagazine.ToString();

            }
            else
            {
                _ammo.text = "Ammo\r\n" + 0;
            }
            _hp.text = "HP\r\n" + _health.NetworkedHealth.ToString();
        }
    }
    private void GameStatisticUI()
    {
        if (_showroundStatistics)
        { 
            _roundStatistics.gameObject.SetActive(true);
            _timer.text = _networkRoundData.Time.ToString();
            _kills.text = _networkRoundData.Kills.ToString();
        }
        else
        { _roundStatistics.gameObject.SetActive(false); }
    }
}
