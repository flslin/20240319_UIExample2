using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static GameObject beginDraggedIcon; // �巡�� �� �� �̵��Ǵ� �����ܿ� ���� static ����

    Vector3 startingPosition; // ������ �ƴ� ��ġ�� �巡�� ���� ��� ���� ��ġ�� ���ư��� ���� ��ġ

    [SerializeField] Transform onDragParentPosition; // ������ �巡�� �� ����� �θ� ���� RectTransform ����

    public Transform startingParentPosition; // �θ� ��ġ �⺻��
    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDraggedIcon = gameObject;

        startingParentPosition = transform.parent;
        startingPosition = transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false; // �����ܿ� ���� RectTransform�� ������.

        transform.SetParent(onDragParentPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beginDraggedIcon = null; // �Ҵ� ����
        GetComponent<CanvasGroup>().blocksRaycasts = true; // �̺�Ʈ ���� Ȱ��ȭ

        if(transform.parent == onDragParentPosition) // ��� �̺�Ʈ �ߴµ� �θ� ������� ���� ��Ȳ, �θ� transform�� ������Ȳ
            // ���� ��ġ�� ����
        {
            transform.position = startingPosition;
            transform.SetParent(startingParentPosition);
        }
    }

}
