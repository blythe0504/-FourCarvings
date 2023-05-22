using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FourCarvings
{
    public class AudiaoManager : MonoBehaviour
    {
        static AudiaoManager current;

        public AudioClip walk_audioClip;

        public AudioSource playerSource;

        private void Awake()
        {
            current = this;
            DontDestroyOnLoad(gameObject);

            playerSource = gameObject.AddComponent<AudioSource>();
        }

        public static void PlayFootstepAudio()
        {
            current.playerSource.clip = current.walk_audioClip;
            current.playerSource.Play();
        }
    }

}