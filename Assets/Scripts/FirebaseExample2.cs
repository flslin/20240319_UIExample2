using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student
{
    public string sName;
    public string email;

    public Student(string name, string email)
    {
        sName = name;
        this.email = email;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["s_name"] = sName;
        result["email"] = email;
        return result;
    }

    // ��� ����
}

// SetValueAsync() �� ���� ������ ������ �����͸� �����ϰ� �ش� ����� ���� �����ͷ� �����ϴ� �۾�
// string, long, double, bool Dictionary<string, Object>, List<Object>

public class FirebaseExample2 : MonoBehaviour
{
    DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        StudentRegister("20211367", "NAME", "example@gmail.com");
        StudentUpdate("20211367", "MANE");
    }
    
    /// <summary>
    /// ������ ���̽��� �л� Ŭ������ ���� ������ �߰��ϴ� �ڵ�
    /// </summary>
    /// <param name="_sID">�л� ���� �ĺ� �ڵ� (�й�)</param>
    /// <param name="_sName">�л� �̸�</param>
    /// <param name="_email">�л� �̸��� �ּ�</param>
    private void StudentRegister(string _sID, string _sName, string _email)
    {
        // 1. Ŭ������ ���� ����
        Student student = new Student(_sName, _email);

        // 2. �ش� Ŭ������ Json�� ���·� �ٲ���(string)
        string Student_json =JsonUtility.ToJson(student);

        // 3. ���۷��� �� ����
        reference.Child("students").Child(_sID).SetRawJsonValueAsync(Student_json);

        Debug.Log($"{_sID} / {_sName} / {_email}");
    }

    private void StudentUpdate(string _sID, string _sName)
    {
        reference.Child("students").Child(_sID).Child("student_name").SetValueAsync(_sName);
        
        Debug.Log($"�̸��� ����Ǿ����ϴ�.");
    }

    // ��� ����
    void Sample()
    {
        Student s = new Student("a", "a@naver.com");
        var sjson = JsonUtility.ToJson(s);
        Dictionary<string, object> update = s.ToDictionary();

        var key = reference.Child("students").Push().Key;

        reference.Child("students").Child("20211367").SetRawJsonValueAsync(sjson);
    }
}
