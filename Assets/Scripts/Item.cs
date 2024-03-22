using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;


// ��ũ���ͺ� ������Ʈ(Scriptable Object)
// ����Ƽ���� �����ϴ� �뷮�� �����͸� ������ �� �ִ� ������ �����̳� �Դϴ�.
// ���� �纻�� �����Ǵ� ���� ���� ����
// ���� ������Ʈ�� ������Ʈ�� ���� �Ұ���, ������Ʈ���� �������� ����

public enum ItemType
{
    WEAPON, ARMOR, POTION
}

[CreateAssetMenu(fileName = "item", menuName = "SO/Item")]

public class Item : ScriptableObject
{
    [SerializeField] private ItemType type;
    [SerializeField] private string description;

    public ItemType Type { get => type; set => type = value; }
    public string Description { get => description; set => description = value; }

    public static Item Create()
    {
        var asset = CreateInstance<Item>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Item/item1.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static Item Load()
    {
        var item = AssetDatabase.LoadAssetAtPath("Assets/Resource/Item/item1.asset", typeof(Item)) as Item;
        return item;
    }
}
