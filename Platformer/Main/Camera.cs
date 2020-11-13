using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Main
{
    public class Camera
    {
        public Matrix transform;

        public Vector3 cameraPosition = new Vector3(0, 0, 0);
        
        public void Update()
        {
            transform = Matrix.CreateScale(1);
            transform = Matrix.CreateTranslation(cameraPosition);
        }
    }
}
