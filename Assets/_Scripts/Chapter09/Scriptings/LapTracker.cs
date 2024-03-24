using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
namespace Chapter.LogicAndGameplay
{

    public class LapTracker : MonoBehaviour
    {
        [SerializeField] Transform target = null;

        [SerializeField] int longestPermittedShortcut = 2;
        [SerializeField] GameObject wrongWayIndicator;
        [SerializeField] UnityEngine.UI.Text lapCounter;

        int lapsComplete = 0;
        CheckPoint lastSeenCheckpoint;

        CheckPoint[] allCheckPoints;

        CheckPoint StartCheckPoint
        {
            get
            {
                return FindObjectsOfType<CheckPoint>()
                .Where(c => c.isLapStart)
                .FirstOrDefault();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            UpdateLapCounter();
            wrongWayIndicator.SetActive(false);

            allCheckPoints = FindObjectsOfType<CheckPoint>();

            CreateCircuit();
            lastSeenCheckpoint = StartCheckPoint;
        }





        // Update is called once per frame
        private void Update()
        {
            var nearestCheckPoint = NearestCheckPoint();

            if (nearestCheckPoint == null)
            {
                return;
            }
            if (nearestCheckPoint.index == lastSeenCheckpoint.index)
            {
            }
            else if (nearestCheckPoint.index > lastSeenCheckpoint.index)
            {
                var distance = nearestCheckPoint.index - lastSeenCheckpoint.index;
                if (distance > longestPermittedShortcut + 1)
                {
                    wrongWayIndicator.SetActive(true);
                }
                else
                {
                    lastSeenCheckpoint = nearestCheckPoint;
                    wrongWayIndicator.SetActive(false);
                }
            }
            else if (nearestCheckPoint.isLapStart && lastSeenCheckpoint.next.isLapStart)
            {
                lastSeenCheckpoint = nearestCheckPoint;
                lapsComplete += 1;
                UpdateLapCounter();
            }
            else
            {
                wrongWayIndicator.SetActive(true);
            }

        }

        CheckPoint NearestCheckPoint()
        {
            if (allCheckPoints == null)
            {
                return null;
            }
            CheckPoint nearestSoFar = null;
            float nearestDistanceSoFar = float.PositiveInfinity;
            for (int c = 0; c < allCheckPoints.Length; c++)
            {
                var checkpoint = allCheckPoints[c];
                var distance = (target.position - checkpoint.transform.position).sqrMagnitude;
                if (distance < nearestDistanceSoFar)
                {
                    nearestSoFar = checkpoint;
                    nearestDistanceSoFar = distance;
                }

            }
            return nearestSoFar;
        }
        void UpdateLapCounter()
        {
            lapCounter.text = string.Format("Lap {0}", lapsComplete + 1);

        }
        void CreateCircuit()
        {
            var index = 0;
            var currentCheckPoint = StartCheckPoint;
            do
            {
                currentCheckPoint.index = index;
                index += 1;
                currentCheckPoint = currentCheckPoint.next;
                if (currentCheckPoint == null)
                {
                    Debug.LogError("The circuit is not closed");
                    return;
                }
            } while (currentCheckPoint.isLapStart == false);
        }
        void OnDrawGizmos()
        {
            var nearest = NearestCheckPoint();
            if (target != null && nearest != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(target.position, nearest.transform.position);
            }
        }
    }
}
