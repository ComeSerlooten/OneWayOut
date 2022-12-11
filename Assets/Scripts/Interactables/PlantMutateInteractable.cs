using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlantMutateInteractable : InteractableObject
{
    public UnityEvent postParticles;
    [SerializeField] Transform monsterPlant;
    [SerializeField] Transform particlePos;
    [Space]
    [SerializeField] Transform mouthPos;
    [SerializeField] GameObject leafyKey;
    [Space]
    [SerializeField] Animator anim;
    [SerializeField] GameObject mutateParticles;
    PlayerMovement player;
    float initialSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    public override void ActivationSequence()
    {
        Instantiate(mutateParticles, particlePos.position, Quaternion.identity, this.transform);
        StartCoroutine(ParticleEnd());
    }

    IEnumerator ParticleEnd()
    {
        yield return new WaitForSeconds(2f);

        postParticles.Invoke();
    }

    Vector3 GetToPlayerLook()
    {
        Vector3 plantToPlayer = (player.transform.position - monsterPlant.position).normalized;
        plantToPlayer.y = 0;

        return plantToPlayer;
    }

    public  void BiteDown()
    {
        initialSpeed = player.speed;
        player.speed = 0;

        //yield return new WaitForSeconds(1f);
        monsterPlant.DOLookAt(monsterPlant.position + GetToPlayerLook(), 1f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            transform.DOMove(transform.position, 1f).OnComplete(() =>
            {
                anim.SetTrigger("Bite");
                StartCoroutine(AfterBite());
            });
        });

        //yield break;
    }

    IEnumerator AfterBite()
    {
        yield return new WaitForSeconds(40f/24);
        anim.SetBool("used", true);
        //Invoke death

        yield return new WaitForSeconds(.5f);
        Instantiate(leafyKey, mouthPos.position, mouthPos.rotation);

        yield return new WaitForSeconds(.5f);
        player.speed = initialSpeed;

        yield break;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
