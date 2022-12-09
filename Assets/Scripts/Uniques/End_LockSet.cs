using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_LockSet : MonoBehaviour
{
    [SerializeField] UseEmptyHand doorOpening;
    
    public enum Lock
    {
        None,
        Burning,
        Leaf,
        Neon,
        Rust,
        Pixel
    }

    [Space]
    [SerializeField] bool fireUnlocked = false;
    [SerializeField] bool leafUnlocked = false;
    [SerializeField] bool neonUnlocked = false;
    [SerializeField] bool rustUnlocked = false;
    [SerializeField] bool pixelUnlocked = false;
    [Space]
    public bool allUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        CheckAll();
    }

    void CheckAll()
    {
        allUnlocked = fireUnlocked && leafUnlocked && neonUnlocked && rustUnlocked && pixelUnlocked;
        doorOpening.canBeUsed = allUnlocked;
    }

    public void SetLockState(int choice)
    {
        switch((Lock)choice)
        {
            case Lock.Burning:
                fireUnlocked = true;
                break;

            case Lock.Leaf:
                leafUnlocked = true;
                break;

            case Lock.Neon:
                neonUnlocked = true;
                break;

            case Lock.Pixel:
                pixelUnlocked = true;
                break;

            case Lock.Rust:
                rustUnlocked = true;
                break;

        }

        CheckAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
