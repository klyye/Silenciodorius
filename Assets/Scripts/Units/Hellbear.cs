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
        ///     What the Hellbear currently doing.
        /// </summary>
        private State _currState;

        /// <summary>
        ///     How far away should an enemy be in order to pull this unit's aggro?
        /// </summary>
        public float aggroRange;

        /// <summary>
        ///     How far away should an enemy be in order to attack?
        /// </summary>
        public float attackRange;

        protected override void Start()
        {
            base.Start();
            _currState = State.Idle;
        }

        protected override void Update()
        {
            base.Update();
            var playerTransform = GameManager.player.transform;
            var dist = Vector2.Distance(transform.position, playerTransform.position);
            if (dist < attackRange) _currState = State.Attacking;
            else if (dist < aggroRange) _currState = State.Chasing;
            else _currState = State.Idle;

            switch (_currState)
            {
                case State.Attacking:
                    MainhandAttack(playerTransform.position);
                    break;
                case State.Chasing:
                    _controller.Direction = playerTransform.position - transform.position;
                    break;
                case State.Idle:
                    break;
                default:
                    throw GameException.Error("Wtf? How did you get here???");
            }
        }

        /// <summary>
        ///     All possible states that a Hellbear can be in.
        /// </summary>
        private enum State
        {
            Idle,
            Attacking,
            Chasing
        }
    }
}