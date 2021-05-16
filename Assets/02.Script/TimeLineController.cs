using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
   
    [SerializeField]
    private List<PlayableDirectors> directors = new List<PlayableDirectors>();

    public void Play(string Startname)
    {
       
        foreach(var playable  in directors)
        {
            if (playable.name == Startname)
            {
                playable.playableDirector.gameObject.SetActive(true);
                playable.playableDirector.Play();
                return;
            }
        }

    }

    [System.Serializable]
    private class PlayableDirectors
    {
        public PlayableDirector playableDirector;
        public string name;

    }

}


