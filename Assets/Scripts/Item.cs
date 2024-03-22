using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;


// 스크립터블 오브젝트(Scriptable Object)
// 유니티에서 제공하는 대량의 데이터를 저장할 수 있는 데이터 컨테이너 입니다.
// 값의 사본이 생성되는 것을 방지 가능
// 게임 오브젝트에 컴포넌트로 부착 불가능, 프로젝트에서 에셋으로 저장

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
