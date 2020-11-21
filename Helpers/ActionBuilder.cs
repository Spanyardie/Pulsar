using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Primitives;
using System.Windows.Forms;
using Urho;
using Urho.Actions;
using static Pulsar.ObjectModel.PulsarAction;

namespace Pulsar.Helpers
{
    public static class ActionBuilder
    {
        //    public static PulsarAction BuildRotateAroundBy(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //deltas
        //        //x
        //        float deltaAngleX = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.X;
        //        //y
        //        float deltaAngleY = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.Y;
        //        //z
        //        float deltaAngleZ = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.Z;

        //        Vector3 delta3 = new Vector3(deltaAngleX, deltaAngleY, deltaAngleZ);

        //        //point
        //        //x
        //        float pointX = ((PulsarActionVector3)controls.Find("groupPoint", true)[0]).Vector3.X;
        //        //y
        //        float pointY = ((PulsarActionVector3)controls.Find("groupPoint", true)[0]).Vector3.Y;
        //        //z
        //        float pointZ = ((PulsarActionVector3)controls.Find("groupPoint", true)[0]).Vector3.Z;

        //        Vector3 point3 = new Vector3(pointX, pointY, pointZ);

        //        //transform space
        //        TransformSpace transformSpace = ((PulsarActionTransformSpace)controls.Find("groupTransformSpace", true)[0]).TransFormSpace;

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            DeltaXYZ = delta3,
        //            Point = point3,
        //            TransformSpace = transformSpace,
        //            PulsarActionType = ActionTypes.RotateAroundBy
        //        };
        //        newAction.InitRotateAroundBy();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildRotateBy(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);
        //        //x
        //        float deltaAngleX = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.X;
        //        //y
        //        float deltaAngleY = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.Y;
        //        //z
        //        float deltaAngleZ = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(deltaAngleX, deltaAngleY, deltaAngleZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            DeltaXYZ = vector3,
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.RotateBy
        //        };
        //        newAction.InitRotateBy();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildRotateTo(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);
        //        //x
        //        float deltaAngleX = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.X;
        //        //y
        //        float deltaAngleY = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.Y;
        //        //z
        //        float deltaAngleZ = ((PulsarActionVector3)controls.Find("groupDeltaXYZ", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(deltaAngleX, deltaAngleY, deltaAngleZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            DeltaXYZ = vector3,
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.RotateTo
        //        };
        //        newAction.InitRotateTo();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildMoveBy(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);
        //        //x
        //        float positionX = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.X;
        //        //y
        //        float positionY = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Y;
        //        //z
        //        float positionZ = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(positionX, positionY, positionZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Position = vector3,
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.MoveBy
        //        };
        //        newAction.InitMoveBy();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildMoveTo(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);
        //        //x
        //        float positionX = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.X;
        //        //y
        //        float positionY = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Y;
        //        //z
        //        float positionZ = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(positionX, positionY, positionZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Position = vector3,
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.MoveTo
        //        };
        //        newAction.InitMoveTo();

        //        return newAction;
        //    }

        //    //Requires a shape component be attached to the node
        //    public static PulsarAction BuildFadeIn(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.FadeIn
        //        };
        //        newAction.InitFadeIn();

        //        return newAction;
        //    }

        //    //Requires a shape component be attached to the node
        //    public static PulsarAction BuildFadeOut(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.FadeOut
        //        };
        //        newAction.InitFadeOut();

        //        return newAction;
        //    }

        //    //Requires a shape component be attached to the node
        //    public static PulsarAction BuildFadeTo(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //opacity
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtOpacity", true)[0]).Value, out float opacity);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            Opacity = opacity,
        //            PulsarActionType = ActionTypes.FadeTo
        //        };
        //        newAction.InitFadeTo();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildJumpBy(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //height
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtHeight", true)[0]).Value, out float height);

        //        //jumps
        //        uint.TryParse(((PulsarActionProperty)controls.Find("txtJumps", true)[0]).Value, out uint jumps);

        //        //position
        //        //x
        //        float positionX = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.X;
        //        //y
        //        float positionY = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Y;
        //        //z
        //        float positionZ = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(positionX, positionY, positionZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            Height = height,
        //            Jumps = jumps,
        //            Position = vector3,
        //            PulsarActionType = ActionTypes.JumpBy
        //        };
        //        newAction.InitJumpBy();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildJumpTo(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //height
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtHeight", true)[0]).Value, out float height);

        //        //jumps
        //        uint.TryParse(((PulsarActionProperty)controls.Find("txtJumps", true)[0]).Value, out uint jumps);

        //        //position
        //        //x
        //        float positionX = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.X;
        //        //y
        //        float positionY = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Y;
        //        //z
        //        float positionZ = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(positionX, positionY, positionZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            Height = height,
        //            Jumps = jumps,
        //            Position = vector3,
        //            PulsarActionType = ActionTypes.JumpTo
        //        };
        //        newAction.InitJumpTo();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildPlace(Node node, Control.ControlCollection controls)
        //    {
        //        //position
        //        //x
        //        float positionX = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.X;
        //        //y
        //        float positionY = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Y;
        //        //z
        //        float positionZ = ((PulsarActionVector3)controls.Find("groupPosition", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(positionX, positionY, positionZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Position = vector3,
        //            PulsarActionType = ActionTypes.Place
        //        };
        //        newAction.InitPlace();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildScaleBy(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);
        //        //x
        //        float scaleX = ((PulsarActionVector3)controls.Find("groupScale", true)[0]).Vector3.X;
        //        //y
        //        float scaleY = ((PulsarActionVector3)controls.Find("groupScale", true)[0]).Vector3.Y;
        //        //z
        //        float scaleZ = ((PulsarActionVector3)controls.Find("groupScale", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(scaleX, scaleY, scaleZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Scale = vector3,
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.ScaleBy
        //        };
        //        newAction.InitScaleBy();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildScaleTo(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);
        //        //x
        //        float scaleX = ((PulsarActionVector3)controls.Find("groupScale", true)[0]).Vector3.X;
        //        //y
        //        float scaleY = ((PulsarActionVector3)controls.Find("groupScale", true)[0]).Vector3.Y;
        //        //z
        //        float scaleZ = ((PulsarActionVector3)controls.Find("groupScale", true)[0]).Vector3.Z;

        //        Vector3 vector3 = new Vector3(scaleX, scaleY, scaleZ);

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Scale = vector3,
        //            Duration = duration,
        //            PulsarActionType = ActionTypes.ScaleTo
        //        };
        //        newAction.InitScaleTo();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildTintBy(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //RGBA Vector4
        //        //x
        //        float tintR = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.R;
        //        //y
        //        float tintG = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.G;
        //        //z
        //        float tintB = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.B;

        //        float tintA = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.A;

        //        PulsarVector4RGBA vector4 = new PulsarVector4RGBA(tintR, tintG, tintB, tintA);

        //        //material index
        //        int.TryParse(((PulsarActionProperty)controls.Find("txtMaterialIndex", true)[0]).Value, out int materialIndex);

        //        //shader parameter name
        //        string shaderParameterName = ((PulsarActionProperty)controls.Find("txtShaderParameterName", true)[0]).Value;

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            DeltaRGBA = vector4,
        //            MaterialIndex = materialIndex,
        //            ShaderParameterName = shaderParameterName,
        //            PulsarActionType = ActionTypes.TintBy
        //        };
        //        newAction.InitTintBy();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildTintTo(Node node, Control.ControlCollection controls)
        //    {
        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //RGBA Vector4
        //        //x
        //        float tintR = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.R;
        //        //y
        //        float tintG = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.G;
        //        //z
        //        float tintB = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.B;

        //        float tintA = ((PulsarActionVector4)controls.Find("groupDeltaRGBA", true)[0]).Vector4.A;

        //        PulsarVector4RGBA vector4 = new PulsarVector4RGBA(tintR, tintG, tintB, tintA);

        //        //material index
        //        int.TryParse(((PulsarActionProperty)controls.Find("txtMaterialIndex", true)[0]).Value, out int materialIndex);

        //        //shader parameter name
        //        string shaderParameterName = ((PulsarActionProperty)controls.Find("txtShaderParameterName", true)[0]).Value;

        //        PulsarAction newAction = new PulsarAction(node, false)
        //        {
        //            Duration = duration,
        //            DeltaRGBA = vector4,
        //            MaterialIndex = materialIndex,
        //            ShaderParameterName = shaderParameterName,
        //            PulsarActionType = ActionTypes.TintTo
        //        };
        //        newAction.InitTintTo();

        //        return newAction;
        //    }

        //    public static PulsarAction BuildBezierBy(Node node, Control.ControlCollection controls)
        //    {
        //        PulsarAction newAction = null;

        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //BezierConfig
        //        var config = (PulsarActionBezierConfig)controls.Find("groupBezierConfig", true)[0];
        //        BezierConfig bezierConfig = new BezierConfig
        //        {
        //            ControlPoint1 = config.ControlPoint1,
        //            ControlPoint2 = config.ControlPoint2,
        //            EndPosition = config.EndPoint
        //        };

        //        if (config != null)
        //        {
        //            newAction = new PulsarAction(node, false)
        //            {
        //                Duration = duration,
        //                BezierConfig = bezierConfig,
        //                PulsarActionType = ActionTypes.BezierBy
        //            };
        //            newAction.InitBezierBy();
        //        }

        //        return newAction;
        //    }

        //    public static PulsarAction BuildBezierTo(Node node, Control.ControlCollection controls)
        //    {
        //        PulsarAction newAction = null;

        //        //duration
        //        float.TryParse(((PulsarActionProperty)controls.Find("txtDuration", true)[0]).Value, out float duration);

        //        //BezierConfig
        //        var config = (PulsarActionBezierConfig)controls.Find("groupBezierConfig", true)[0];
        //        BezierConfig bezierConfig = new BezierConfig
        //        {
        //            ControlPoint1 = config.ControlPoint1,
        //            ControlPoint2 = config.ControlPoint2,
        //            EndPosition = config.EndPoint
        //        };

        //        if (config != null)
        //        {
        //            newAction = new PulsarAction(node, false)
        //            {
        //                Duration = duration,
        //                BezierConfig = bezierConfig,
        //                PulsarActionType = ActionTypes.BezierTo
        //            };
        //            newAction.InitBezierTo();
        //        }

        //        return newAction;
        //    }
    }
}
