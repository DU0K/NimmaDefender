using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTools : MonoBehaviour
{
    private float timeScale = 1f;
    public float TimeScale => timeScale;
    private float timeScaleInput;

    [SerializeField] private InputActionAsset inputActions;
    private InputAction timeController;
    private InputAction reset;

    private void OnEnable()
    {
        inputActions.FindActionMap("Debug").Enable();
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Debug").Disable();
    }

    private void Awake()
    {
        timeController = inputActions.FindActionMap("Debug").FindAction("TimeController");
        reset = inputActions.FindActionMap("Debug").FindAction("Reset");
    }
    private void Update()
    {
        if (timeController.WasPressedThisFrame())
        {
            timeScaleInput = timeController.ReadValue<float>();
            if (timeScaleInput > 0)
            {
                timeScale += 1f;
                Debug.Log("Time scale increased to: " + timeScale);
            }
            else if (timeScaleInput < 0)
            {
                timeScale -= 1f;
                Debug.Log("Time scale decreased to: " + timeScale);
            }
        }

        if (reset.WasPressedThisFrame())
        {
            timeScale = 1f;
            Debug.Log("Time scale reset to 1");
        }
    }
}
