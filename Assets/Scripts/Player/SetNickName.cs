using TMPro;
using UnityEngine;

public class SetNickName : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nickName;
    [SerializeField] private PlayerController _playerController;

    public void SetNick()
    {
        _playerController.NickName = _nickName.text;
    }
}
