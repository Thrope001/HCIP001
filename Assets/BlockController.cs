using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform leftHandAnchor;
    public Transform rightHandAnchor;

    private bool isLeftTriggerPressed = false;
    private bool isRightTriggerPressed = false;
    private GameObject currentBlock;

    private void Update()
    {
        OVRInput.Update();

        // �޼� ��Ʈ�ѷ� �����ͷ� �� �̵�
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            if (!isLeftTriggerPressed)
            {
                isLeftTriggerPressed = true;

                // �޼� ��Ʈ�ѷ� �����͸� ���� �� �̵�
                RaycastHit hit;
                if (Physics.Raycast(leftHandAnchor.position, leftHandAnchor.forward, out hit))
                {
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        currentBlock = hit.collider.gameObject;
                    }
                }
            }
        }
        else
        {
            isLeftTriggerPressed = false;
            currentBlock = null;
        }

        // ������ ��Ʈ�ѷ� �����ͷ� �� ����
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (!isRightTriggerPressed)
            {
                isRightTriggerPressed = true;

                // ������ ��Ʈ�ѷ� ������ ��ġ�� �� ����
                Instantiate(blockPrefab, rightHandAnchor.forward * 5f, Quaternion.identity);
            }
        }
        else
        {
            isRightTriggerPressed = false;
        }

        // ���� ���õ� ���� �޼� ��Ʈ�ѷ� ������ ��ġ�� �̵�
        if (currentBlock != null)
        {
            currentBlock.transform.position = leftHandAnchor.position + leftHandAnchor.forward * 5f;
        }
    }
}
