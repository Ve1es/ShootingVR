using TMPro;
using UnityEngine;

public class Nickname : MonoBehaviour
{
    [SerializeField] public RectTransform _canvasRectTransform;

    public Transform _playerViewer;
    public Transform _playerParent;
    public TMP_Text Nick;

    public void SetNickname(string nickname)
    {
        Nick.text = nickname;
    }
    private void Start()
    {
        _playerViewer = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        if (_playerViewer != null)
        {
            _canvasRectTransform.LookAt(_playerViewer);
            _canvasRectTransform.Rotate(0, 180, 0);
        }
        if(_playerParent.transform.position != transform.position)
        {
            transform.position = new Vector3(_playerParent.transform.position.x, _playerParent.transform.position.y+2.5f, _playerParent.transform.position.z);
        }
    }
}
