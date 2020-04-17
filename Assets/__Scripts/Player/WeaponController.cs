using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float arrowSpeed = 15.0f;
    [SerializeField] private float firingRate = 0.3f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject arrowPrefabs;
    [SerializeField] private AudioClip shootClip;
    [SerializeField][Range(0f, 1.0f)] private float shootVol = 0.5f;
    [SerializeField] private ArrowTypes arrowTypes;
    [SerializeField] private List<Image> arrowImages;

    private Animator anim;
    private Coroutine firingCoroutine;
    private GameObject arrowParent;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        arrowParent = GameObject.Find("Arrow Parent");
        EnableArrowImages();

        if (!arrowParent)
        {
            arrowParent = new GameObject("Arrow Parent");
        }
    }

    void Update()
    {
        CycleArrows();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Coroutine to fire bullets
            firingCoroutine = StartCoroutine(FireCoroutine());
        }
        
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Stop the routine once the mouse button is let go
            StopCoroutine(firingCoroutine);
        }
    }

    // Coroutine returns an IEnumerator type
    private IEnumerator FireCoroutine()
    {
         while(true)
        {
            // Create a bullet on the transform Fire Point so bullet fires from the
            // gun barrel and from the same rotation as it
            GameObject arrow = Instantiate(arrowPrefabs, firePoint.position, firePoint.rotation, arrowParent.transform);
            Rigidbody2D rbb = arrow.GetComponent<Rigidbody2D>();
            // Play shooting animation
            anim.SetTrigger("Active");
            // Play sound clip
            audioSource.PlayOneShot(shootClip, shootVol);
            // Fire bullet in the direction the player is facing
            rbb.velocity = firePoint.right * arrowSpeed;

            yield return new WaitForSeconds(firingRate);
        }
    }

    // Allow the player to cycle through different arrow types
    private GameObject CycleArrows()
    {
        // Set the current arrow prefab using a config file and set the UI Image
        // by accessing the List index of the where the arrow image is stored
        // and enable the currently used arrow and disable the other ones.
        // Reference: https://web.microsoftstream.com/video/82de6b44-8d57-470b-8f0f-7326bc36195f
        if(Input.GetKey(KeyCode.Q))
        {
            arrowPrefabs = arrowTypes.GetStartArrow();
            arrowImages[0].enabled = true;
            arrowImages[1].enabled = false;
            arrowImages[2].enabled = false;
        }

        if(Input.GetKey(KeyCode.E))
        {
            arrowPrefabs = arrowTypes.GetFireArrow();
            arrowImages[0].enabled = false;
            arrowImages[1].enabled = true;
            arrowImages[2].enabled = false;

            
        }

        if(Input.GetKey(KeyCode.R))
        {
            arrowPrefabs = arrowTypes.GetPoisonArrow();
            arrowImages[0].enabled = false;
            arrowImages[1].enabled = false;
            arrowImages[2].enabled = true;
        }

        return arrowPrefabs;
    }

    private void EnableArrowImages()
    {
            arrowImages[0].enabled = true;
            arrowImages[1].enabled = false;
            arrowImages[2].enabled = false;
    }
}
