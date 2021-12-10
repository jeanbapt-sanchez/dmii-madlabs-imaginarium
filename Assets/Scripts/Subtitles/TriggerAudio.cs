using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour {
    private GameObject activeGameObject;
    private Coroutine blinkCoroutine;
    public AudioObject clipToPlay;
    
    
    private void Start() {
        if (gameObject.tag == "lastEvent") {
            activeGameObject = GameObject.Find("Garland");
            activeGameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Vocals.instance.Say(clipToPlay);
            if (gameObject.tag == "lastEvent") {
                if(blinkCoroutine != null) {
                    StopCoroutine(blinkCoroutine);
                }
                blinkCoroutine = StartCoroutine(BlinkDelay());
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        GetComponent<BoxCollider>().enabled = false;
    }

    private IEnumerator BlinkDelay() {
        System.Random random = new System.Random();
        float max = 0.9f;
        float min = 0.1f;

        for (int i = 0; i < 7; i++) {
            activeGameObject.SetActive(true);
            yield return new WaitForSeconds((float)random.NextDouble() * (max - min) + min);
            activeGameObject.SetActive(false);
            yield return new WaitForSeconds((float)random.NextDouble());
            activeGameObject.SetActive(true);
        }
    }
}
