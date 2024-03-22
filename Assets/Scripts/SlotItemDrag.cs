using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static GameObject beginDraggedIcon; // 드래그 될 때 이동되는 아이콘에 대한 static 변수

    Vector3 startingPosition; // 슬롯이 아닌 위치에 드래그 했을 경우 원래 위치로 돌아가기 위한 위치

    [SerializeField] Transform onDragParentPosition; // 아이콘 드래그 중 변경될 부모에 대한 RectTransform 변수

    public Transform startingParentPosition; // 부모 위치 기본값
    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDraggedIcon = gameObject;

        startingParentPosition = transform.parent;
        startingPosition = transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false; // 아이콘에 대한 RectTransform을 무시함.

        transform.SetParent(onDragParentPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beginDraggedIcon = null; // 할당 해제
        GetComponent<CanvasGroup>().blocksRaycasts = true; // 이벤트 감지 활성화

        if(transform.parent == onDragParentPosition) // 드랍 이벤트 했는데 부모가 변경되지 않은 상황, 부모 transform이 같은상황
            // 원래 위치로 유지
        {
            transform.position = startingPosition;
            transform.SetParent(startingParentPosition);
        }
    }

}
