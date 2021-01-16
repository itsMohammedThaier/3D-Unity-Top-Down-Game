using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaButtons : MonoBehaviour
{
    public string websiteLink;
    private void OnCollisionEnter(Collision other) 
    {
        if (other.collider.tag == "Player")
            EnterWebsite();
    }

    public void EnterWebsite()
    {
        Application.OpenURL(websiteLink);
    }
}
