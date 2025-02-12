using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IObserver
{
    [Header("Ref")]
    [SerializeField] Subject playerMove;
    [SerializeField] Subject gravity;
    [SerializeField] Subject gate;
    AudioSource audSource;
    
    [Header("Sound")]
    public AudioClip landedSound;
    public AudioClip jumpSound;
    public AudioClip gravitySound;
    public AudioClip activateSound;

    void Awake()
    {
        audSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.Landed):                
                audSource.clip = landedSound;
                audSource.Play();
                print("Play Landed Sound");
                return;

            case (PlayerAction.Jump):
                audSource.clip = jumpSound;
                audSource.Play();
                print("Play Jump Sound");
                return;

            case (PlayerAction.GravityChange):
                audSource.clip = gravitySound;
                audSource.Play();
                print("Play Gravity Sound");
                return;

            case (PlayerAction.Activate):
                audSource.clip = activateSound;
                audSource.Play();
                print("Play Activate Sound");
                return;

            default:
                return;
        }
    }

    void OnEnable()
    {
        playerMove.AddObserver(this);        
        gravity.AddObserver(this);
        gate.AddObserver(this);
    }

    void OnDisable()
    {
        playerMove.RemoveObserver(this);
        gravity.RemoveObserver(this);
        gate.RemoveObserver(this);
    }
}
