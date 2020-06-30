using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // 대화 걸수 있는 거리

    private bool TalkActivated = false; // 습득가능할시 true

    private RaycastHit hitInfo; // 충돌체 정보 저장
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
