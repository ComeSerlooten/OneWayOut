using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Glow : MonoBehaviour
{
    [SerializeField] Color glowColor_;
    [SerializeField] [Range(0.05f, 5f)] float glowTime = 2f;
    [Range(0.0f, 1f)] public float intensity = 0;
    [Range(0, 20f)] public float maxViewDistance = 10f;
    public Transform observer;

    public bool canGlow = true;
    public bool glow = false;
    public bool isGlowing = false;

    Renderer render;
    Material mat;
    Tweener tw = null;
    public bool shineUp = false;
    bool twAlive = false;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        mat = render.material;

        mat.EnableKeyword("_EMISSION");
    }

    float MaxIntensity()
    {
        float distanceToObs = (observer.position - transform.position).magnitude;
        float distRatio = Mathf.Pow(Mathf.Clamp(maxViewDistance / distanceToObs, 0, 1),3);
        distRatio = Mathf.Clamp(distRatio, 0, 1);
        //if (distanceToObs > maxViewDistance) return 0;

        if (canGlow) return (distRatio);
        else return 0;
    }

    IEnumerator DoGlow()
    {
        DOTween.Kill(this.mat);
        twAlive = false;

        isGlowing = true;
        while(glow)
        {
            twAlive = true;

            tw = DOTween.To(() => intensity, x => intensity = x, 1, glowTime/2)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    tw = DOTween.To(() => intensity, x => intensity = x, 0, glowTime / 2)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() => { twAlive = false; })
                    .OnStart(() => { shineUp = false; });
                })
                .OnStart(() => { shineUp = true; });


            bool breakWhile = false;
            for(int i = 0; i < 10; i++)
            {
                if (!glow)
                {
                    breakWhile = true;
                    DOTween.Kill(this.mat);
                    twAlive = false;
                    break;
                }
                yield return new WaitForSeconds((glowTime)/10);
            }

            if (breakWhile) break;   
        }

        tw = DOTween.To(() => intensity, x => intensity = x, 0, .25f)
               .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    shineUp = false;
                    DOTween.Kill(this.mat);
                    twAlive = false;
                    mat.SetColor("_EmissionColor", glowColor_ * 0);
                    isGlowing = false;
                });
        
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (glow && !isGlowing)
        {
            StopAllCoroutines();
            twAlive = false;
            StartCoroutine(DoGlow());
        }

        if(isGlowing) mat.SetColor("_EmissionColor", glowColor_ * intensity * MaxIntensity());
    }

    private void OnDestroy()
    {
        Destroy(mat);
    }

}
