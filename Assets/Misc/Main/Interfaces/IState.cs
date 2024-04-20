using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
    void FixedUpdate();
    void LateUpdate();
    void UpdateTargetRotationData(float angle);
    void SmoothRotateToTargetRotation();
    void OnCollisionEnter(Collision collision);
    void OnCollisionExit(Collision collision);
    void OnCollisionStay(Collision collision);
    void OnTriggerEnter(Collider Collider);
    void OnTriggerExit(Collider Collider);
    void OnTriggerStay(Collider Collider);
    void OnAnimationTransition();
}
