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
        // 데이터 베이스에서 데이터를 읽는 방법
        // DatabaseReference에 대한 인스턴스가 요구됨

        //WriteUserDatatoDatabase("0", "Mcdonald's");
        //WriteUserDatatoDatabase("1", "Lotte");
        //WriteUserDatatoDatabase("2", "BurgerKing");
        //WriteUserDatatoDatabase("3", "FrankBurger");
        //WriteUserDatatoDatabase("4", "Mom'sTouch");

        ReadUserDatatoDatabase();
    }

    void WriteUserDatatoDatabase(string _userID, string _userName)
    {
        // 데이터 베이스쪽에 값을 전달해주는 기능
        reference.Child("users").Child(_userID).Child("userName").SetValueAsync(_userName);

        // 실행 시 데이터 베이스에 다음과 같이 저장
        // database 이름
        // users
        // -> _userID
        //      -> userName : _userName
        Debug.Log($"users / {_userID} / userName / {_userName}이 데이터베이스에 등록됨");
    }

    void ReadUserDatatoDatabase()
    {
        // 데이터베이스에 저장되어 있는 값을 읽어내는 기능
        // 1. 파이어베이스로부터 인스턴스를 통해 값을 얻어옴. 메인스레드에서 계속 진행

        // 코드를 정확히 이해하기 위해 알아둘 개념
        // 1. 스레드 (Thread) : 프로세스 내에서 실행되는 흐름의 단위. 둘 이상일 경우 멀티 스레드라 함
        // 2. 태스크 (Task) : 스레드를 통한 비동기 작업에 사용되는 데이터
        // 3. 무명 메소드 (annonymous method) : 주로 delegate에서 사용 한번 쓰고 다시 쓸 일 없는 기능에 대한 표현으로 사용
        // 4. 람다식 (Lamda Expression) : 코드에 대한 단순화, 주로 무명 함수 표현할 때 사용
        //                               => 연산자를 통해 해당 값이 무엇인지 처리
        // 컴파일러가 코드 생성 시 많으 ㄴ자원을 쓰게 돼서 성능 저하 유발
        // 간단한 작업에 대한 표기로만 사용하는 것이 바람직
        FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted) // 태스크가 오류인 경우
            {
                // 오류에 대한 핸들링 작업
            }
            else if (task.IsCompleted) // 태스크가 완료된 경우 
            {
                // 파이어베이스에서 지원하는 데이터 송수신용 객체(클래스)
                DataSnapshot dataSnapshot = task.Result; // 태스크 결과물을 받아옴

                for (int i = 0; i < dataSnapshot.ChildrenCount; i++)
                {
                    Debug.Log(dataSnapshot.Child(i.ToString()).Child("userName").Value);
                }
            }
        });
        
    }
}
