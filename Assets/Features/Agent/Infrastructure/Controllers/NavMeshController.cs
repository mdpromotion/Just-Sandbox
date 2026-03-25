using Shared.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Feature.Agent.Infrastructure
{
    /// <summary>
    /// Provides navigation and movement control for a character using Unity's NavMeshAgent, enabling movement towards
    /// specified destinations and handling actions such as starting, stopping, punching, and dying.
    /// </summary>
    /// <remarks>This class requires NavMeshAgent, Transform, Rigidbody, and MaterialController components to
    /// function correctly. It manages the character's movement state and performs ground checks to determine when
    /// navigation should be enabled or disabled. Use this controller to direct character movement and apply physical
    /// forces for gameplay actions.</remarks>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(MaterialController))]
    public class NavMeshController : MonoBehaviour, INavMeshController
    {
        private NavMeshAgent _agent;
        private Transform _transform;
        private Rigidbody _rb;
        private MaterialController _materialController;

        private bool _isAlive = true;
        public Position3 Position => Mapper.ToPosition3(_transform.position);

        private float _groundCheckTimer;
        private float _timeBeforeGroundCheck = 1f;

        private const float GroundCheckInterval = 0.1f;
        private const float TimeBeforeGroundCheckDefault = 1f;

        private void Awake()
        {
            _isAlive = true;
            _materialController = GetComponent<MaterialController>();
            _agent = GetComponent<NavMeshAgent>();
            _transform = transform;
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_agent.enabled || !_isAlive)
                return;

            _timeBeforeGroundCheck -= Time.deltaTime;
            if (_timeBeforeGroundCheck > 0)
                return;

            _groundCheckTimer -= Time.deltaTime;
            if (_groundCheckTimer > 0)
                return;

            _groundCheckTimer = GroundCheckInterval;

            CheckGround();
        }
        
        public void SetSpeed(float speed)
        {
            _agent.speed = speed;
            _agent.acceleration = speed * 2;
        }

        public void SetDestination(Position3 destination)
        {
            if (_agent.enabled)
                _agent.SetDestination(Mapper.ToVector3(destination));
        }

        public void Punch(Position3 velocity)
        {
            ToggleAgent(false);
            _timeBeforeGroundCheck = TimeBeforeGroundCheckDefault;
            _rb.AddForce(Mapper.ToVector3(velocity), ForceMode.VelocityChange);
            _materialController.Hurt();
        }

        public void Die(Position3 velocity)
        {
            _isAlive = false;
            ToggleAgent(false);
            _rb.AddForce(Mapper.ToVector3(velocity), ForceMode.VelocityChange);
            UnfreezeAgent();
            _materialController.Hurt();
        }

        public void StartMovement()
        {
            if (_agent.enabled)
                _agent.isStopped = false;
        }
        public void StopMovement() 
        {
            if (_agent.enabled)
                _agent.isStopped = true;
        }

        private void CheckGround()
        {
            var origin = _transform.position + Vector3.up * 0.2f;

            if (Physics.Raycast(origin, Vector3.down, 0.4f))
            {
                ToggleAgent(true);
            }
        }

        private void ToggleAgent(bool state) 
        {
            _agent.enabled = state;
            _rb.isKinematic = state;
            _rb.useGravity = !state;
        }

        private void UnfreezeAgent()
        {
            _rb.constraints = RigidbodyConstraints.None;
        }
    }
}