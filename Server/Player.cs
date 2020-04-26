using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Server;

namespace GameServer {
    public class Player : updatable {
        #region Interfaces
        private int MyId;
        public int id {
            get { return MyId; }
            set { MyId = value; }
        }

        public int gold=100;

        private Vector3 MyPosition;
        public Vector3 position{
            get{return MyPosition;}
            set{MyPosition = value;}
        }
        private Quaternion MyRotation;
        public Quaternion rotation{
            get{return MyRotation;}
            set{MyRotation = value;}
        }
        public int type{
            get{return 1;}
        }
        #endregion

        public string username;

        //TODO: Add a list with a players spells and cooldowns.
        public SpellBook spellBook;


        public bool removed;

        public SpellType[] spells = new SpellType[6];
        public double[] cooldowns = new double[6];
        public DateTime[] LastCast = new DateTime[6];
        private float maxHp;
        private float currentHP;
        public int[] spellRank = new int[6];
        //
        private float drag = 0.95f;
        private Vector3 inputs;
        private Vector3 Velocity;

        private float moveSpeed = 5f / Constants.TICKS_PR_SEC;


        public Player (int _id, string _username, Vector3 _pos) {
            id = _id;
            username = _username;
            position = _pos;
            rotation = Quaternion.Identity;
            inputs = new Vector3 ();
            spells[0] = SpellType.Fireball;
            spells[1] = SpellType.Teleport;
            cooldowns[0] = 2000;
            cooldowns[1] = 4000;
            LastCast[0] = DateTime.Now;
            LastCast[1] = DateTime.Now;
            spellRank[0] = 1;
            spellRank[1] = 1;
            maxHp = 100;
            currentHP = 100;
            removed = false;
            spellBook = new SpellBook(id);
        }

        public void Update () {
            if (!removed) {
                Move ();
                addDrag ();
                if (position.Length () > Program.game.mapSize) {
                    dmg (0.5f);
                }
            }
            ServerSend.Instance.updateObject(this);

        }
        private void Move () {
            position += inputs * moveSpeed;
            position += Velocity;
        }

        private void addDrag () {
            if (Velocity.Length () < 0.01f) {
                Velocity = new Vector3 (0, 0, 0);
            } else {
                Velocity = Velocity * drag;
            }
        }
        public Vector3 getPosition () {
            return this.position;
        }
        public void SetInput (Vector3 _inputs, Quaternion _rotation) {
            inputs = _inputs;
            rotation = _rotation;
        }

        public void addVelocity (Vector3 _velocity) {
            Velocity += _velocity * HpScale ();
        }
        public void setVelocity (Vector3 _velocity) {
            Velocity = _velocity * HpScale ();
        }
        public void dmg (float amount) {
            currentHP -= amount;
            if (currentHP <= 0 && !removed && Program.game.round) {
                Server.cleanUp.Add(this);
                removed = true;
                Program.game.deadPlayers += 1;
            }
        }
        public void changeMaxHp (float amount) {
            maxHp += amount;
        }

        private float HpScale () {
            return 1 + (1 - currentHP / maxHp);
        }

        public void resetHp () {
            currentHP = maxHp;
        }

        public float getHp () {
            return this.currentHP;
        }

    }
}