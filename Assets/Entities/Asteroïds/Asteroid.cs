using UnityEngine;

namespace DoNotModify {
    public class AsteroidView {
        public AsteroidView(Asteroid asteroid) { _asteroid = asteroid; }

        Asteroid _asteroid;

        // Variables
        public float Radius => _asteroid.Radius;
        public Vector2 Position => _asteroid.Position;
    }

    public class Asteroid : MonoBehaviour {
        public AsteroidView view;

        public Vector2 Position => (Vector2)(transform.position);
        public float Radius => _collider.radius * Mathf.Abs(_collider.transform.lossyScale.x);

        private CircleCollider2D _collider = null;

        void Awake() {
            _collider = GetComponentInChildren<CircleCollider2D>();

            view = new AsteroidView(this);
            GameManager.Instance.GetGameData().Asteroids.Add(view);
        }

        private void OnDestroy() {
            GameManager.Instance.GetGameData().Asteroids.Remove(view);
        }
    }

}