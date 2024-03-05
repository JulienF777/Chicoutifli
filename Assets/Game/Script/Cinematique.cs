using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class Cinematique : MonoBehaviour
{
    public PlayableDirector timelineDirector;

    void Start()
    {
        Debug.Log("Cinematique démarrée");
    }

    void Update()
    {
        if (timelineDirector != null && timelineDirector.state == PlayState.Playing)
        {
            if (timelineDirector.time >= timelineDirector.duration)
            {
                Debug.Log("Cinematique terminée");
                SceneManager.LoadScene("Lobby");
            }
            else
            {
                Debug.Log("Cinematique en cours");
            }
        }
    }
}
