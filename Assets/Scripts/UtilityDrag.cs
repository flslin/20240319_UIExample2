using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UtilityDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 defaultPostion;

    /// <summary>
    /// 물체를 드래그하기 시작 했을 때 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultPostion = transform.position;
    }

    /// <summary>
    /// 드래그가 끝났을 때 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData) // 드래그가 끝났을 때
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 
        //transform.position = defaultPostion; // 처음 위치로 이동
        transform.position = eventData.position; // 해당 위치로 이동
    }

    /// <summary>
    /// 드래그를 진행하는 동안 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData) // 드래그 중일 때
    {
        Vector2 currentPos = eventData.position; // 드래그를 시작한 위치를 잡아서
        transform.position = currentPos; // 대상의 위치를 그곳으로 설정
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
