using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float rcsRotate = 5f;
    [SerializeField] AudioClip thrust;
    [SerializeField] AudioClip dead;
    [SerializeField] AudioClip win;
    [SerializeField] ParticleSystem explosionPart;
    [SerializeField] ParticleSystem successPart;
    [SerializeField] ParticleSystem thrustPart;
    enum State {Alive,Dead,Transcending};
    State state = State.Alive;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            ThrustHandler();
            RotateHandler();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(state!=State.Alive)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("launchpad");
                break;
            case "Finish":
                PlaySuccessSequence();
                break;
            case "Booster":
                break;
            case "Jammer":
                break;
            default:
                DeathSequence();
                break;
        }
    }
    private void PlaySuccessSequence()
    {
        state = State.Transcending;
        successPart.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(win);
        Invoke("LoadNextState", 1.5f);
    }
    private void LoadNextState()
    {
        int currentlvl = SceneManager.GetActiveScene().buildIndex;
        if(currentlvl >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(currentlvl+1);
    }
    private void DeathSequence()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(dead);
        explosionPart.Play();
        Invoke("Dead", 1.5f);
    }
    private void Dead()
    {
        SceneManager.LoadScene(0);
    }
    private void ThrustHandler()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
            
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * rcsThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrust);
        }
        thrustPart.Play();
    }
    private void RotateHandler()
    {
        rigidBody.freezeRotation= true; //take manual control of rotation
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rcsRotate*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rcsRotate*Time.deltaTime);
        }
        rigidBody.freezeRotation = false; //take physical control of rotation
    }
    
}
