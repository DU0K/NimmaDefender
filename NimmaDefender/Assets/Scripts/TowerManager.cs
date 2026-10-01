using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private float towerPointRadius = 1f;

    private InputAction drag;
    private GameObject[] towerPoints;

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;

    private void Awake()
    {
        drag = inputActions.FindActionMap("Main").FindAction("Drag");

        towerPoints = GameObject.FindGameObjectsWithTag("TowerPoint");
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = drag.ReadValue<Vector2>();

        float cameraDistance = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cameraDistance));
        worldMousePos.z = 0f;

        Transform closestTower = null;
        float shortestDistance = float.PositiveInfinity;

        for (int i = 0; i < towerPoints.Length; i++)
        {
            if (towerPoints[i] == null) continue;

            float distance = Vector3.Distance(worldMousePos, towerPoints[i].transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestTower = towerPoints[i].transform;
            }
        }

        if (closestTower != null && shortestDistance < towerPointRadius)
        {
            Transform towerVisual = closestTower.Find(gameObject.tag);
            if (towerVisual != null)
            {
                towerVisual.GetComponent<SpriteRenderer>().enabled = true;
                towerVisual.GetComponent<Sagittarii>().enabled = true;
            }
            else
            {
                Debug.LogWarning($"Geen kind-object gevonden met de tag: {gameObject.tag} op {closestTower.name}");
            }
        }
        rectTransform.anchoredPosition = originalPosition;
    }
}
