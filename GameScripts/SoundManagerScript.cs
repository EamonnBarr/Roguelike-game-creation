using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip PlayerShoot, ItemSound, PlayerDeathSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    // Sets the audio path into the respective variable.
    // Also Sets the audioSrc to be used in the switch.
    void Start()
    {
        PlayerShoot = Resources.Load<AudioClip>("PlayerShoot");
        ItemSound = Resources.Load<AudioClip>("ItemSound");
        PlayerDeathSound = Resources.Load<AudioClip>("PlayerDeathSound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If the string matches Playershoot it will play that audio clip once.
    //This is the same with the other matched strings in the cases.

    public static void PlaySound (string clip)
    {
        switch (clip) {
            case "PlayerShoot":
                audioSrc.PlayOneShot(PlayerShoot);
                break;
            case "ItemSound":
                audioSrc.PlayOneShot(ItemSound);
                break;
            case "PlayerDeathSound":
                audioSrc.PlayOneShot(PlayerDeathSound);
                break;

        }
        
    }

}
