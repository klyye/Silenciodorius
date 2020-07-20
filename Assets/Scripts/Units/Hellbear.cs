using UnityEngine;

namespace Units
{
    /// <summary>
    ///     A basic creep enemy with a clap attack.
    ///     ʕ͡·ᴥ͡·ʔ You've been visited by Hellbear Smasher of fortune.
    ///     Clap your hands and copy paste this 5 times for EZ LIFE and EZ RARES ʕ͡·ᴥ͡·ʔ
    /// </summary>
    public class Hellbear : Unit
    {
        /// <summary>
        ///     All possible states that a Hellbear can be in.
        /// </summary>
        private enum State
        {
            Idle,
            Attacking,
            Chasing
        }

        /// <summary>
        ///     What the Hellbear currently doing.
        /// </summary>
        private State _currState;

        /// <summary>
        ///     The player's transform.
        /// </summary>
        private Transform _player;

        /// <summary>
        ///     How far away should an enemy be in order to pull this unit's aggro?
        /// </summary>
        public float aggroRange;

        /// <summary>
        ///    How far away should an enemy be in order to attack?
        /// </summary>
        public float attackRange;

        protected override void Start()
        {
            base.Start();
            _currState = State.Idle;
            _player = FindObjectOfType<Player>().transform;
            _mainhand = Instantiate(startingWeapon, transform);    //TODO: DRY (repeats with player)
        }

        private void Update()
        {
            var sqrDist = Vector2.SqrMagnitude(transform.position - _player.position);
            //TODO: is there a shorter way to write this?
            if (sqrDist < attackRange)
            {
                _currState = State.Chasing;
            }
            else if (sqrDist < aggroRange)
            {
                _currState = State.Attacking;
            }
            else
            {
                _currState = State.Idle;
            }

            switch (_currState)
            {
                case State.Attacking:
                    _mainhand.Attack(_player.position, true);
                    break;
                case State.Chasing:
                    _controller.Move(_player.position - transform.position);
                    break;
                case State.Idle:
                    break;
                default:
                    throw GameException.Error("Wtf? How did you get here???");
            }
        }
    }
}