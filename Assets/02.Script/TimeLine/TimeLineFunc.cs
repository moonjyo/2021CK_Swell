using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeLineFunc : MonoBehaviour
{
   public GameObject[] Stars;

   public GameObject ShakeStar;
    public float MoveTime;
    public void StarMove()
    {
        for(int i = 0; i <  Stars.Length; ++i)
        {
            float randomx = Random.Range(2, 14);
            float randomy = Random.Range(2, 14);
            float randomz = Random.Range(2, 14);

            Stars[i].transform.DOMove(new Vector3(randomx, randomy, randomz), MoveTime).OnComplete(() => { ActiveFalse(); });

        }
    }


    public void ActiveFalse()
    {
        for(int i = 0; i  < Stars.Length; ++i)
        {
            Stars[i].SetActive(false);
        }
    }    


    public void StarShake()
    {
        ShakeStar.transform.DOMove(new Vector3(10f ,15f , 15f), 1f);
    }


}
