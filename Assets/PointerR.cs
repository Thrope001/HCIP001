using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Oculus;

public class PointerR : MonoBehaviour
{
    private LineRenderer laser; // ������

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
}
