using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    
    [Header("FireBreath")] 
    [SerializeField] Transform insideMouth;
    [SerializeField] GameObject fireParticles;
    public float fireDuration = 5f;
    List<GameObject> flames;
    [Header("Circle of flames")]
    public uint flameAmount = 8;
    public float flameDist = 4;
    public float flameDelay = .05f;
    Transform cam;
    PlayerMovement player;
    [HideInInspector] public float playerSpeed;
    [HideInInspector] public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        player = GameObject.FindObjectOfType<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    public void BreatheFire()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        GameObject fb = Instantiate(fireParticles, insideMouth);
        fb.transform.localPosition = Vector3.zero;
        //go.transform.localRotation = Quaternion.identity;
        StartCoroutine(StopFireAfter(fb, 2 + fireDuration));
        yield return new WaitForSeconds(2f);
        
        Vector3 playerForward = (new Vector3(cam.forward.x, 0, cam.forward.z)).normalized * flameDist;

        for(int i = 0; i < flameAmount; i++)
        {
            Vector3 flamePos = Quaternion.AngleAxis(360 * i / flameAmount, Vector3.up) * playerForward;
            GameObject flame = Instantiate(fireParticles, cam.position + flamePos - Vector3.up *1.5f , new Quaternion(-0.707106829f, 0, 0, 0.707106829f));
            StartCoroutine(StopFireAfter(flame, fireDuration - flameDelay * i));
            yield return new WaitForSeconds(flameDelay);
        }

        yield return new WaitForSeconds(1 + (fireDuration - flameDelay * flameAmount));
        player.speed = playerSpeed;
    }

    IEnumerator StopFireAfter(GameObject particleSystem, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ParticleSystem.EmissionModule emissor = particleSystem.GetComponent<ParticleSystem>().emission;
        emissor.enabled = false;
        yield return new WaitForSeconds(5f);
        Destroy(particleSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
