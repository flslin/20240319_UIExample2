using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField] private int _id; // 아이템 식별번호
    [SerializeField] private string _name; // 아이템 이름
    [SerializeField] private string _description; // 아이템 설명
    [SerializeField] private Sprite _icon; // 아이템 아이콘
    [SerializeField] private GameObject _prefab; // 아이템 드랍 시 형태

    public abstract Items Create();

}
