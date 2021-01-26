using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Engine
{

    public class PolygonCollisionResult 
    {
        public bool Intersect {get;set;}
        public bool WillIntersect {get; set;}
        public Vector2D MinimumTranslationVector {get;set;}
    }

    public class CollisionDetection {
        
        private List<ICollidable> _collidables;
        public CollisionDetection() {
            _collidables = new List<ICollidable>();
        }

        public void AddCollidable(ICollidable collidable) {
            _collidables.Add(collidable);
        }

        public bool RemoveCollidable(ICollidable collidable) {
            return _collidables.Remove(collidable);
        }

        public bool Contains(ICollidable collidable) {
            return _collidables.Contains(collidable);
        }

        public void Update(double timeMilliSeconds) {
            
        }

        // Check if polygon A is going to collide with polygon B.
        // The last parameter is the *relative* velocity 
        // of the polygons (i.e. velocityA - velocityB)
        public PolygonCollisionResult PolygonCollision(Polygon2D polygonA, Polygon2D polygonB, Vector2D velocity) {
            PolygonCollisionResult result = new PolygonCollisionResult();
            result.Intersect = true;
            result.WillIntersect = true;

            int edgeCountA = polygonA.Count;
            int edgeCountB = polygonB.Count;
            double minIntervalDistance = float.PositiveInfinity;
            Vector2D translationAxis = new Vector2D();
            Vector2D edge;

            Vector2D polyACenter = polygonA.Center();
            Vector2D polyBCenter = polygonB.Center();

            // Loop through all the edges of both polygons
            for (int edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB; edgeIndex++) {
                if (edgeIndex < edgeCountA) {
                    edge = polygonA.EdgeByIndex(edgeIndex);
                } else {
                    edge = polygonB.EdgeByIndex(edgeIndex - edgeCountA);
                }

                // ===== 1. Find if the polygons are currently intersecting =====

                // Find the axis perpendicular to the current edge
                Vector2D axis = new Vector2D(-edge.Y, edge.X);
                //Normalize
                var axisLen = axis.Length;
                axis.X = axis.X / axisLen;
                axis.Y = axis.Y / axisLen;

                // Find the projection of the polygon on the current axis
                double minA = 0; double minB = 0; double maxA = 0; double maxB = 0;
                ProjectPolygon(axis, polygonA, ref minA, ref maxA);
                ProjectPolygon(axis, polygonB, ref minB, ref maxB);

                // Check if the polygon projections are currentlty intersecting
                if (IntervalDistance(minA, minB, maxA, maxB) > 0)
                    result.Intersect = false;

                // ===== 2. Now find if the polygons *will* intersect =====

                // Project the velocity on the current axis
                double velocityProjection = axis * velocity;

                // Get the projection of polygon A during the movement
                if (velocityProjection < 0) {
                    minA += velocityProjection;
                } else {
                    maxA += velocityProjection;
                }

                // Do the same test as above for the new projection
                double intervalDistance = IntervalDistance(minA, minB, maxA, maxB);
                if (intervalDistance > 0) result.WillIntersect = false;

                // If the polygons are not intersecting and won't intersect, exit the loop
                if (result.Intersect == false && result.WillIntersect == false) 
                    break;

                // Check if the current interval distance is the minimum one. If so store
                // the interval distance and the current distance.
                // This will be used to calculate the minimum translation vector
                intervalDistance = Math.Abs(intervalDistance);
                if (intervalDistance < minIntervalDistance) {
                    minIntervalDistance = intervalDistance;
                    translationAxis = axis;

                    Vector2D d = polyACenter - polyBCenter;
                    if (d * translationAxis < 0)
                        translationAxis = -translationAxis;
                }
            }

            // The minimum translation vector
            // can be used to push the polygons appart.
            if (result.WillIntersect)
                result.MinimumTranslationVector = translationAxis * minIntervalDistance;
            
            return result;
        }
        //TODO: This needs to be tested
// private bool Collides(Polygon2D one, Polygon2D other, Vector2D relativeVelocity) {
// int edgeCountA;
// int edgeCountB;
// double minIntervalDistance ;
// int edgeIndex ;
// Vector2D axis;
// Vector2D edge;
// //Dim Collision As Collision2D
// double minA; 
// double minB; 
// double maxA; 
// double maxB;
// double intervalDist ;
// Vector2D translationAxis;
// double velocityProjection;
// bool intersect = true;
// bool willIntersect = true;
// Vector2D d;

// edgeCountA = one.Count;
// edgeCountB = other.Count;
// minIntervalDistance = double.MaxValue;

// for (edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB - 1; edgeIndex++)
// {
//     if (edgeIndex < edgeCountA) {
//         edge = one.EdgeByIndex(edgeIndex);
//     } else {
//         edge = other.EdgeByIndex(edgeIndex - edgeCountA);
//     }
//     axis = new Vector2D(edge.X, edge.Y);
//     double len = axis.Length;
//     axis.X = axis.X /len; axis.Y = axis.Y / len; //TO Unit vector

//     minA = 0;
//     minB = 0;
//     maxA = 0;
//     maxB = 0;

//     ProjectPolygon(axis, one, out minA, out maxA);
//     ProjectPolygon(axis, other, out minB, out maxB);
//     intervalDist = IntervalDistance(minA, minB, maxA, maxB);
//     if (intervalDist > 0) {
//         intersect = false;
//     }

//     velocityProjection = axis * relativeVelocity;
//     if (velocityProjection < 0) {
//             minA += velocityProjection;
//     } else {
//             maxA += velocityProjection;
//     }

//     intervalDist = IntervalDistance(minA, minB, maxA, maxB);
//     if (intervalDist > 0) {
//         // 'Return Nothing
//         willIntersect = false;
//     }

//     if (intersect == false && willIntersect == false) {
//         return false;
//     }

//     intervalDist = Math.Abs(intervalDist);
//     if (minIntervalDistance > intervalDist) {
//         minIntervalDistance = intervalDist;
//         translationAxis = axis;
//         d = one.CenterOfGravity.AsVector2D - other.CenterOfGravity.AsVector2D;
//         if ((d * translationAxis) < 0) {
//             translationAxis = -translationAxis;
//         }
//     }
// }

// // '<DEBUG>
// // 'If WillIntersect Then
// // '    Debug.Print("Here we are")
// // 'End If
// // '</DEBUG>

// //Collision = New Collision2D(translationAxis * minIntervalDistance, WillIntersect)
// return willIntersect;

// }
        private void ProjectPolygon(Vector2D Axis, Polygon2D Polygon, ref double min, ref double max)
        {
            double d ;
            Vector2D p;
            int i;

            p = new Vector2D(Polygon.Point(0).X, Polygon.Point(0).Y);
            d = Axis * p;
            min = d;
            max = d;
            for (i = 1; i < Polygon.Count; i++) {
                p = new Vector2D(Polygon.Point(i).X, Polygon.Point(i).Y);
                d = Axis * p;
                if (d < min) 
                    min = d;
                
                else if (d > max)
                    max = d;

            }

        }
        private double IntervalDistance(double MinA, double MinB, double MaxA, double MaxB) {
            if (MinA < MinB )
                return MinB - MaxA;
            else
                return MinA - MaxB;
        }
    }
}