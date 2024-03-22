using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler // ��� �̺�Ʈ ó��
{
    GameObject Icon()
    {
        // ����(������ ���)�� ������(�ڽ� Ʈ������)�� �ִٸ� �������� gameObject�� return
        if (transform.childCount > 0)
            return transform.GetChild(0).gameObject;
        else
            // ���� ��� null ó��
            return null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // ������ ����ִ� ���
        if (Icon() == null)
        {
            // �������� �ڽ����� ��ġ�� �����ϴ� �ڵ�(���� ������ �巡�� Ŭ���� ��� Ȱ��)

        }
    }
}
