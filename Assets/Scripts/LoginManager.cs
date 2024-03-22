using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerInfo
{
    public string id;
    public string pw;

    public PlayerInfo(string _id, string _pw)
    {
        id = _id;
        pw = _pw;
    }
}

public class LoginManager : MonoBehaviour
{
    [Header("화면")]
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    [Header("로그인 UI")]
    public InputField loginIDText;
    public InputField loginPWText;
    public Text loginCheckText;
    [Header("회원가입 UI")]
    public InputField signinIDText;
    public InputField signinPWText;
    public InputField signinPWCheckText;
    public Text signinCheckText;

    DatabaseReference reference;

    public void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }



    public void Awake()
    {
        //firstPanel = GetComponent<GameObject>();
        //secondPanel = GetComponent<GameObject>();
        //thirdPanel = GetComponent<GameObject>();
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
    }

    #region 화면전환
    public void OnClickButton()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
    }

    public void OnClickGoSigninButton()
    {
        secondPanel.SetActive(false);
        thirdPanel.SetActive(true);
    }

    public void OnClick2PUndo()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
    }

    public void OnClick3PUndo()
    {
        secondPanel.SetActive(true);
        thirdPanel.SetActive(false);
    }
    #endregion

    string id;
    string pw;

    public void OnClickLoginButton()
    {
        if (loginIDText.text != string.Empty && loginPWText.text != string.Empty)
        {
            FirebaseDatabase.DefaultInstance.GetReference("playerInfo").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("오류애오");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot.ChildrenCount > 0)
                    {
                        //for (int i = 0; i < snapshot.ChildrenCount; i++)
                        //{
                            id = snapshot.Child(loginIDText.text.ToString()).Child("id").Value.ToString();
                            pw = snapshot.Child(loginIDText.text.ToString()).Child("pw").Value.ToString();
                            
                            if (id == loginIDText.text && pw == loginPWText.text)
                            {
                                loginCheckText.text = "로그인 성공!";
                            }
                            else
                            {
                                loginCheckText.text = "아이디, 비밀번호를 확인해 주세요!";
                                
                            }
                        //}
                    }
                    else
                    {
                        loginCheckText.text = "회원 데이터가 없습니다.";
                    }
                }
            });
        }

        //else if (loginIDText.text == string.Empty)
        //{
        //    loginCheckText.text = "아이디를 입력해 주세요!";
        //}

        //else if (loginPWText.text == string.Empty)
        //{
        //    loginCheckText.text = "비밀번호를 입력해 주세요";
        //}

        else
        {
            loginCheckText.text = "아이디 또는 비밀번호를 입력해 주세요";
        }
    }

    int index = 0;

    public void OnClickSigninButton()
    {
        string _id = signinIDText.text;
        string _pw = signinPWText.text;
        PlayerInfo playerInfo = new PlayerInfo(_id, _pw);
        string player_json = JsonUtility.ToJson(playerInfo);

        if (signinIDText.text != string.Empty && signinPWText.text != string.Empty && signinPWCheckText.text != string.Empty)
        {
            //reference.Child("playerInfo").Child($"signInfo{index}").SetRawJsonValueAsync(player_json);
            reference.Child("playerInfo").Child(_id).SetRawJsonValueAsync(player_json);
            index++;

            signinCheckText.text = "이게 되네";
        }

        else if ((signinIDText.text != string.Empty && signinPWText.text != string.Empty && signinPWCheckText.text == string.Empty) || (signinPWText.text != signinPWCheckText.text))
        {
            signinCheckText.text = "입력한 비밀번호가 같지 않습니다.";
        }

        else
        {
            signinCheckText.text = "이게 안되네";
        }
    }

}
