using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [Header("UI References :")]
    [SerializeField]
    private GameObject uiCanvas;

    [SerializeField]
    private TMP_Text uiWinnerText;

    [SerializeField]
    private Button uiRestartButton;

    [Header("Board Reference :")]
    [SerializeField]
    private Board board;

    private void Start()
    {
        uiRestartButton.onClick.AddListener(() => SceneManager.LoadScene("Level1"));
        board.OnEndGameEvent += OnEndGameEvent;

        uiCanvas.SetActive(false);
    }

    private void OnEndGameEvent(Mark mark, Color color)
    {
        uiWinnerText.text = (mark == Mark.None) ? "Nobody Wins" : mark.ToString() + " Wins.";
        uiWinnerText.color = color;

        uiCanvas.SetActive(true);
    }

    private void OnDestroy()
    {
        uiRestartButton.onClick.RemoveAllListeners();
        board.OnEndGameEvent -= OnEndGameEvent;
    }
}
