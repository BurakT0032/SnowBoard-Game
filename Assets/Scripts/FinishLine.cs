using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float delayTime = 2f;
    [SerializeField] ParticleSystem finishParticle;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            finishParticle.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", delayTime);
            FindAnyObjectByType<PlayerController>().DisableMove();
        }
    }
    void ReloadScene ()
    {
        SceneManager.LoadScene(0);
    }

}
