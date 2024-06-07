using TMPro;
using UnityEngine;

public class Nickname : MonoBehaviour
{
    private const float PlayerHeight = 2;
    private const float Turnaround = 90;

    public RectTransform CanvasRectTransform;
    public Transform PlayerViewer;
    public Transform PlayerParent;
    public TMP_Text Nick;
    public Transform NicknamePosition;

    public void SetNickname(string nickname)
    {
        Nick.text = nickname;
    }
    private void Start()
    {
        PlayerViewer = FindObjectOfType<PlayerController>().Head.transform;
        CanvasRectTransform = gameObject.GetComponent<RectTransform>();
    }
    private void Update()
    {
        Vector3 direction = PlayerViewer.position - CanvasRectTransform.position;
        direction.y = 0; 
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -angle - Turnaround, 0));
        NicknamePosition.position = new Vector3(PlayerParent.transform.position.x, PlayerParent.transform.position.y + PlayerHeight, PlayerParent.transform.position.z);
        NicknamePosition.rotation = targetRotation;

        CanvasRectTransform.position = NicknamePosition.position;
        CanvasRectTransform.rotation = NicknamePosition.rotation;

    }
}
