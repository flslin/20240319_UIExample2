using System.Collections;
using System.Collections.Generic;
using System.IO; // File, Directory 사용을 위함
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
        // 리소스 폴더에 있는 info(Text Asset)을 로드
        // Text Asset은 텍스트 형태의 에셋을 의미

        //player_info = new Info();
        //string json = JsonUtility.ToJson(loadedJson);

        player_info = JsonUtility.FromJson<Info>(loadedJson.text);
        // JsonUtility.FromJson<T>(string json);
        // json 파일로부터 읽어온 파일을 기준으로 데이터를 적용하는 코드

        Gold_Text.text = player_info.gold.ToString();
        Point_Text.text = player_info.point.ToString();
    }

    /// <summary>
    /// 포인트를 사용해서 골드로 변경하는 코드 (100P -> 10000G)
    /// </summary>
    public void GoldPlus()
    {
        if (player_info.point >= 100)
        {
            player_info.point -= 100;
            player_info.gold += 10000;
            var classtojson = JsonUtility.ToJson(player_info);
            // JsonUtility.ToJson(object obj);
            // 객체의 정보를 json파일로 보내는 기능
            // 플레이어 정보를 json파일에 저장
            SaveData(player_info/*, GetPlayer_info()*/);
            LoadData2();
        }
        else
        {
            Debug.Log("잔액이 부 족합니다.");
        }
    }

    public void PointPlus()
    {
        player_info.point += 100;
        var classtojson = JsonUtility.ToJson(player_info);
        SaveData(player_info/*, GetPlayer_info()*/);
    }

    private string ResourcePath => Application.dataPath + "/Resources/";

    // [유니티 데이터 경로]
    private string SavePath => Application.persistentDataPath;
    // 쓰기가능한 폴더의 위치, 특정 운영체제에서 앱이 사용할 수 있도록 허용하는 경로
    // C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]
    private string DataPath => Application.dataPath;
    // 데이터의 저장 경로(읽기 전용)으로 프로젝트 폴더 내부(Asset)을 의미
    private string StreamingPath => Application.streamingAssetsPath;

    //public Info GetPlayer_info()
    //{
    //    return player_info;
    //}

    // Application.dataPath + StreamingAssets = Application.streamingAssetsPath 공식


    public void SaveData(Info info/*, Info player_info*/)
    {
        // 폴더가 없을 경우 폴더 생성
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }

        var sJson = JsonUtility.ToJson(info); // 1. json 파일의 정보를 string형태로 저장
        var FilePath = ResourcePath + "info.json";
        //var FilePath = Path.Combine(DataPath, "info.json"); // 여러 문자열을 한 경로로 결합하는 기능 (System.IO)

        Gold_Text.text = player_info.gold.ToString();
        Point_Text.text = player_info.point.ToString();

        File.WriteAllText(FilePath, sJson);
    }

    public Info LoadData(string path)
    {
        player_info = null; // 클래스 객체 비우기 (안해도 상관 무)
        if (File.Exists(path)) // 파일이 전달한 패스에 존재할 경우
        {
            var json = File.ReadAllText(path); // 해당 경로로부터 파일을 읽어옴
            player_info = JsonUtility.FromJson<Info>(json); // 읽어온 내용을 Info에 값 전달
        }
        return player_info; // 완성된 객체 return
    }

    public void LoadData2() // 둘 중 하나 사용
    {
        var data = File.ReadAllText(ResourcePath + "info.json");
        player_info = JsonUtility.FromJson<Info>(data);
    }
}
