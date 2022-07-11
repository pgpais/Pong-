using System;
using Godot;

namespace Pong.Scripts
{
    [Tool]
    public class ResizeRectanglePolygonToCollider
    {
        #region References
        private readonly CollisionShape2D _collider;
        private readonly Polygon2D _shape;
        #endregion

        public ResizeRectanglePolygonToCollider(CollisionShape2D collider, Polygon2D shape)
        {
            _collider = collider;
            _shape = shape;
        }

        public void SetObjectSize(Vector2 size)
        {
            if (_collider != null)
            {
                var colliderShape = _collider.Shape as RectangleShape2D;
                colliderShape.Extents = new Vector2(size.x / 2, size.y / 2);
            }

            if (_shape != null)
            {
                var newPolygon = new Vector2[]
                {
            new Vector2(-size.x / 2, -size.y / 2),
            new Vector2(size.x / 2, -size.y / 2),
            new Vector2(size.x / 2, size.y / 2),
            new Vector2(-size.x / 2, size.y / 2)
                };


                _shape.Polygon = newPolygon;
            }
        }
    }
}