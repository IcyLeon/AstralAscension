using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
    void FixedUpdate();
    void LateUpdate();
    void OnEnable();
    void OnDisable();
    void StartAnimation(string parameter);
    void StopAnimation(string parameter);
    void SetAnimationTrigger(string parameter);
    void UpdateTargetRotationData(float angle);
    void OnCollisionEnter(Collision collision);
    void OnCollisionExit(Collision collision);
    void OnCollisionStay(Collision collision);
    void OnTriggerEnter(Collider Collider);
    void OnTriggerExit(Collider Collider);
    void OnTriggerStay(Collider Collider);
    void OnAnimationTransition();
}
