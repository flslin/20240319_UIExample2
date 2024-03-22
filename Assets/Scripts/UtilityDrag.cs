using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UtilityDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 defaultPostion;

    /// <summary>
    /// ��ü�� �巡���ϱ� ���� ���� �� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultPostion = transform.position;
    }

    /// <summary>
    /// �巡�װ� ������ �� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData) // �巡�װ� ������ ��
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 
        //transform.position = defaultPostion; // ó�� ��ġ�� �̵�
        transform.position = eventData.position; // �ش� ��ġ�� �̵�
    }

    /// <summary>
    /// �巡�׸� �����ϴ� ���� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData) // �巡�� ���� ��
    {
        Vector2 currentPos = eventData.position; // �巡�׸� ������ ��ġ�� ��Ƽ�
        transform.position = currentPos; // ����� ��ġ�� �װ����� ����
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
