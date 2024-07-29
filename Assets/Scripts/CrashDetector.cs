using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CrashDetector : MonoBehaviour
{
    // Start is called before the first frame update

    /* private void OnTriggerEnter2D(Collider2D collision) // bu metodu kullaniyorsak circle colliderinin isTrigger oz. acik olmasi gerekiyor.
                                                        // bu metod cok saglikli degil cunku oyuncunun kafasi ground icinden geciyor.
    {
        if (collision.tag == "ground") 
        {
            Debug.Log("game over");
        }
    } */

    CircleCollider2D circleCollider2d;
    [SerializeField] public float delayTime = 0.5f;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] AudioClip CrashSFX;
    bool playonce = true;
    private void Start()
    {
        circleCollider2d = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground") && circleCollider2d.IsTouching(collision.collider))
        {

            //(collision.collider) yerine circleCollider2d.IsTouching(collision.gameObject.GetComponent<Collider2D>()) yazilabilirdi. İki durum da playerin circlecolliderinin baska bir collidera carpip carpmadigini tespit ediyor.
            // Trigger ile icinden gecmesindense mapte gorunurken yere kafasini vurmasi goruntuyu bozmuyor.
            FindObjectOfType<PlayerController>().DisableMove();
            crashParticle.Play();
            if (playonce == true)
            {
                GetComponent<AudioSource>().PlayOneShot(CrashSFX, 0.5f);
                playonce = false;
            }
            

            Invoke("ReloadScene", delayTime);

        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }


}       
    
