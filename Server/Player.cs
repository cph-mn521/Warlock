using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;


namespace GameServer
{
    public class Player
    {
        public int id;
        public string username;
        public Vector3 position;
        public Quaternion rotation;

        //TODO: Add a list with a players spells and cooldowns.
    
        
        

        public SpellType [] spells = new SpellType[6];
        public double [] cooldowns = new double[6];
        public DateTime[] LastCast = new DateTime[6];

        public int [] spellRank = new int[6];
        //
        private float drag = 0.9f;
        private Vector3 inputs;
        private Vector3 Velocity;

        private float moveSpeed = 5f / Constants.TICKS_PR_SEC;
        public Player(int _id,string _username,Vector3 _pos){
            id =_id;
            username=_username;
            position=_pos;
            rotation = Quaternion.Identity;       
            inputs = new Vector3();    
            spells[0] = SpellType.Fireball;
            cooldowns[0] = 2000;
            LastCast[0] = DateTime.Now;
            spellRank[0]=1;
        }

        public void Update(){            
            Move();
            addDrag();
        }

        private void Move(){
            position += inputs*moveSpeed;
            position +=Velocity;
            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        private void addDrag(){
            if(Velocity.Length() < 0.5f){
                Velocity = new Vector3(0,0,0);
            }else{
                Velocity = Velocity*drag;
            }
        }
        public Vector3 getPosition(){
            return this.position;
        }
        public void SetInput(Vector3 _inputs, Quaternion _rotation){
            inputs =_inputs;
            rotation=_rotation;
        }

        public void addVelocity(Vector3 _velocity){
            Velocity += _velocity;
        }
        public void setVelocity(Vector3 _velocity){
            Velocity = _velocity;
        }

    }
}