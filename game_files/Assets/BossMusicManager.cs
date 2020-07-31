using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
public class BossMusicManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _boss;
    private bool musicOn = false;
    private AudioManager _manager;

    void Start()
    {
        _manager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_boss.GetComponent<BossInteraction>().ReturnActiveState() && !musicOn)
        {
            _manager.PlayAudio(_manager.GetOtherAudioClip());
            musicOn = true;
        }

        if(!_boss.activeSelf)
            GetComponent<AudioSource>().Pause();
    }
}
