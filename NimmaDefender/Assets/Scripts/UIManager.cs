using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image dropDown;

    public void ClickedButton()
    {
        Debug.Log("clicked");
        StartCoroutine(ExtendDropDown());
    }

    private IEnumerator ExtendDropDown()
    {
        Debug.Log(dropDown.rectTransform.offsetMax.x);

        float Max = -700f;
        float Min = 0f;
        float snelheid = 4000f;

        if (dropDown.rectTransform.offsetMax.x == Min)
        {
            while (dropDown.rectTransform.offsetMax.x > Max)
            {
                float nieuweX = Mathf.MoveTowards(dropDown.rectTransform.offsetMax.x, Max, snelheid * Time.deltaTime);
                dropDown.rectTransform.offsetMax = new Vector2(nieuweX, dropDown.rectTransform.offsetMax.y);
                yield return null;
            }
        }
        else if (dropDown.rectTransform.offsetMax.x == Max)
        {
            while (dropDown.rectTransform.offsetMax.x < Min)
            {
                float nieuweX = Mathf.MoveTowards(dropDown.rectTransform.offsetMax.x, Min, snelheid * Time.deltaTime);
                dropDown.rectTransform.offsetMax = new Vector2(nieuweX, dropDown.rectTransform.offsetMax.y);
                yield return null;
            }
        }
    }
}
