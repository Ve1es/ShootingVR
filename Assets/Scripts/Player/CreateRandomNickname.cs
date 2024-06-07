using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateRandomNickname : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        CheckAndFillInputField();
    }

    private void CheckAndFillInputField()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = GenerateRandomName();
        }
    }

    private string GenerateRandomName()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        char[] nameArray = new char[5];
        System.Random random = new System.Random();

        for (int i = 0; i < nameArray.Length; i++)
        {
            nameArray[i] = chars[random.Next(chars.Length)];
        }

        return new string(nameArray);
    }
}
