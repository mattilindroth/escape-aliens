using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Engine
{
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


//TODO: This needs to be tested
        private bool Collides(Polygon2D one, Polygon2D other, Vector2D relativeVelocity) {
            int edgeCountA;
            int edgeCountB;
            double minIntervalDistance ;
            int edgeIndex ;
            Vector2D axis;
            Vector2D edge;
            //Dim Collision As Collision2D
            double minA; 
            double minB; 
            double maxA; 
            double maxB;
            double intervalDist ;
            Vector2D translationAxis;
            double velocityProjection;
            bool intersect = true;
            bool willIntersect = true;
            Vector2D d;

            edgeCountA = one.Count;
            edgeCountB = other.Count;
            minIntervalDistance = double.MaxValue;

            for (edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB - 1; edgeIndex++)
            {
                if (edgeIndex < edgeCountA) {
                    edge = one.EdgeByIndex(edgeIndex);
                } else {
                    edge = other.EdgeByIndex(edgeIndex - edgeCountA);
                }
                axis = new Vector2D(edge.X, edge.Y);
                double len = axis.Length;
                axis.X = axis.X /len; axis.Y = axis.Y / len; //TO Unit vector

                minA = 0;
                minB = 0;
                maxA = 0;
                maxB = 0;

                ProjectPolygon(axis, one, out minA, out maxA);
                ProjectPolygon(axis, other, out minB, out maxB);
                intervalDist = IntervalDistance(minA, minB, maxA, maxB);
                if (intervalDist > 0) {
                    intersect = false;
                }

                velocityProjection = axis * relativeVelocity;
                if (velocityProjection < 0) {
                     minA += velocityProjection;
                } else {
                     maxA += velocityProjection;
                }

                intervalDist = IntervalDistance(minA, minB, maxA, maxB);
                if (intervalDist > 0) {
                    // 'Return Nothing
                    willIntersect = false;
                }

                if (intersect == false && willIntersect == false) {
                    return false;
                }

                intervalDist = Math.Abs(intervalDist);
                if (minIntervalDistance > intervalDist) {
                    minIntervalDistance = intervalDist;
                    translationAxis = axis
                    d = one.CenterOfGravity.AsVector2D - other.CenterOfGravity.AsVector2D
                    if ((d * translationAxis) < 0) {
                        translationAxis = -translationAxis;
                    }
                }
            }

            // '<DEBUG>
            // 'If WillIntersect Then
            // '    Debug.Print("Here we are")
            // 'End If
            // '</DEBUG>

            //Collision = New Collision2D(translationAxis * minIntervalDistance, WillIntersect)
            return willIntersect;

        }
        private void ProjectPolygon(Vector2D Axis, Polygon2D Polygon, out double min, out double max)
        {
            double d ;
            Vector2D p;
            int i;

            p = new Vector2D(Polygon.Point(0).X, Polygon.Point(0).Y);
            d = Axis * p;
            min = d;
            max = d;
            for (i = 1; i < Polygon.Count - 1; i++) {
                p = new Vector2D(Polygon.Point(i).X, Polygon.Point(i).Y);
                d = Axis * p;
                if (d < min) 
                    min = d;
                
                if (d > max)
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