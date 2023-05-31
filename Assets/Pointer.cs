using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Oculus;

public class Pointer : MonoBehaviour
{
    private LineRenderer laser; // ������
    private GameObject grabbedObject; // ���� ��ü
    private Vector3 grabOffset; // ���� ��ü�� ��Ʈ�ѷ� ������ ������
    //public Vector3 grabOffsetDistance = new Vector3(0f, 0f, -0.1f);
    private bool isGrabbing; // ��ü�� ��� �ִ��� ����

    public float raycastDistance = 50f; // ������ ������ ���� �Ÿ�

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
        laser.SetPosition(0, transform.position); // ������ �������� ��Ʈ�ѷ��� ��ġ

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            laser.SetPosition(1, hit.point); // ������ ������ �浹 ����

            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                // �浹�� ��ü�� Interactable �±׸� ������ �ִ� ���
                // ��ü�� ���� ǥ���ϰų� ��ȣ�ۿ� ������ ���·� ��ȭ��ų �� �ֽ��ϴ�.
                // ��: ���� ȿ��, �÷� ���� ��
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
                        // ��ü�� ���� �� �ִ� ������ �߰��ϼ���.
                        // ��: �浹�� ��ü�� Interactable �±׸� ������ �ִ� ���

                        // ���� ��ü ����
                        grabbedObject = hit.collider.gameObject;

                        // ��ü�� ��Ʈ�ѷ� ������ ������ ���
                        grabOffset = grabbedObject.transform.position - transform.position;

                        // ��ü�� ��� �ִ� ���·� ����
                        isGrabbing = true;
                    }
                }
            }
            else
            {
                // ��ü�� ���� �� �ִ� ������ �߰��ϼ���.
                // ��: Ư�� ��ư�� ������ ��, �浹 ������ Ư�� ������ ���� �� ��

                // ���� ��ü ����
                grabbedObject = null;

                // ��ü�� ���� �ִ� ���·� ����
                isGrabbing = false;
            }
        }

        // ���� ��ü�� �ִ� ���, ��Ʈ�ѷ� ��ġ�� ���� ��ü�� �̵���Ŵ
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, transform.position + grabOffset, Time.deltaTime * 10f);
        }
    }
}
