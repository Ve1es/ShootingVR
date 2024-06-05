using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    private XRIDefaultInputActions _inputActions;
    private bool _showroundLeftStatistics = false;
    private bool _showroundRightStatistics = false;

    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _ammo;
    [SerializeField] private Canvas _roundLeftStatistics;
    [SerializeField] private Canvas _roundRightStatistics;
    [SerializeField] private NetworkRoundData _networkRoundData;

    public Health Health;
    public WeaponController WeaponController;


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
        { _showroundLeftStatistics = false; }

        if (_inputActions.XRIRightHand.BButtonTouched.IsPressed())
        { _showroundRightStatistics = true; }
        else
        { _showroundRightStatistics = false; }


        GameStatisticUI();
    }
            
private void PlayerStatisticUI()
    {
        if (Health != null && WeaponController != null)
        {
            if (WeaponController.Ammo != null)
            {
                _ammo.text = "Патроны\r\n" + WeaponController.Ammo.AmmoInMagazine.ToString();

            }
            else
            {
                _ammo.text = "Патроны\r\n" + 0;
            }
            _hp.text = "HP\r\n" + Health.NetworkedHealth.ToString();
        }
    }
    private void GameStatisticUI()
    {
        if (_showroundLeftStatistics)
        {
            _roundRightStatistics.gameObject.SetActive(false);
            _roundLeftStatistics.gameObject.SetActive(true);
            if (_networkRoundData.InNetwork)
            {
                _roundLeftStatistics.GetComponent<RoundStatistic>().Timer.text = _networkRoundData.TimerName + _networkRoundData.Time.ToString();
                _roundLeftStatistics.GetComponent<RoundStatistic>().Kills.text = "Килы: " + _networkRoundData.LocalPlayerKills.ToString();
            }
            else
            {
                _roundLeftStatistics.GetComponent<RoundStatistic>().Timer.text = "";
                _roundLeftStatistics.GetComponent<RoundStatistic>().Kills.text = "";
            }
        }
        else
        { 
            _roundLeftStatistics.gameObject.SetActive(false);
            if (_showroundRightStatistics)
            {
                _roundRightStatistics.gameObject.SetActive(true);
                if (_networkRoundData.InNetwork)
                {
                    _roundRightStatistics.GetComponent<RoundStatistic>().Timer.text = _networkRoundData.TimerName + _networkRoundData.Time.ToString();
                    _roundRightStatistics.GetComponent<RoundStatistic>().Kills.text = "Килы: " + _networkRoundData.LocalPlayerKills.ToString();
                }
                else
                {
                    _roundLeftStatistics.GetComponent<RoundStatistic>().Timer.text = "";
                    _roundLeftStatistics.GetComponent<RoundStatistic>().Kills.text = "";
                }
            }
            else
            { _roundRightStatistics.gameObject.SetActive(false); }
        }
    }
       
}
