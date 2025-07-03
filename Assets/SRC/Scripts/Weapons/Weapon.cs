using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform _target;
    public WeaponScriptable brain;
    public Vector3 targetPos;
    public Vector3 targetDir;
    private bool isReady;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTarget(Transform Target)
    {
        _target = Target;
        targetPos = _target.position;
        targetDir = (targetPos - transform.position).normalized;
        transform.Rotate(targetDir, Space.World);
        isReady = true;
        GetKind();

        Destroy(this.gameObject, 10f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(this);
            Destroy(other);
        }
    }

    void GetKind()
    {
        switch(brain.Type)
        { 
            case WeaponType.Proj:
                TargetProj();
                break;
        }
    }

    void TargetProj()
    {
        if (isReady)
        {
            transform.position += targetDir * Time.deltaTime * brain.Speed;
        }
    }
}
