using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    private XRIDefaultInputActions _inputActions;
    private bool _showroundLeftStatistics = false;
    private bool _showroundRightStatistics = false;

    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _ammo;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _kills;
    [SerializeField] private Canvas _roundLeftStatistics;
    [SerializeField] private Canvas _roundRightStatistics;
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
        { _showroundLeftStatistics = true; }
        else
            _showroundLeftStatistics = false;

        if (_inputActions.XRIRightHand.BButtonTouched.IsPressed())
        {
            _showroundRightStatistics = true;
        }
        else
            _showroundRightStatistics = false;


        GameStatisticUI();
    }
    private void PlayerStatisticUI()
    {
        if (_health != null && _weaponController != null)
        {
            if (_weaponController.Ammo != null)
            {
                _ammo.text = "Патроны\r\n" + _weaponController.Ammo.AmmoInMagazine.ToString();

            }
            else
            {
                _ammo.text = "Патроны\r\n" + 0;
            }
            _hp.text = "HP\r\n" + _health.NetworkedHealth.ToString();
        }
    }
    private void GameStatisticUI()
    {
        if (_showroundLeftStatistics)
        {
            _roundLeftStatistics.gameObject.SetActive(true);
            _roundLeftStatistics.GetComponent<RoundStatistic>().Timer.text = _networkRoundData.TimerName + _networkRoundData.Time.ToString();
            _roundLeftStatistics.GetComponent<RoundStatistic>().Kills.text = "Килы: " + _networkRoundData.LocalPlayerKills.ToString();
        }
        else
        { _roundLeftStatistics.gameObject.SetActive(false); }

        if (_showroundRightStatistics)
        {
            _roundRightStatistics.gameObject.SetActive(true);
            _roundRightStatistics.GetComponent<RoundStatistic>().Timer.text = _networkRoundData.TimerName + _networkRoundData.Time.ToString();
            _roundRightStatistics.GetComponent<RoundStatistic>().Kills.text = "Килы: " + _networkRoundData.LocalPlayerKills.ToString();
        }
        else
        { _roundRightStatistics.gameObject.SetActive(false); }
    }
}
