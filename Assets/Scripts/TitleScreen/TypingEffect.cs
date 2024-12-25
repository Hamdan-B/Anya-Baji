using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text textComponent;
    public float typingSpeed = 0.05f;

    private string fullText =
        "The sun dipped below the horizon, painting the sky in hues of orange and lavender. Anya sat alone on the weathered wooden bench in her grandmother's backyard, clutching an old scrapbook. She hadn’t opened it in years. The smell of lavender wafted in the breeze, mingling with the soft hum of cicadas.\nTaking a deep breath, she flipped open the first page. A photograph greeted her: a little girl with pigtails and a mischievous smile, holding a kite almost as big as her. Anya felt a tug at her heart, and the memory came flooding back.";
    private Coroutine typingCoroutine;

    void OnEnable()
    {
        textComponent.text = "";

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            textComponent.text = fullText;
        }
    }
}
