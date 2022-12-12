using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReptileInteractable : InteractableObject
{
    [SerializeField] Renderer render;
    [SerializeField] GameObject mutateParticles;
    [SerializeField] GameObject dragonPrefab;
    [SerializeField] Transform dragonPos;
    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    public override void ActivationSequence()
    {
        StartCoroutine(Mutate());
    }

    IEnumerator Mutate()
    {
        float speed = player.speed;
        player.speed = 0;
        Instantiate(mutateParticles, transform.position, new Quaternion(-0.707106829f, 0, 0, 0.707106829f));
        yield return new WaitForSeconds(.5f);
        render.enabled = false;
        GetComponent<Collider>().enabled = false;
        GameObject dragon = Instantiate(dragonPrefab, transform.position, transform.rotation);
        dragon.GetComponentInChildren<Dragon>().playerSpeed = speed;
        dragon.transform.localScale = .01f * Vector3.one;
        dragon.transform.DOMove(dragonPos.position, 2f).SetEase(Ease.InOutSine);
        dragon.transform.DOScale(Vector3.one, 2f).SetEase(Ease.InOutSine).OnComplete(() => 
        {
            dragon.GetComponentInChildren<Dragon>().anim.SetTrigger("Start");
        });

        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
