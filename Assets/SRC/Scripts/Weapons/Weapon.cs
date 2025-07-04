using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform _target;
    public WeaponXScriptable brain;
    public Vector3 targetPos;
    public Vector3 targetDir;
    private bool isReady;
        
    public void GetTarget(Transform Target, WeaponXScriptable pBrain)
    {
        Debug.LogWarning("Arma" + pBrain);

        brain = pBrain;

        //GameObject prefab = Instantiate(brain.Prefab, this.gameObject.transform);
        //prefab.transform.localPosition = Vector3.zero;
        //prefab.transform.localRotation = Quaternion.identity;

        _target = Target;
        targetPos = _target.position;
        targetDir = (targetPos - transform.position).normalized;
        transform.Rotate(targetDir, Space.World);
        isReady = true;
        GetKind();

        Destroy(this.gameObject, brain.cooldown);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    void GetKind()
    {
        switch (brain.Type)
        { 
            case WeaponTypeX.Proj:
                InvokeRepeating("TargetProj", 0, Time.deltaTime);
                break;
            case WeaponTypeX.Area:
                TargetArea();
                break;
            case WeaponTypeX.Orbit:
                InvokeRepeating("TargetOrbit", 0, Time.deltaTime);
                break;
            case WeaponTypeX.Aura:
                InvokeRepeating("TargetAura", 0, Time.deltaTime);
                break;
            case WeaponTypeX.Cone:
                InvokeRepeating("TargetCone", 0, Time.deltaTime);
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

    void TargetArea()
    {

    }

    void TargetOrbit()
    {

    }

    void TargetAura()
    {

    }

    void TargetCone()
    {

    }
}
