using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _step01;
    [SerializeField] private AudioClip _step02;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _hurt;
    [SerializeField] private AudioClip _heal;
    [SerializeField] private AudioClip _other;
    private AudioSource _audioSource;

    // Update is called once per frame
    void Update()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StepAudio01(float volume)
    {
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(_step01);
    }

    public void StepAudio02(float volume)
    {
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(_step02);
    }

    public void AttackAudio()
    {
        _audioSource.PlayOneShot(_attack);
    }

    public void HurtAudio()
    {
        _audioSource.PlayOneShot(_hurt);
    }

    public void OtherAudio()
    {
        _audioSource.PlayOneShot(_other);
    }

    public void HealAudio()
    {
        _audioSource.PlayOneShot(_heal);
    }

    public void PlayAudio(AudioClip aclip)
    {
        _audioSource.clip = aclip;
        _audioSource.Play();
    }

    public AudioClip GetOtherAudioClip()
    {
        return _other;
    }
}