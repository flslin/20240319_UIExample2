using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseExample : MonoBehaviour
{
    DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        // ������ ���̽����� �����͸� �д� ���
        // DatabaseReference�� ���� �ν��Ͻ��� �䱸��

        //WriteUserDatatoDatabase("0", "Mcdonald's");
        //WriteUserDatatoDatabase("1", "Lotte");
        //WriteUserDatatoDatabase("2", "BurgerKing");
        //WriteUserDatatoDatabase("3", "FrankBurger");
        //WriteUserDatatoDatabase("4", "Mom'sTouch");

        ReadUserDatatoDatabase();
    }

    void WriteUserDatatoDatabase(string _userID, string _userName)
    {
        // ������ ���̽��ʿ� ���� �������ִ� ���
        reference.Child("users").Child(_userID).Child("userName").SetValueAsync(_userName);

        // ���� �� ������ ���̽��� ������ ���� ����
        // database �̸�
        // users
        // -> _userID
        //      -> userName : _userName
        Debug.Log($"users / {_userID} / userName / {_userName}�� �����ͺ��̽��� ��ϵ�");
    }

    void ReadUserDatatoDatabase()
    {
        // �����ͺ��̽��� ����Ǿ� �ִ� ���� �о�� ���
        // 1. ���̾�̽��κ��� �ν��Ͻ��� ���� ���� ����. ���ν����忡�� ��� ����

        // �ڵ带 ��Ȯ�� �����ϱ� ���� �˾Ƶ� ����
        // 1. ������ (Thread) : ���μ��� ������ ����Ǵ� �帧�� ����. �� �̻��� ��� ��Ƽ ������� ��
        // 2. �½�ũ (Task) : �����带 ���� �񵿱� �۾��� ���Ǵ� ������
        // 3. ���� �޼ҵ� (annonymous method) : �ַ� delegate���� ��� �ѹ� ���� �ٽ� �� �� ���� ��ɿ� ���� ǥ������ ���
        // 4. ���ٽ� (Lamda Expression) : �ڵ忡 ���� �ܼ�ȭ, �ַ� ���� �Լ� ǥ���� �� ���
        //                               => �����ڸ� ���� �ش� ���� �������� ó��
        // �����Ϸ��� �ڵ� ���� �� ���� ���ڿ��� ���� �ż� ���� ���� ����
        // ������ �۾��� ���� ǥ��θ� ����ϴ� ���� �ٶ���
        FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted) // �½�ũ�� ������ ���
            {
                // ������ ���� �ڵ鸵 �۾�
            }
            else if (task.IsCompleted) // �½�ũ�� �Ϸ�� ��� 
            {
                // ���̾�̽����� �����ϴ� ������ �ۼ��ſ� ��ü(Ŭ����)
                DataSnapshot dataSnapshot = task.Result; // �½�ũ ������� �޾ƿ�

                for (int i = 0; i < dataSnapshot.ChildrenCount; i++)
                {
                    Debug.Log(dataSnapshot.Child(i.ToString()).Child("userName").Value);
                }
            }
        });
        
    }
}
