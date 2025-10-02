using System;
using System.Collections;
using UnityEngine;

public class GarbageUI : MonoBehaviour
{ 
    [SerializeField] private RectTransform targetPos;  // จุดเหนือถังขยะ
    [SerializeField] private float moveDuration = 1f;  // เวลาที่ใช้เคลื่อนที่
    [SerializeField] private float curveHeight = 50f; // ความสูงของเส้นโค้ง

    private RectTransform rectTransform;
    private bool isMoving = false;
    public static event Action OnGarbageCollected;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    private IEnumerator MoveToTarget()
    {
        isMoving = true;

        Vector3 start = rectTransform.localPosition;
        Vector3 end = targetPos.localPosition;

        // จุด Control อยู่กลางทาง + ยกขึ้นบน
        Vector3 control = (start + end) / 2f + Vector3.up * curveHeight;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;

            // Bezier Curve (Quadratic)
            Vector3 m1 = Vector3.Lerp(start, control, t);
            Vector3 m2 = Vector3.Lerp(control, end, t);
            rectTransform.localPosition = Vector3.Lerp(m1, m2, t);

            yield return null;
        }

        rectTransform.localPosition = end; // fix ตำแหน่งตอนสุดท้าย
        isMoving = false;

        OnGarbageCollected?.Invoke();
        Destroy(gameObject, 0.1f);
    }
    public void OnPickUpGarbage()
    {
        if (!isMoving)
            StartCoroutine(MoveToTarget());
    }
}
