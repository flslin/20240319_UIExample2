using System.Collections;
using System.Collections.Generic;
using System.IO; // File, Directory ����� ����
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

class InfoManager : MonoBehaviour
{
    [SerializeField]
    Info player_info;

    public Text ID_Text;
    public Text Gold_Text;
    public Text Point_Text;

    private void Awake()
    {
        player_info = new Info();

        var loadedJson = Resources.Load<TextAsset>("info");
        // ���ҽ� ������ �ִ� info(Text Asset)�� �ε�
        // Text Asset�� �ؽ�Ʈ ������ ������ �ǹ�

        //player_info = new Info();
        //string json = JsonUtility.ToJson(loadedJson);

        player_info = JsonUtility.FromJson<Info>(loadedJson.text);
        // JsonUtility.FromJson<T>(string json);
        // json ���Ϸκ��� �о�� ������ �������� �����͸� �����ϴ� �ڵ�

        Gold_Text.text = player_info.gold.ToString();
        Point_Text.text = player_info.point.ToString();
    }

    /// <summary>
    /// ����Ʈ�� ����ؼ� ���� �����ϴ� �ڵ� (100P -> 10000G)
    /// </summary>
    public void GoldPlus()
    {
        if (player_info.point >= 100)
        {
            player_info.point -= 100;
            player_info.gold += 10000;
            var classtojson = JsonUtility.ToJson(player_info);
            // JsonUtility.ToJson(object obj);
            // ��ü�� ������ json���Ϸ� ������ ���
            // �÷��̾� ������ json���Ͽ� ����
            SaveData(player_info/*, GetPlayer_info()*/);
            LoadData2();
        }
        else
        {
            Debug.Log("�ܾ��� �� ���մϴ�.");
        }
    }

    public void PointPlus()
    {
        player_info.point += 100;
        var classtojson = JsonUtility.ToJson(player_info);
        SaveData(player_info/*, GetPlayer_info()*/);
    }

    private string ResourcePath => Application.dataPath + "/Resources/";

    // [����Ƽ ������ ���]
    private string SavePath => Application.persistentDataPath;
    // ���Ⱑ���� ������ ��ġ, Ư�� �ü������ ���� ����� �� �ֵ��� ����ϴ� ���
    // C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]
    private string DataPath => Application.dataPath;
    // �������� ���� ���(�б� ����)���� ������Ʈ ���� ����(Asset)�� �ǹ�
    private string StreamingPath => Application.streamingAssetsPath;

    //public Info GetPlayer_info()
    //{
    //    return player_info;
    //}

    // Application.dataPath + StreamingAssets = Application.streamingAssetsPath ����


    public void SaveData(Info info/*, Info player_info*/)
    {
        // ������ ���� ��� ���� ����
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }

        var sJson = JsonUtility.ToJson(info); // 1. json ������ ������ string���·� ����
        var FilePath = ResourcePath + "info.json";
        //var FilePath = Path.Combine(DataPath, "info.json"); // ���� ���ڿ��� �� ��η� �����ϴ� ��� (System.IO)

        Gold_Text.text = player_info.gold.ToString();
        Point_Text.text = player_info.point.ToString();

        File.WriteAllText(FilePath, sJson);
    }

    public Info LoadData(string path)
    {
        player_info = null; // Ŭ���� ��ü ���� (���ص� ��� ��)
        if (File.Exists(path)) // ������ ������ �н��� ������ ���
        {
            var json = File.ReadAllText(path); // �ش� ��ηκ��� ������ �о��
            player_info = JsonUtility.FromJson<Info>(json); // �о�� ������ Info�� �� ����
        }
        return player_info; // �ϼ��� ��ü return
    }

    public void LoadData2() // �� �� �ϳ� ���
    {
        var data = File.ReadAllText(ResourcePath + "info.json");
        player_info = JsonUtility.FromJson<Info>(data);
    }
}
