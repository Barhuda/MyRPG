using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerPosition : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    float updateIntervall;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdatePlayerPosition", 2, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePlayerPosition()
    {
        transform.position = playerPosition.position;
    }
}
