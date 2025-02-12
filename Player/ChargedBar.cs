using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargedBar : MonoBehaviour
{
    public static ChargedBar instance;

    [SerializeField] private Slider chargedBar;

    [SerializeField] private playerMove playerForce;

    [SerializeField] private Transform target;
    [SerializeField] private float yOffset;

    [SerializeField] private float currentForce;
    

    private void Awake()
    {
        instance = this;       
        
    }

    // Start is called before the first frame update
    void Start()
    {
        chargedBar.gameObject.SetActive(false);
        currentForce = playerForce.jumpForce;
        UpdateChargedBar(currentForce, playerForce.maxJumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(target.position.x, target.position.y + yOffset);        
    }

    public void ShowChargedBar()
    {
        chargedBar.gameObject.SetActive(true);
    }

    public void HideChargedBar()
    {
        chargedBar.gameObject.SetActive(false);
    }

    public void UpdateChargedBar(float currentValue, float maxValue)
    {
        chargedBar.value = currentValue / maxValue;
    }
}
