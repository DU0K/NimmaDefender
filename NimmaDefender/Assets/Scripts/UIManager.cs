using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DropDownScroller dropDownScroller;
    [SerializeField] private RectTransform rectTransform;
    float Max = -700f;
    float Min = 0f;
    float snelheid = 4000f;

    private void Awake()
    {
        StartCoroutine(FoldUp());
    }

    public void ClickedButton()
    {
        if (rectTransform.offsetMax.x == Min)
        {
            dropDownScroller.ResetPosition();
            StartCoroutine(FoldUp());
        }
        else if (rectTransform.offsetMax.x == Max)
        {
            StartCoroutine(Extend());
        }
    }

    private IEnumerator FoldUp()
    {
        while (rectTransform.offsetMax.x > Max)
        {
            float nieuweX = Mathf.MoveTowards(rectTransform.offsetMax.x, Max, snelheid * Time.deltaTime);
            rectTransform.offsetMax = new Vector2(nieuweX, rectTransform.offsetMax.y);
            yield return null;
        }
    }

    private IEnumerator Extend()
    {
        while (rectTransform.offsetMax.x < Min)
        {
            float nieuweX = Mathf.MoveTowards(rectTransform.offsetMax.x, Min, snelheid * Time.deltaTime);
            rectTransform.offsetMax = new Vector2(nieuweX, rectTransform.offsetMax.y);
            yield return null;
        }
    }
}
