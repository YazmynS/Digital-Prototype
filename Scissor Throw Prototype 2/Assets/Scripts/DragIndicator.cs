using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DragIndicator : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 centerPos;
    private bool isDragging = false;

    [SerializeField] AnimationCurve ac;
    [SerializeField] Gradient gradient =new Gradient();

    private void Start()
    {
        lr = GetComponent<LineRenderer>() ?? gameObject.AddComponent<LineRenderer>();
        
        lr.positionCount = 2;
        lr.enabled = false;
        lr.colorGradient = gradient;
        lr.widthCurve = ac;
        lr.numCapVertices = 10;
    }

    private void OnMouseDown() {
        lr.enabled = true;
        isDragging = true;
    }

    private void OnMouseDrag() {
        if (isDragging) {
            centerPos = transform.position;
            lr.SetPosition(0, centerPos);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            lr.SetPosition(1, mousePos);
        }
    }

    private void OnMouseUp() {
        isDragging = false;
        lr.enabled = false;
    }
}
