using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SOExample : MonoBehaviour
{
    public Item itemObject;
    public ItemList itemList;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(itemObject.Type);
        //Debug.Log(itemObject.Description);

        itemList = ItemList.Create();
        var itemObj = Item.Create();

        itemList.iList.Add(itemObj);

        for (int i = 0; i < itemList.iList.Count; i++)
        {
            Debug.Log(itemList.iList[i].name);
        }

        var itemList2 = ItemList.Load();
        Debug.Log(AssetDatabase.GetAssetPath(itemList2));
        // AssetDatabase�� ���ο� ������ ������ ��ο� ������ �� ���
        // ��ο��� Ȯ���ڸ� ����ؾ���
        // ������ �̹� path��ο� �����ϴ� ��� �������
        // ��� ��δ� ������Ʈ�� ������ �������� ����

        // ������ : ������ �ҽ� ���Ͽ��� ���� �Ǵ� �ǽð� �ۿ��� ����� �� �ִ� ���·� �����͸� ��ȯ�ؾ� �ϴµ�
        // ����Ƽ�� ��ȯ�� ����, ��ȯ�� ���ϰ� ����� �����͸� ���� ������ ���̽��� ������.
    }

    // Update is called once per frame
    void Update()
    {

    }
}
