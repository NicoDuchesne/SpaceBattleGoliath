using UnityEngine;

namespace DoNotModify {
    public class SpaceShipView {
        public SpaceShipView(SpaceShip spaceship) { _spaceship = spaceship; }

        // Constants
        public float Radius => _spaceship.Radius;
        public float SpeedMax => _spaceship.SpeedMax;
        public float RotationSpeed => _spaceship.RotationSpeed;
        public float EnergyPerSecond => _spaceship.EnergyPerSecond;
        public float ThrustConsumption => _spaceship.ThrustConsumption;
        public float MineEnergyCost => _spaceship.MineEnergyCost;
        public float ShootEnergyCost => _spaceship.ShootEnergyCost;
        public float ShockwaveEnergyCost => _spaceship.ShockwaveEnergyCost;
        public float HitPenaltyDuration => _spaceship.HitPenaltyDuration;
        public float HitPenaltySpeedFactor => _spaceship.HitPenaltySpeedFactor;
        public float StunPenaltyDuration => _spaceship.StunPenaltyDuration;

        // Variables
        public int Owner => _spaceship.Owner;
        public float Thrust => _spaceship.Thrust;
        public Vector2 Velocity => _spaceship.Velocity;
        public Vector2 Position => _spaceship.Position;
        public float Orientation => _spaceship.Orientation;
        public Vector2 LookAt => _spaceship.LookAt;
        public float Energy => _spaceship.Energy;
        public int HitCount => _spaceship.HitCount;
        public float HitPenaltyCountdown => _spaceship.HitPenaltyCountdown;
        public float StunPenaltyCountdown => _spaceship.StunPenaltyCountdown;
        public int Score => _spaceship.Score;
        public int HitScore => _spaceship.HitScore;
        public int WaypointScore => _spaceship.WaypointScore;
        public bool HasShot => _spaceship.HasShot;
        public bool HasFiredShockwave => _spaceship.HasFiredShockwave;
        public bool HasDroppedMine => _spaceship.HasDroppedMine;

        private SpaceShip _spaceship;
    }

    public class SpaceShip : MonoBehaviour {
        public SpaceShipView view;

        // Constant views
        public float Radius => _collider.radius * Mathf.Abs(_collider.transform.lossyScale.x);
        public float SpeedMax => _speedMax;
        public float RotationSpeed => _rotationSpeed;
        public float EnergyPerSecond => _energyPerSecond;
        public float ThrustConsumption => _thrustConsumption;
        public float MineEnergyCost => _mineEnergyCost;
        public float ShootEnergyCost => _shootEnergyCost;
        public float ShockwaveEnergyCost => _shockwaveEnergyCost;
        public float HitPenaltyDuration => _hitDuration;
        public float HitPenaltySpeedFactor => _hitSpeedFactor;
        public float StunPenaltyDuration => _stunDuration;

        // Variable views
        public int Owner => _owner;
        public float Thrust => _thrust;
        public Vector2 Velocity => _rigidbody.linearVelocity;
        public Vector2 Position => (Vector2)(transform.position);
        public float Orientation => transform.eulerAngles.z;
        public Vector2 LookAt => new Vector2(Mathf.Cos(Orientation * Mathf.Deg2Rad), Mathf.Sin(Orientation * Mathf.Deg2Rad));
        public float Energy => _energy;
        public int HitCount => _hitCount;
        public float HitPenaltyCountdown => _hitCountdown;
        public float StunPenaltyCountdown => _stunCountdown;
        public int Score => GameManager.Instance.GetScoreForPlayer(_owner);
        public int HitScore => GameManager.Instance.GetHitScoreForPlayer(_owner);
        public int WaypointScore => GameManager.Instance.GetWayPointScoreForPlayer(_owner);
        public bool HasShot => _hasShot;
        public bool HasFiredShockwave => _hasFiredShockwave;
        public bool HasDroppedMine => _hasDroppedMine;

        // Owner
        private int _owner;

        // Prefabs
        [SerializeField]
        private Mine minePrefab;
        [SerializeField]
        private Bullet bulletPrefab;
        [SerializeField]
        private Shockwave shockwavePrefab;

        // Hit
        private const float _hitDuration = 3.0f;
        private float _hitSpeedFactor = 0.3f;
        private float _hitCountdown = 0.0f;
        private int _hitCount = 0;

        // Stun
        private const float _stunDuration = 1.5f;
        private float _stunCountdown = 0.0f;

        // Energy
        private const float _energyPerSecond = 0.12f;
        private const float _thrustConsumption = 0.5f;
        private const float _mineEnergyCost = 0.20f;
        private const float _shootEnergyCost = 0.12f;
        private const float _shockwaveEnergyCost = 0.4f;
        private float _energy = 1.0f;

        // Visual
        [SerializeField]
        private Color color;
        [SerializeField]
        private Material hitMaterial = null;
        [SerializeField]
        private GameObject stunFXPrefab;
        private GameObject _stunFX = null;
        private MeshRenderer _meshRenderer = null;

        // Speed
        private const float _speedForThrust = 5.0f;
        private const float _speedMax = 2.5f;
        private float _thrust = 0.0f;

        // Orientation
        private const float _rotationSpeed = 180.0f;
        private float _orientationTarget = 0.0f;

        private BaseSpaceShipController _controller = null;

        // Physics
        private Rigidbody2D _rigidbody = null;
        private CircleCollider2D _collider = null;
        private ParticleSystem _thrustFX = null;

        private Vector2 previousPosition = Vector2.zero;
        private float previousRotation = 0.0f;
        private bool willCheckRigidbody = false;

        // Audio
        [SerializeField]
        public AudioSource shootAudio = null;
        public AudioSource dropMineAudio = null;
        public AudioSource fireShockwaveAudio = null;
        public AudioSource hitAudio = null;
        public AudioSource stunAudio = null;
        public AudioSource shockAudio = null;

        // Actions
        private bool _hasShot = false;
        private bool _hasFiredShockwave = false;
        private bool _hasDroppedMine = false;

        private void Awake() {

            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponentInChildren<CircleCollider2D>();
            previousPosition = _rigidbody.position;
            previousRotation = _rigidbody.rotation;
            _thrustFX = GetComponentInChildren<ParticleSystem>();
            ParticleSystem.EmissionModule emission = _thrustFX.emission;
            emission.enabled = false;
            _orientationTarget = transform.eulerAngles.z;
            _meshRenderer = GetComponentInChildren<MeshRenderer>();

            view = new SpaceShipView(this);
            GameManager.Instance.GetGameData().SpaceShips.Add(view);
        }

        private void OnDestroy() {
            GameManager.Instance.GetGameData().SpaceShips.Remove(view);
        }


        private void Update() {
            if (GameManager.Instance.IsGameFinished())
                return;

            if (IsHit()) {
                _hitCountdown = Mathf.Max(_hitCountdown - Time.deltaTime, 0);
                if (_hitCountdown <= 0) {
                    _meshRenderer.materials = new Material[1] { _meshRenderer.materials[0] };
                }
            }

            if (IsStun()) {
                _stunCountdown = Mathf.Max(_stunCountdown - Time.deltaTime, 0);
                if (_stunCountdown <= 0) {
                    Destroy(_stunFX);
                }
            }


            if (_controller == null)
                return;

            InputData inputData = _controller.UpdateInput(this.view, GameManager.Instance.GetGameData());
            _hasShot = false;
            _hasFiredShockwave = false;
            _hasDroppedMine = false;
            _thrust = 0.0f;
            if (!IsStun()) {
                _thrust = Mathf.Clamp01(inputData.thrust);
                _orientationTarget = Mathf.Repeat(inputData.targetOrientation, 360.0f);
                _orientationTarget = Mathf.Repeat(inputData.targetOrientation, 360.0f);

                if (inputData.shoot) {
                    Shoot();
                }
                if (inputData.dropMine) {
                    DropMine();
                }
                if (inputData.fireShockwave) {
                    FireShockwave();
                }
            }
            _energy = Mathf.Clamp01(_energy + _energyPerSecond * Time.deltaTime * Mathf.Lerp(1, 1 - _thrustConsumption, _thrust));

            ParticleSystem.EmissionModule emission = _thrustFX.emission;
            emission.enabled = _thrust > 0;
        }

        private void LateUpdate() {
            willCheckRigidbody = true;
            previousPosition = _rigidbody.position;
            previousRotation = _rigidbody.rotation;
        }


        private void FixedUpdate() {
            if (IsStun())
                return;
            /*if(!CheckRigidbody())
				return;*/
            float speedMax = _speedMax * (IsHit() ? _hitSpeedFactor : 1.0f);
            Vector2 direction = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
            _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity + direction * _thrust * _speedForThrust * Time.fixedDeltaTime, speedMax);
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, _orientationTarget)) > Mathf.Epsilon) {
                float deltaRotation = _rotationSpeed * Time.fixedDeltaTime;
                if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, _orientationTarget)) < deltaRotation) {
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, _orientationTarget);
                } else {
                    transform.eulerAngles += new Vector3(0.0f, 0.0f, deltaRotation * Mathf.Sign(Mathf.DeltaAngle(transform.eulerAngles.z, _orientationTarget)));
                }
            }
        }

        private bool CheckRigidbody() {
            if (!willCheckRigidbody)
                return true;

            if (_rigidbody.position != previousPosition) {
                Debug.LogError("GameObject position was changed elsewhere. Be sure not to change position directly in your controller.");
                return false;
            }
            if (_rigidbody.rotation != previousRotation) {
                Debug.LogError("GameObject rotation was changed elsewhere. Be sure not to change rotation directly in your controller.");
                return false;
            }
            return true;
        }

        public void Initialize(BaseSpaceShipController controller, int owner) {
            _controller = controller;
            _owner = owner;
        }

        public void DropMine() {
            if (_energy < _mineEnergyCost)
                return;
            GameObject.Instantiate(minePrefab, transform.position, Quaternion.identity);
            _energy -= _mineEnergyCost;
            dropMineAudio?.Play();
            _hasDroppedMine = true;
        }

        public void Shoot() {
            if (_energy < _shootEnergyCost)
                return;
            Bullet spawned = GameObject.Instantiate<Bullet>(bulletPrefab, transform.position, transform.rotation);
            spawned.SetOwner(_owner);
            _energy -= _shootEnergyCost;
            shootAudio?.Play();
            _hasShot = true;

        }

        public void FireShockwave() {
            if (_energy < _shockwaveEnergyCost)
                return;
            Shockwave spawned = GameObject.Instantiate<Shockwave>(shockwavePrefab, transform.position, transform.rotation);
            spawned.SetOwner(_owner);
            _energy -= _shockwaveEnergyCost;
            fireShockwaveAudio?.Play();
            _hasFiredShockwave = true;
        }

        public void OnHitMine(Mine mine) {
            if (!mine.IsHitting())
                return;
            Hit();
        }

        public void OnHitBullet(Bullet bullet) {
            if (!bullet.IsHitting())
                return;
            Hit();
        }

        public void OnApplyShockwave(Shockwave shockwave) {
            Stun();
        }

        private void Stun() {
            if (!IsStun()) {
                _stunFX = GameObject.Instantiate<GameObject>(stunFXPrefab, transform, false);
            }
            _stunCountdown = _stunDuration;
            stunAudio?.Play();
        }

        private void Hit() {
            if (!IsHit()) {
                _hitCount++;
            }

            hitAudio.Play();
            _hitCountdown = _hitDuration;
            _meshRenderer.materials = new Material[2] { _meshRenderer.materials[0], hitMaterial };
        }

        public bool IsHit() {
            return _hitCountdown > 0.0f;
        }

        public bool IsStun() {
            return _stunCountdown > 0.0f;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            shockAudio?.Play();
        }

        public Color GetColor() {
            return color;
        }
    }

}