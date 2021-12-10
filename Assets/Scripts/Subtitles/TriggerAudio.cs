using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour {
    private GameObject activeGameObject;
    public AudioObject clipToPlay;
    
    private void Start() {
        activeGameObject = GameObject.Find("Garland");
        activeGameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (gameObject.tag == "lastEvent") {
                activeGameObject.SetActive(true);
            }
            Vocals.instance.Say(clipToPlay);
        }
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<BoxCollider>().enabled = false;
    }
}
