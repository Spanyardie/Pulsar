using Urho;

namespace Pulsar.Helpers
{
    public static class MatrixHelper
    {
        public static Quaternion RotateAroundAxisX(Vector3 entityRotation)
        {
            Quaternion rotationX = new Quaternion();
            Matrix3 matrix;

            //Matrix for rotation about X
            //Rx = { 1     0     0  }
            //     { 0    cos   sin }
            //     { 0   -sin   cos }
            //matrix.R0C0 = 1;
            //matrix.R0C1 = 0;
            //matrix.R0C2 = 0;
            //matrix.R1C0 = 0;
            //matrix.R1C1 = (float)Math.Cos(Rx);
            //matrix.R1C2 = (float)Math.Sin(Rx);
            //matrix.R2C0 = 0;
            //matrix.R2C1 = -((float)Math.Sin(Rx));
            //matrix.R2C2 = (float)Math.Cos(Rx);

            //rotationX = matrix.ToQuaternion();

            return rotationX;
        }

        public static Quaternion RotateAroundAxisY(Vector3 entityRotation)
        {
            Quaternion rotationY = new Quaternion();
            Matrix3 matrix;

            //Matrix for rotation about Y
            //Rx = { cos   0   -sin }
            //     {  0    1     0  }
            //     { sin   0    cos }
            //matrix.R0C0 = (float)Math.Cos(Ry);
            //matrix.R0C1 = 0;
            //matrix.R0C2 = -((float)Math.Sin(Ry));
            //matrix.R1C0 = 0;
            //matrix.R1C1 = 1;
            //matrix.R1C2 = 0;
            //matrix.R2C0 = (float)Math.Sin(Ry);
            //matrix.R2C1 = 0;
            //matrix.R2C2 = (float)Math.Cos(Ry);

            //rotationY = matrix.ToQuaternion();

            return rotationY;
        }

        public static Quaternion RotateAroundAxisZ(Vector3 entityRotation)
        {
            Quaternion rotationZ = new Quaternion();
            Matrix3 matrix;

            //Matrix for rotation about Z
            //Rx = { cos   sin  0 }
            //     { -sin  cos  0 }
            //     {   0    0   1 }
            //matrix.R0C0 = (float)Math.Cos(Rz);
            //matrix.R0C1 = (float)Math.Sin(Rz);
            //matrix.R0C2 = 0;
            //matrix.R1C0 = -((float)Math.Sin(Rz));
            //matrix.R1C1 = (float)Math.Cos(Rz);
            //matrix.R1C2 = 0;
            //matrix.R2C0 = 0;
            //matrix.R2C1 = 0;
            //matrix.R2C2 = 1;

            //rotationZ = matrix.ToQuaternion();

            return rotationZ;
        }
    }
}
