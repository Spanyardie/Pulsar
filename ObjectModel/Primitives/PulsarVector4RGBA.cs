using System;

namespace Pulsar.ObjectModel.Primitives
{
    public struct PulsarVector4RGBA : IEquatable<PulsarVector4RGBA>
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public PulsarVector4RGBA(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override bool Equals(object obj)
        {
            bool returnValue = false;
            if (obj != null)
            {
                PulsarVector4RGBA vec = (PulsarVector4RGBA)obj;
                    return Equals(vec);
            }

            return returnValue;
        }

        public bool Equals(PulsarVector4RGBA other)
        {
                return Equals(other);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(PulsarVector4RGBA left, PulsarVector4RGBA right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PulsarVector4RGBA left, PulsarVector4RGBA right)
        {
            return !(left == right);
        }

        //
        // Summary:
        //     Adds two instances.
        //
        // Parameters:
        //   left:
        //     The first instance.
        //
        //   right:
        //     The second instance.
        //
        // Returns:
        //     The result of the calculation.
        public static PulsarVector4RGBA operator +(PulsarVector4RGBA left, PulsarVector4RGBA right)
        {
            PulsarVector4RGBA vec4 = new PulsarVector4RGBA(left.R, left.G, left.B, left.A);
            vec4.R += right.R;
            vec4.G += right.G;
            vec4.B += right.B;
            vec4.A += right.A;

            return vec4;
        }
        //
        // Summary:
        //     Negates an instance.
        //
        // Parameters:
        //   vec:
        //     The instance.
        //
        // Returns:
        //     The result of the calculation.
        public static PulsarVector4RGBA operator -(PulsarVector4RGBA left, PulsarVector4RGBA right)
        {
            PulsarVector4RGBA vec4 = new PulsarVector4RGBA(left.R, left.G, left.B, left.A);

            vec4.R -= right.R;
            vec4.G -= right.G;
            vec4.B -= right.B;
            vec4.A -= right.A;

            return vec4;
        }
    }
}
