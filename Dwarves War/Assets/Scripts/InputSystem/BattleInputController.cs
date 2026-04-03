using UnityEngine;
using UnityEngine.InputSystem;

public class BattleInputController : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // --- TOUCH ---
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 pos = Touchscreen.current.primaryTouch.position.ReadValue();
            HandleTap(pos);
        }

        // --- MOUSE (для редактора) ---
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 pos = Mouse.current.position.ReadValue();
            HandleTap(pos);
        }
    }

    private void HandleTap(Vector2 screenPos)
    {
        Vector3 screen = new Vector3(
            screenPos.x,
            screenPos.y,
            Mathf.Abs(cam.transform.position.z)
        );

        Vector3 world = cam.ScreenToWorldPoint(screen);

        RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);

        if (hit.collider == null)
        {
            Debug.Log("Nothing hit");
            return;
        }

        Debug.Log("Hit: " + hit.collider.name);

        ITappable tappable = hit.collider.GetComponentInChildren<ITappable>();

        if (tappable != null)
        {
            Debug.Log("Tappable found");
            tappable.OnTap();
        }
    }
}