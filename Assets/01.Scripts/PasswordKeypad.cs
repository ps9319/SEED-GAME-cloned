using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordKeypad : MonoBehaviour
{
    public TextMeshProUGUI inputDisplay;           // 비밀번호 표시 텍스트
    public GameObject keypadPanel;      // 키패드 전체 패널
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
        if (currentInput == correctPassword)
        {
            Debug.Log("비밀번호 일치!");
            keypadPanel.SetActive(false);
            Time.timeScale = 1f;
            // 여기서 문을 연다거나 다음 액션 실행!
        }
        else
        {
            Debug.Log("비밀번호 틀림!");
            inputDisplay.text = "틀렸습니다!";
            currentInput = "";
        }
    }

    public void ClosePanel()
    {
        keypadPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
