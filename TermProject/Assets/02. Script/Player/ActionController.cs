using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // 대화 걸수 있는 거리

    private bool TalkActivated = false; // 습득가능할시 true

    private RaycastHit hitInfo; // 충돌체 정보 저장

    // NPC 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트
    [SerializeField]
    private Text actionText;

    // Update is called once per frame
    void Update()
    {
        CheckNpc();
    }

    private void CheckNpc()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "NPC")
            {
                npcInfoAppear();
            }
        }
    }

    private void npcInfoAppear()
    {
        TalkActivated = true;
        //actionText.text = hitInfo.transform.GetComponent<>
    }
}
