using UnityEngine;

public class EnemyHelth : MonoBehaviour
{

    //Acionar a camera principal
    private Camera _camera;
    public float maxHealth = 100f;

    private void Start()
    {
        _camera = Camera.main;
    }    

    void Update()
    {
        //Rotacionar o objeto na direcao da camera
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);   
    }
}
