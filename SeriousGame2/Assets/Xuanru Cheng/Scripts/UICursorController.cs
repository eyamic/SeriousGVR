using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UICursorController : MonoBehaviour
{
    public RectTransform cursorRectTransform;
    public float cursorSpeed = 100.0f;

    private PlayerControls controls;
    private Vector2 navigationInput;


    private void Awake() {
        controls = new PlayerControls();
        controls.UI.Navigate.performed += ctx => navigationInput = ctx.ReadValue<Vector2>();
        controls.UI.Navigate.canceled += ctx => navigationInput = Vector2.zero;
    }

    private void Update() {
        // D-pad 输入控制光标移动
        Vector2 movement = navigationInput * cursorSpeed * Time.deltaTime;
        Vector2 newPos = cursorRectTransform.anchoredPosition + movement;
        newPos.x = Mathf.Clamp(newPos.x, -Screen.width / 2, Screen.width / 2);
        newPos.y = Mathf.Clamp(newPos.y, -Screen.height / 2, Screen.height / 2);

        cursorRectTransform.anchoredPosition = newPos;
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}