using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceAnt
{
    interface IAnt
    {
        void Move();

        void RunToBase();

        void DoWork();
    }

    class Ant : IAnt
    {
        protected Random random = new Random();
        protected coordinates coordinate;
        protected string job;

        public Ant(coordinates coordinate, string job)
        {
            this.coordinate = coordinate;
            this.job = job;
        }

        public virtual void Move()
        {
            int dir = random.Next(0, 4);
            int step = random.Next(1, 10);
            for (int i = 0; i < step; i++)
            {
                switch (dir)
                {
                    case 0:
                        coordinate.X += 1;
                        break;
                    case 1:
                        coordinate.Y += 1;
                        break;
                    case 2:
                        coordinate.Y += 1;
                        break;
                    case 3:
                        coordinate.X += 1;
                        break;
                }
            }
        }
        public virtual void RunToBase() { }
        public virtual void DoWork() { }


        public virtual coordinates Cor
        {
            get { return coordinate; }
        }
        public virtual string Job
        {
            get { return job; }
        }
    }
}
