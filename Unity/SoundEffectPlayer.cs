using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour {

    public SoundCategory[] soundCategories;
    public Dictionary<string, SoundCategory> soundMap;
    AudioSource src;
    void Start()
    {
        src = GetComponent<AudioSource>();
        soundMap = new Dictionary<string, SoundCategory>();
        foreach(SoundCategory c in soundCategories)
        {
            soundMap[c.name] = c;
        }
    }

    public void PlaySound(string category)
    {
        if(!soundMap.ContainsKey(category))
        {
            Debug.LogError("Category " + category + "not found!");
            return;
        }
        AudioClip clip = soundMap[category].GetRandomClip();
        src.PlayOneShot(clip);
    }
	
}


[System.Serializable]
public class SoundCategory : System.Object
{
    public string name = "INSERT_NAME";
    public AudioClip[] clips;
    public AudioClip GetRandomClip()
    {
        AudioClip rv = clips[Random.Range(0, clips.Length)];
        return rv;
    }
}
