using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [Header("Input Settings : ")]
    [SerializeField]
    private LayerMask boxesLayerMask;

    [Header("Mark Sprites : ")]
    [SerializeField]
    private Sprite spriteX;

    [SerializeField]
    private Sprite spriteO;

    [Header("Mark Colors : ")]
    [SerializeField]
    private Color colorX;

    [SerializeField]
    private Color colorO;

    public UnityAction<Mark, Color> OnEndGameEvent;

    public Mark[] marks;

    private Camera cam;

    private Mark currentMark;

    private bool canPlay;

    private LineRenderer lineRenderer;

    private int marksCount = 0;

    public float delayTime = 1;
    float timer;

    private void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        currentMark = Mark.X;

        marks = new Mark[9];

        canPlay = true;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (currentMark == Mark.O)
        {
            if (canPlay && Input.GetMouseButtonUp(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, boxesLayerMask))
                {
                    Box boxComponent = hit.collider.GetComponent<Box>();
                    if (boxComponent != null)
                    {
                        HitBox(boxComponent);
                        timer = delayTime;
                    }
                }
            }
        }
        else if (canPlay && currentMark == Mark.X && timer <= 0)
        {
            Box[] allBoxes = FindObjectsOfType<Box>();

            List<Box> unCheckedBoxesList = new List<Box>();

            foreach (var item in allBoxes)
            {
                if (marks[item.index] == Mark.None)
                {
                    unCheckedBoxesList.Add(item);
                }
            }

            Box[] unCheckedBoxes = unCheckedBoxesList.ToArray();
            int randInd = Random.Range(0, unCheckedBoxes.Length);
            Debug.Log(randInd);
            HitBox(unCheckedBoxes[randInd]);
        }
    }

    private void HitBox(Box box)
    {
        if (!box.isMarked)
        {
            marks[box.index] = currentMark;

            box.SetAsMarked(GetSprite(), currentMark, GetColor());
            marksCount++;

            //check if anybody wins:
            bool won = CheckIfWin();
            if (won)
            {
                if (currentMark == Mark.X)
                {
                    //Bot Win
                    if (OnEndGameEvent != null)
                        OnEndGameEvent.Invoke(currentMark, GetColor());

                    Debug.Log(currentMark.ToString() + " Wins.");
                }
                else
                {
                    //Anya Win
                    FindObjectOfType<TicTacToeEvent>()
                        .AfterWin();
                }
                canPlay = false;
                return;
            }

            if (!won && marksCount == 9)
            {
                if (OnEndGameEvent != null)
                    OnEndGameEvent.Invoke(Mark.None, Color.white);

                Debug.Log("Nobody Wins.");

                canPlay = false;
                return;
            }

            SwitchPlayer();
        }
    }

    private bool CheckIfWin()
    {
        return AreBoxesMatched(0, 1, 2)
            || AreBoxesMatched(3, 4, 5)
            || AreBoxesMatched(6, 7, 8)
            || AreBoxesMatched(0, 3, 6)
            || AreBoxesMatched(1, 4, 7)
            || AreBoxesMatched(2, 5, 8)
            || AreBoxesMatched(0, 4, 8)
            || AreBoxesMatched(2, 4, 6);
    }

    private bool AreBoxesMatched(int i, int j, int k)
    {
        Mark m = currentMark;
        bool matched = (marks[i] == m && marks[j] == m && marks[k] == m);

        if (matched)
            DrawLine(i, k);

        return matched;
    }

    private void DrawLine(int i, int k)
    {
        lineRenderer.SetPosition(0, transform.GetChild(i).position);
        lineRenderer.SetPosition(1, transform.GetChild(k).position);
        Color color = GetColor();
        color.a = .3f;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        lineRenderer.enabled = true;
    }

    private void SwitchPlayer()
    {
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Color GetColor()
    {
        return (currentMark == Mark.X) ? colorX : colorO;
    }

    private Sprite GetSprite()
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }
}
