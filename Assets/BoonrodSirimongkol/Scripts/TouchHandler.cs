using BoonrodSirimongkol.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 worldPos = mainCamera.ScreenToWorldPoint(touchPos);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                InterectableObject target = hit.collider.GetComponent<InterectableObject>();
                if (target != null)
                {
                    target.OnClicked();
                }
            }
        }
    }
}
