using TMPro;
using UnityEngine;

public class Nickname : MonoBehaviour
{
    public RectTransform CanvasRectTransform;
    public Transform PlayerViewer;
    public Transform PlayerParent;
    public TMP_Text Nick;

    public void SetNickname(string nickname)
    {
        Nick.text = nickname;
    }
    private void Start()
    {
        PlayerViewer = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        if (PlayerViewer != null)
        {
            CanvasRectTransform.LookAt(PlayerViewer);
            CanvasRectTransform.Rotate(0, 180, 0);
        }
        if(PlayerParent.transform.position != transform.position)
        {
            transform.position = new Vector3(PlayerParent.transform.position.x,PlayerParent.transform.position.y+2.5f, PlayerParent.transform.position.z);
        }
    }
}
