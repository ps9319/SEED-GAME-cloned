using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordKeypad : MonoBehaviour
{
    public TextMeshProUGUI inputDisplay; // 비밀번호 표시 텍스트
    public GameObject keypadPanel; // 키패드 전체 패널
    public GameObject statusTextObject;
    public TextMeshProUGUI statusText;
    public float statusDisplayDuration = 2f;
    private string currentInput = "";
    private string correctPassword = "1234";

    public void AppendNumber(string number)
    {
        if (currentInput.Length < 4) // 최대 4자리 제한 (원하면 조절)
        {
            currentInput += number;
            inputDisplay.text = currentInput;
        }
    }

    public void ClearInput()
    {
        currentInput = "";
        inputDisplay.text = "";
    }

    public void CheckPassword()
    {
        statusTextObject.SetActive(true);

        if (currentInput == correctPassword)
        {
            statusText.text = "<color=green>비밀번호 일치!</color>";
            StartCoroutine(HideStatusTextAfterDelay());

            keypadPanel.SetActive(false);
            Time.timeScale = 1f;
            // 여기서 문 열기 등 액션
        }
        else
        {
            statusText.text = "<color=red>틀렸습니다!</color>";
            StartCoroutine(HideStatusTextAfterDelay());

            currentInput = "";
            inputDisplay.text = "";
        }
    }

    public void ClosePanel()
    {
        keypadPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private IEnumerator HideStatusTextAfterDelay()
    {
        yield return new WaitForSecondsRealtime(statusDisplayDuration);
        statusTextObject.SetActive(false);
    }
}
