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
    [Header("ȭ��")]
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    [Header("�α��� UI")]
    public InputField loginIDText;
    public InputField loginPWText;
    public Text loginCheckText;
    [Header("ȸ������ UI")]
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

    #region ȭ����ȯ
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
                    Debug.Log("�����ֿ�");
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
                                loginCheckText.text = "�α��� ����!";
                            }
                            else
                            {
                                loginCheckText.text = "���̵�, ��й�ȣ�� Ȯ���� �ּ���!";
                                
                            }
                        //}
                    }
                    else
                    {
                        loginCheckText.text = "ȸ�� �����Ͱ� �����ϴ�.";
                    }
                }
            });
        }

        //else if (loginIDText.text == string.Empty)
        //{
        //    loginCheckText.text = "���̵� �Է��� �ּ���!";
        //}

        //else if (loginPWText.text == string.Empty)
        //{
        //    loginCheckText.text = "��й�ȣ�� �Է��� �ּ���";
        //}

        else
        {
            loginCheckText.text = "���̵� �Ǵ� ��й�ȣ�� �Է��� �ּ���";
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

            signinCheckText.text = "�̰� �ǳ�";
        }

        else if ((signinIDText.text != string.Empty && signinPWText.text != string.Empty && signinPWCheckText.text == string.Empty) || (signinPWText.text != signinPWCheckText.text))
        {
            signinCheckText.text = "�Է��� ��й�ȣ�� ���� �ʽ��ϴ�.";
        }

        else
        {
            signinCheckText.text = "�̰� �ȵǳ�";
        }
    }

}
