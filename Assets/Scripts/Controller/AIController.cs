using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;

        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float waypointTolarence = 1f;
        [SerializeField] float waypointLifeTime = 3f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] PatrolPath patrolPath;

        Vector3 enemyLocation;


        float timeSinceLastSawPlayer;
        float timeSinceArrivedWaypoint;
        int currentWaypointIndex = 0;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            enemyLocation = transform.position;
        }

        void Update()
        {
            if (health.IsDead())
            {
                return;
            }

            if (DistanceToPlayer() < chaseDistance && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                Vector3 nextPosition = enemyLocation;

                if (patrolPath != null)
                {
                    if (AtWayPoint())
                    {
                        timeSinceArrivedWaypoint = 0;
                        CycleWayPoint();
                    }

                    nextPosition = GetNextWayPoint();
                }

                if (timeSinceArrivedWaypoint > waypointLifeTime)
                {
                    mover.StartMoveAction(nextPosition, patrolSpeedFraction);
                }
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedWaypoint += Time.deltaTime;
        }

        private Vector3 GetNextWayPoint()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        private void CycleWayPoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceWaypoint = Vector3.Distance(transform.position, GetNextWayPoint());

            return distanceWaypoint < waypointTolarence;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
