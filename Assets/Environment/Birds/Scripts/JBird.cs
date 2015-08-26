using UnityEngine;
using System.Collections;

public class JBird : MonoBehaviour {

	public AudioClip[] sounds;
	
	AudioSource audioSource;
	static System.Random rnd = new System.Random();

	// Use this for initialization
	void Start () {
		audioSource = (AudioSource) GetComponent(typeof(AudioSource));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!audioSource.isPlaying) {
			int n = rnd.Next(1000);
			if (n > 997) {
				n = rnd.Next(sounds.Length);
				if (sounds[n].isReadyToPlay) {
					audioSource.PlayOneShot(sounds[n]);
				}
			}
		}
	}
}
