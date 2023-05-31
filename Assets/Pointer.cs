using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Oculus;

public class Pointer : MonoBehaviour
{
    private LineRenderer laser; // 레이저
    private GameObject grabbedObject; // 잡은 객체
    private Vector3 grabOffset; // 잡은 객체와 컨트롤러 사이의 오프셋
    //public Vector3 grabOffsetDistance = new Vector3(0f, 0f, -0.1f);
    private bool isGrabbing; // 객체를 잡고 있는지 여부

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
        HandleGrab();
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

    private void HandleGrab()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (!isGrabbing)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
                {
                    if (hit.collider.gameObject.CompareTag("Interactable"))
                    {
                        // 객체를 잡을 수 있는 조건을 추가하세요.
                        // 예: 충돌한 객체가 Interactable 태그를 가지고 있는 경우

                        // 잡은 객체 설정
                        grabbedObject = hit.collider.gameObject;

                        // 객체와 컨트롤러 사이의 오프셋 계산
                        grabOffset = grabbedObject.transform.position - transform.position;

                        // 객체를 잡고 있는 상태로 설정
                        isGrabbing = true;
                    }
                }
            }
            else
            {
                // 객체를 놓을 수 있는 조건을 추가하세요.
                // 예: 특정 버튼을 눌렀을 때, 충돌 지점이 특정 영역에 들어갔을 때 등

                // 잡은 객체 해제
                grabbedObject = null;

                // 객체를 놓고 있는 상태로 설정
                isGrabbing = false;
            }
        }

        // 잡은 객체가 있는 경우, 컨트롤러 위치에 따라 객체를 이동시킴
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, transform.position + grabOffset, Time.deltaTime * 10f);
        }
    }
}
