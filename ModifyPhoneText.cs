using TMPro;
using UnityEngine;

public class ModifyPhoneText : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text output;

    public void ChangeText()
    {
        string memText =  input.text;
        output.text = memText;
    }
}
