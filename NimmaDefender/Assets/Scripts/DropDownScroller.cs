using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DropDownScroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private float scrollSpeed = 200f;
    [SerializeField] private float maxScroll = 564f;
    private InputAction scroll;
    private bool isPointerOver = false;
    private RectTransform rectTransform;


    private void OnEnable()
    {
        inputActions.FindActionMap("Main").Enable();
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Main").Disable();
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        scroll = inputActions.FindActionMap("Main").FindAction("Scroll");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
    }

    private void Update()
    {
        if (isPointerOver && scroll != null)
        {
            float scrollValue = scroll.ReadValue<float>();

            if (scrollValue != 0)
            {
                float currentLeft = rectTransform.offsetMin.x;
                float newLeft = currentLeft - (scrollValue * scrollSpeed * Time.deltaTime);
                newLeft = Mathf.Clamp(newLeft, 0f, maxScroll);
                float deltaX = newLeft - currentLeft;
                rectTransform.offsetMin = new Vector2(newLeft, rectTransform.offsetMin.y);
                rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x + deltaX, rectTransform.offsetMax.y);
            }
        }
    }

    public void ResetPosition()
    {
        float oudeLeft = rectTransform.offsetMin.x;
        rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x - oudeLeft, rectTransform.offsetMax.y);
    }

}