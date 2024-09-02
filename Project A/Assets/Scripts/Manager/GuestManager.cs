using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : SingleTon<GuestManager>
{
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] Queue<string> guestQueue;
    public string[] guestArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
