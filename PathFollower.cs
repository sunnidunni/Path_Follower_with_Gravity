
using UnityEngine;
using System;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public double speed; //initial velocity
        public GameObject obj; //cart
        public double g; //gravitational acceleration
        double distanceTravelled;
        double energy;
        double v; 

        //friction calc

        // double prevx;
        // double prevy;
        // double dx;
        // double dy;
        // double Fn;
        // public double fric;



        async void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                //energy = mgh, m = 1
                energy = 57.9331  * g;

                // prevx = obj.transform.position.x;
                // prevy = obj.transform.position.y;

            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                //dx = obj.transform.position.x - prevx;
                //dy = obj.transform.position.y - prevy;
                //Fn = g * ((dx / Math.Sqrt((Math.Pow(dx, 2) + Math.Pow(dy, 2)))));
                //Debug.Log(dx);




                v = speed+Math.Sqrt(2 * (energy - g * obj.transform.position.y));
                distanceTravelled += v * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance((float)distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance((float)distanceTravelled, endOfPathInstruction);
                // prevx = obj.transform.position.x;
                // prevy = obj.transform.position.y;

            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}