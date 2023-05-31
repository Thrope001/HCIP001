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

        // 왼손 컨트롤러 포인터로 블럭 이동
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            if (!isLeftTriggerPressed)
            {
                isLeftTriggerPressed = true;

                // 왼손 컨트롤러 포인터를 통해 블럭 이동
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

        // 오른손 컨트롤러 포인터로 블럭 생성
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (!isRightTriggerPressed)
            {
                isRightTriggerPressed = true;

                // 오른손 컨트롤러 포인터 위치에 블럭 생성
                Instantiate(blockPrefab, rightHandAnchor.forward * 5f, Quaternion.identity);
            }
        }
        else
        {
            isRightTriggerPressed = false;
        }

        // 현재 선택된 블럭을 왼손 컨트롤러 포인터 위치로 이동
        if (currentBlock != null)
        {
            currentBlock.transform.position = leftHandAnchor.position + leftHandAnchor.forward * 5f;
        }
    }
}
