using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;

public class ShowKeyboard : MonoBehaviour
{
    public TMP_InputField inputField;
    public NonNativeKeyboard NNK;
    public ShowKeyboard anotherInputField;
    public bool hear = false;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }
    public void OpenKeyboard()
    {
        hear = true;
        anotherInputField.hear = false;
        NNK.InputField.text = inputField.text;
    }
    public void Update()
    {
        if (hear)
        {
            inputField.text = NNK.InputField.text;
        }
    }
}
