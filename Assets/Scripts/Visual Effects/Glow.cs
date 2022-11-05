using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Glow : MonoBehaviour
{

    [SerializeField] Color glowColor_ = new Color32(0, 210, 255,0);
    [SerializeField] [Range(0.05f, 5f)] float glowTime = 2f;
    [Range(0.0f, 1f)] public float intensity = 0;
    [Range(0, 20f)] public float maxViewDistance = 10f;
    public Transform observer;

    public bool canGlow = true;
    public bool glow = false;
    public bool isGlowing = false;

    [Space] [SerializeField] GameObject _target;
    public List<Material> mats;
    Tweener tw = null;
    bool shineUp = false;
    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (observer == null) observer = Camera.main.transform;
        if (_target == null) _target = this.gameObject;
        //render = _target.GetComponent<Renderer>();
        //mat = render.material;
        Transform[] objects = _target.GetComponentsInChildren<Transform>();
        foreach (Transform t in objects)
        {
            if(t.GetComponent<Renderer>())
            {
                Renderer r = t.GetComponent<Renderer>();
                mats.Add(t.GetComponent<Renderer>().material);
                r.material.EnableKeyword("_EMISSION");
            }
        }

        //mat.EnableKeyword("_EMISSION");
        hasStarted = true;
    }

    public void ResetGlowing()
    {
        StopAllCoroutines();
        glow = false;
        isGlowing = false;

        foreach (Material mat in mats) mat.SetColor("_EmissionColor", glowColor_ * 0);
    }    

    float MaxIntensity()
    {
        float distanceToObs = (observer.position - _target.transform.position).magnitude;
        float distRatio = Mathf.Pow(Mathf.Clamp(maxViewDistance / distanceToObs, 0, 1),3);
        distRatio = Mathf.Clamp(distRatio, 0, 1);
        //if (distanceToObs > maxViewDistance) return 0;

        if (canGlow) return (distRatio);
        else return 0;
    }

    IEnumerator DoGlow()
    {
        foreach (Material mat in mats) DOTween.Kill(mat);

        isGlowing = true;
        while(glow)
        {

            tw = DOTween.To(() => intensity, x => intensity = x, 1, glowTime/2)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    tw = DOTween.To(() => intensity, x => intensity = x, 0, glowTime / 2)
                    .SetEase(Ease.InOutSine)
                    .OnStart(() => { shineUp = false; });
                })
                .OnStart(() => { shineUp = true; });


            bool breakWhile = false;
            for(int i = 0; i < 10; i++)
            {
                if (!glow)
                {
                    breakWhile = true;
                    foreach (Material mat in mats) DOTween.Kill(mat);
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
                    
                    foreach (Material mat in mats)
                    {
                        mat.SetColor("_EmissionColor", glowColor_ * 0);
                        DOTween.Kill(mat);
                    }
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
            StartCoroutine(DoGlow());
        }

        if(isGlowing) foreach (Material mat in mats) mat.SetColor("_EmissionColor", glowColor_ * intensity * MaxIntensity()*0.5f);
    }

    private void OnDestroy()
    {
        foreach (Material mat in mats)
        {
            mat.DOKill();
            Destroy(mat);
        }
        mats.Clear();
    }

}
