using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject revolver;
    public GameObject shotgun;
    public GameObject rifle;
    public GameObject dinamyte;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            revolver.SetActive(true);
            shotgun.SetActive(false);
            rifle.SetActive(false);
            dinamyte.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            revolver.SetActive(false);
            shotgun.SetActive(true);
            rifle.SetActive(false);
            dinamyte.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            revolver.SetActive(false);
            shotgun.SetActive(false);
            rifle.SetActive(true);
            dinamyte.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            revolver.SetActive(false);
            shotgun.SetActive(false);
            rifle.SetActive(false);
            dinamyte.SetActive(true);
        }
    }
}
