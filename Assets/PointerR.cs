using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Oculus;

public class PointerR : MonoBehaviour
{
    private LineRenderer laser; // 레이저

    public float raycastDistance = 50f; // 레이저 포인터 감지 거리

    private void Start()
    {
        laser = gameObject.AddComponent<LineRenderer>();
        laser.material = new Material(Shader.Find("Standard"));
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
    }

    private void Update()
    {
        HandleLaser();
    }

    private void HandleLaser()
    {
        laser.SetPosition(0, transform.position); // 레이저 시작점은 컨트롤러의 위치

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            laser.SetPosition(1, hit.point); // 레이저 끝점은 충돌 지점

            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                // 충돌한 객체가 Interactable 태그를 가지고 있는 경우
                // 객체를 강조 표시하거나 상호작용 가능한 상태로 변화시킬 수 있습니다.
                // 예: 강조 효과, 컬러 변경 등
            }
        }
        else
        {
            laser.SetPosition(1, transform.position + transform.forward * raycastDistance);
        }
    }
}
