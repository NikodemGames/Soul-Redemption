using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
[RequireComponent(typeof(AudioSource))]
public class Door : Interact
{
    // Start is called before the first frame update
    public PlayableDirector timeline;
    public override void Interaction()
    {
        base.Interaction();
        gameObject.GetComponent<AudioSource>().Play();
        timeline.gameObject.SetActive(true);
        
    }
}
