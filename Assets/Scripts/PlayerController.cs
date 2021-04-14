using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 15;
    [SerializeField]
    Camera cam;
    [SerializeField]
    TMP_Text deathCountText;
    [SerializeField]
    TMP_Text winText;
    [SerializeField]
    GameObject winScreen;

    Rigidbody rb;

    Vector3 start;
    int deathCount;
    float timePlayed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        start = transform.position;
        deathCountText.text = "Deaths: ";
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < movementSpeed)
        {
            rb.AddForce(Input.GetAxis("Horizontal") * movementSpeed / 2, 0, 0);
            rb.AddForce(0, 0, Input.GetAxis("Vertical") * movementSpeed / 2);

            timePlayed += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        cam.transform.rotation = Quaternion.Euler(37.5f, 0, 0);
        cam.transform.position = new Vector3(transform.position.x, 10, transform.position.z - 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Win"))
        {
            winText.text = "Time: " + timePlayed.ToString("F2");
            winScreen.SetActive(true);
        }
    }

    public void KillPlayer()
    {
        transform.position = start;
        deathCount++;
        deathCountText.text = "Deaths: " + deathCount;
    }
}
