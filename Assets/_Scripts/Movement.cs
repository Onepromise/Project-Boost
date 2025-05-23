using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 25f;
    [SerializeField] float rotationStrength = 1000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem maineEngineParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

private void Start()
{
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();    
}
   private void OnEnable() 
   {
        thrust.Enable(); 
        rotation.Enable();
   }


   private void FixedUpdate()
    {
        ProcessThrust();

        ProcessRotation();

    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

            if(!maineEngineParticles.isPlaying)
            {
                maineEngineParticles.Play();
            }
            
        }
        else
        {
            audioSource.Stop();
            maineEngineParticles.Stop();
        }
        
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0 )
        {
            ApplyRotation(rotationStrength);

            if(!rightThrustParticles.isPlaying)
            {
                leftThrustParticles.Stop();
                rightThrustParticles.Play();
            }
        }
        else if(rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);

            if(!leftThrustParticles.isPlaying)
            {
                rightThrustParticles.Stop();
                leftThrustParticles.Play();
            }
        }
        else 
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
