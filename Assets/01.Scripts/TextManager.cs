using System.Collections;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI messageText;
    public float displayTime = 2f;
    public float fadeDuration = 1f;

    public void ShowClueMessage()
    {
        StopAllCoroutines();
        StartCoroutine(FadeClueMessage());
    }

    IEnumerator FadeClueMessage()
    {
        messageText.text = "단서를 획득했다!";
        messageText.alpha = 1f;

        yield return new WaitForSeconds(displayTime);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            messageText.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        messageText.alpha = 0f;
    }
}
