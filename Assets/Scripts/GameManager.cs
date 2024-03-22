using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int[] num = new int[3];
    public Text[] Pnum = new Text[3];
    public Text[] inputNumber;

    public int bCount;
    public int sCount;
    public int count = 0;

    System.Random r = new System.Random();


    void Start()
    {

    }

    public void Play()
    {
        RandomNumber(num);
    }

    void RandomNumber(int[] array)
    {
        HashSet<int> set = new HashSet<int>();

        while (set.Count != 3)
        {
            set.Add(Random.Range(0, 8) + 1);
        }


        set.CopyTo(array);



        //체크용
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(num[i]);
        }

    }

    public void GameStart()
    {
        for (int i = 0; i < num.Length; i++)
        {
            for (int j = 0; j < Pnum.Length; j++)
            {
                if (num[i].ToString() == Pnum[i].ToString() || num[j].ToString() == Pnum[j].ToString())
                {
                    sCount++;
                }

                else if (num[i].ToString() == Pnum[j].ToString() || num[j].ToString() == Pnum[i].ToString())
                {
                    bCount++;
                }
            }
        }

        Debug.Log($"{bCount}볼");
        Debug.Log($"{sCount}스트라이크");
    }

    public void InputNumber()
    {
        for (int i = 0; i < num.GetLength(0); i++)
        {
            Pnum[i].text = inputNumber[i].ToString();
            Debug.Log(Pnum[i].text);
        }
        count++;
    }

    public void GameReset()
    {
        bCount = 0;
        sCount = 0;
        Play();
    }
}

