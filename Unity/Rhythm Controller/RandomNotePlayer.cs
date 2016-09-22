using UnityEngine;
using System.Collections;
public class RandomNotePlayer : MonoBehaviour {

    public static float a = Mathf.Pow(2, 1.0f / 12.0f);
    public SoundClipCategory note;
    public SemitoneChance[] semitones;
    public int aSourceCount;
    public float f0 = 392;
    AudioSource[] aSources;
    private int idx;

	void Awake() {
        aSources = new AudioSource[aSourceCount];
        for(int i = 0; i < aSourceCount; i++)
        {
            GameObject r = new GameObject();
            r.transform.parent = transform;
            aSources[i] = r.AddComponent<AudioSource>();
        }
	}

    void Start()
    {
        Play(.1f);
    }
	
	void Update () {
	
	}

    public void Play(float r)
    {
        float p = -1;
        foreach(SemitoneChance c in semitones)
        {
            if(c.chance > r )
            {
                p = Mathf.Pow(a, c.semitone);
                break;
            }
        }
        if( p != -1)
        {
            aSources[idx].pitch = p;
            note.Play(aSources[idx++]);
            idx %= aSourceCount;
        }
 
    }
    public void Play()
    {
        Play(Random.value);
   }

    [System.Serializable]
    public class SemitoneChance
    {
        public int semitone;
        [Range(0,1)]
        public float chance;
    }
}
