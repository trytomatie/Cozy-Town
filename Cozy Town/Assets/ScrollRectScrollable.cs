using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScrollRectScrollable : ScrollRect, IPointerEnterHandler, IPointerExitHandler
{
    private static string mouseScrollWheelAxis = "Mouse ScrollWheel";
    private bool swallowMouseWheelScrolls = true;
    public static bool mouseOverScrollBar = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverScrollBar = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverScrollBar = false;
    }

    private void Update()
    {
        if(mouseOverScrollBar)
        {
            // OnScroll(new PointerEventData(EventSystem.current) { scrollDelta = new Vector2(0, GameManager.PlayerInputMap.Camera.Zoom.ReadValue<Vector2>().y * 0.05f) });
        }
    }



}
