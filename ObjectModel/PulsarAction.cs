using Pulsar.Helpers;
using Pulsar.ObjectModel.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Urho;
using Urho.Actions;
using static Pulsar.ObjectModel.PulsarAction;

namespace Pulsar.ObjectModel
{
    public class PulsarAction
    {
        public enum ActionType
        {
            Single = 0,
            SingleRequiresAdditionalAction,
            SingleRequiresAdditionalActions,
            Sequence,
            Parallel,
            Unset
        }

        public ActionType Type { get; set; }

        [Flags]
        public enum ActionTypes
        {
            //Tween = 0,
            BezierBy = 0,
            BezierTo,
            Blink,
            DelayTime,
            EaseBackIn,
            EaseBackInOut,
            EaseBackOut,
            EaseBounceIn,
            EaseBounceInOut,
            EaseBounceOut,
            EaseElastic,
            EaseElasticIn,
            EaseElasticInOut,
            EaseElasticOut,
            EaseExponentialIn,
            EaseExponentialInOut,
            EaseExponentialOut,
            EaseIn,
            EaseInOut,
            EaseOut,
            EaseSineIn,
            EaseSineInOut,
            EaseSineOut,
            FadeIn,
            FadeOut,
            FadeTo,
            JumpBy,
            JumpTo,
            MoveBy,
            MoveTo,
            Parallel,
            Place,
            Repeat,
            RepeatForever,
            RotateAroundBy,
            RotateBy,
            RotateTo,
            ScaleBy,
            ScaleTo,
            Sequence,
            Hide,
            Show,
            Spawn,
            TintBy,
            TintTo,
            ToggleVisibility,
            TargettedAction,
            Amplitude,
            LastItem = 47
        }
        public ActionTypes PulsarActionType { get; set; }

        public enum ActionDataTypes
        {
            Float = 0,
            String,
            Int,
            UInt,
            Vector3,
            PulsarVector4RGBA,
            BezierConfig,
            TransformSpace,
            Node,
            Color,
            Action,
            Null
        }

        public bool IsDone { get; set; }
        public bool IsRunning { get; set; }

        [Action(ActionTypes.Amplitude, ActionDataTypes.Float)]
        public float Amplitude { get; set; }

        [Action(ActionTypes.EaseElastic, ActionDataTypes.Float)]
        [Action(ActionTypes.EaseElasticIn, ActionDataTypes.Float)]
        [Action(ActionTypes.EaseElasticInOut, ActionDataTypes.Float)]
        [Action(ActionTypes.EaseElasticOut, ActionDataTypes.Float)]
        public float Period { get; set; }

        [Action(ActionTypes.EaseIn, ActionDataTypes.Float)]
        [Action(ActionTypes.EaseInOut, ActionDataTypes.Float)]
        [Action(ActionTypes.EaseOut, ActionDataTypes.Float)]
        public float Rate { get; set; }

        [Action(ActionTypes.FadeTo, ActionDataTypes.Float)]
        public float Opacity { get; set; }

        [Action(ActionTypes.FadeIn, ActionDataTypes.Float)]
        [Action(ActionTypes.FadeOut, ActionDataTypes.Float)]
        [Action(ActionTypes.FadeTo, ActionDataTypes.Float)]
        [Action(ActionTypes.JumpBy, ActionDataTypes.Float)]
        [Action(ActionTypes.JumpTo, ActionDataTypes.Float)]
        [Action(ActionTypes.MoveBy, ActionDataTypes.Float)]
        [Action(ActionTypes.MoveTo, ActionDataTypes.Float)]
        [Action(ActionTypes.RotateAroundBy, ActionDataTypes.Float)]
        [Action(ActionTypes.RotateBy, ActionDataTypes.Float)]
        [Action(ActionTypes.RotateTo, ActionDataTypes.Float)]
        [Action(ActionTypes.ScaleBy, ActionDataTypes.Float)]
        [Action(ActionTypes.ScaleTo, ActionDataTypes.Float)]
        [Action(ActionTypes.TintBy, ActionDataTypes.Float)]
        [Action(ActionTypes.TintTo, ActionDataTypes.Float)]
        [Action(ActionTypes.BezierBy, ActionDataTypes.Float)]
        [Action(ActionTypes.BezierTo, ActionDataTypes.Float)]
        public float Duration { get; set; }

        //[Action(ActionTypes.Tween, ActionDataTypes.Float)]
        //public float From { get; set; }

        //[Action(ActionTypes.Tween, ActionDataTypes.String)]
        //public string Key { get; set; }

        //[Action(ActionTypes.Tween, ActionDataTypes.Float)]
        //public float To { get; set; }

        //[Action(ActionTypes.Tween, ActionDataTypes.Action)]
        //public Action<float, string> TweenAction { get; set; }

        [Action(ActionTypes.BezierBy, ActionDataTypes.BezierConfig)]
        [Action(ActionTypes.BezierTo, ActionDataTypes.BezierConfig)]
        public BezierConfig BezierConfig { get; set; }

        [Action(ActionTypes.Blink, ActionDataTypes.UInt)]
        [Action(ActionTypes.Repeat, ActionDataTypes.UInt)]
        public uint NumberOfTimes { get; set; }

        [Action(ActionTypes.DelayTime, ActionDataTypes.Float)]
        public float DelayTime { get; set; }

        [Action(ActionTypes.JumpBy, ActionDataTypes.Vector3)]
        [Action(ActionTypes.JumpTo, ActionDataTypes.Vector3)]
        [Action(ActionTypes.MoveBy, ActionDataTypes.Vector3)]
        [Action(ActionTypes.MoveTo, ActionDataTypes.Vector3)]
        [Action(ActionTypes.Place, ActionDataTypes.Vector3)]
        public Vector3 Position { get; set; }

        [Action(ActionTypes.JumpBy, ActionDataTypes.Float)]
        [Action(ActionTypes.JumpTo, ActionDataTypes.Float)]
        public float Height { get; set; }

        [Action(ActionTypes.JumpBy, ActionDataTypes.UInt)]
        [Action(ActionTypes.JumpTo, ActionDataTypes.UInt)]
        public uint Jumps { get; set; }

        [Action(ActionTypes.RotateAroundBy, ActionDataTypes.Vector3)]
        public Vector3 Point { get; set; }

        [Action(ActionTypes.RotateAroundBy, ActionDataTypes.Vector3)]
        [Action(ActionTypes.RotateBy, ActionDataTypes.Vector3)]
        [Action(ActionTypes.RotateTo, ActionDataTypes.Vector3)]
        public Vector3 DeltaXYZ { get; set; }

        [Action(ActionTypes.TintBy, ActionDataTypes.PulsarVector4RGBA)]
        [Action(ActionTypes.TintTo, ActionDataTypes.PulsarVector4RGBA)]
        public PulsarVector4RGBA DeltaRGBA { get; set; }

        [Action(ActionTypes.RotateAroundBy, ActionDataTypes.TransformSpace)]
        public TransformSpace TransformSpace { get; set; }

        [Action(ActionTypes.ScaleBy, ActionDataTypes.Vector3)]
        [Action(ActionTypes.ScaleTo, ActionDataTypes.Vector3)]
        public Vector3 Scale { get; set; }

        [Action(ActionTypes.TargettedAction, ActionDataTypes.Node)]
        public Node Target { get; set; }

        [Action(ActionTypes.TintBy, ActionDataTypes.Int)]
        [Action(ActionTypes.TintTo, ActionDataTypes.Int)]
        public int MaterialIndex { get; set; }

        [Action(ActionTypes.TintBy, ActionDataTypes.String)]
        [Action(ActionTypes.TintTo, ActionDataTypes.String)]
        public string ShaderParameterName { get; set; }
        
        [Action(ActionTypes.TintTo, ActionDataTypes.Color)]
        public Color ColorTo { get; set; }

        private readonly List<PulsarAction> _actionSet = new List<PulsarAction>();
        public void AddActionToSet(PulsarAction action) 
        {
            _actionSet.Add(action);
        }
        public void AddActionsToSet(List<PulsarAction> actions)
        {
            foreach(PulsarAction action in actions)
            {
                _actionSet.Add(action);
            }
        }
        public List<PulsarAction> GetActionSet() { return _actionSet; }

        public FiniteTimeAction[] GetFiniteActionSet()
        {
            FiniteTimeAction[] actions = Array.Empty<FiniteTimeAction>();

            foreach(PulsarAction action in _actionSet)
            {
                Array.Resize(ref actions, actions.Length + 1);
                actions[actions.Length - 1] = action._action;
            }
            return actions;
        }

        private readonly Node _node;
        public Node Node 
        {
            get
            {
                return _node;
            }
        }

        private FiniteTimeAction _action;

        public FiniteTimeAction GetAction() { return _action; }
        public void AddAction(FiniteTimeAction action)
        {
            _action = action;
        }

        private ActionState _actionState;
        public ActionState GetActionState() { return _actionState; }

        private string _id;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private void GenerateID()
        {
            //generate an ID from the current tick 
            _id = DateTime.Now.Ticks.ToString();
        }

        public bool RetainAction { get; set; }

        public PulsarAction(Node node, bool retainAction)
        {
            _node = node;
            _actionSet = new List<PulsarAction>();
            GenerateID();
            RetainAction = retainAction;
        }

        public async void StartAction()
        {
            IsRunning = true;
            _actionState = await _node.RunActionsAsync(_action);
            //Debug.Print("Completed action - " + PulsarActionType.ToString());
            IsDone = true;
            IsRunning = false;
        }

        //public ActionTween InitActionTween()
        //{
        //    _action = new ActionTween(Duration, Key, From, To, TweenAction);
        //    return (ActionTween)_action;
        //}

        public BezierBy InitBezierBy()
        {
            _action = new BezierBy(Duration, BezierConfig);
            return (BezierBy)_action;
        }

        public BezierTo InitBezierTo()
        {
            _action = new BezierTo(Duration, BezierConfig);
            return (BezierTo)_action;
        }

        public Blink InitBlink()
        {
            _action = new Blink(Duration, NumberOfTimes);
            return (Blink)_action;
        }

        public DelayTime InitDelayTime()
        {
            _action = new DelayTime(Duration);
            return (DelayTime)_action;
        }

        public EaseBackIn InitEaseBackIn()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseBackIn(GetFiniteActionSet()[0]);
                return (EaseBackIn)_action;
            }

            return null;
        }

        public EaseBackInOut InitEaseBackInOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseBackInOut(GetFiniteActionSet()[0]);
                return (EaseBackInOut)_action;
            }

            return null;
        }

        public EaseBackOut InitEaseBackOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseBackOut(GetFiniteActionSet()[0]);
                return (EaseBackOut)_action;
            }

            return null;
        }

        public EaseBounceIn InitEaseBounceIn()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseBounceIn(GetFiniteActionSet()[0]);
                return (EaseBounceIn)_action;
            }

            return null;
        }

        public EaseBounceInOut InitEaseBounceInOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseBounceInOut(GetFiniteActionSet()[0]);
                return (EaseBounceInOut)_action;
            }

            return null;
        }

        public EaseBounceOut InitEaseBounceOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseBounceOut(GetFiniteActionSet()[0]);
                return (EaseBounceOut)_action;
            }

            return null;
        }

        public EaseElastic InitEaseElastic()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseElastic(GetFiniteActionSet()[0], Period);
                return (EaseElastic)_action;
            }

            return null;
        }

        public EaseElasticIn InitEaseElasticIn()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseElasticIn(GetFiniteActionSet()[0], Period);
                return (EaseElasticIn)_action;
            }

            return null;
        }

        public EaseElasticInOut InitEaseElasticInOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseElasticInOut(GetFiniteActionSet()[0], Period);
                return (EaseElasticInOut)_action;
            }

            return null;
        }

        public EaseElasticOut InitEaseElasticOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseElasticOut(GetFiniteActionSet()[0], Period);
                return (EaseElasticOut)_action;
            }

            return null;
        }

        public EaseExponentialIn InitEaseExponentialIn()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseExponentialIn(GetFiniteActionSet()[0]);
                return (EaseExponentialIn)_action;
            }

            return null;
        }

        public EaseExponentialInOut InitEaseExponentialInOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseExponentialInOut(GetFiniteActionSet()[0]);
                return (EaseExponentialInOut)_action;
            }

            return null;
        }

        public EaseExponentialOut InitEaseExponentialOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseExponentialOut(GetFiniteActionSet()[0]);
                return (EaseExponentialOut)_action;
            }

            return null;
        }

        public EaseIn InitEaseIn()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseIn(GetFiniteActionSet()[0], Rate);
                return (EaseIn)_action;
            }

            return null;
        }

        private void CreateFiniteActionsFromSet()
        {
            foreach(PulsarAction action in _actionSet)
            {
                action.Initialise();
            }
        }

        public EaseInOut InitEaseInOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseInOut(GetFiniteActionSet()[0], Rate);
                return (EaseInOut)_action;
            }

            return null;
        }

        public EaseOut InitEaseOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseOut(GetFiniteActionSet()[0], Rate);
                return (EaseOut)_action;
            }

            return null;
        }

        public EaseSineIn InitSineIn()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseSineIn(GetFiniteActionSet()[0]);
                return (EaseSineIn)_action;
            }

            return null;
        }

        public EaseSineInOut InitSineInOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseSineInOut(GetFiniteActionSet()[0]);
                return (EaseSineInOut)_action;
            }

            return null;
        }

        public EaseSineOut InitSineOut()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new EaseSineOut(GetFiniteActionSet()[0]);
                return (EaseSineOut)_action;
            }

            return null;
        }

        public FadeIn InitFadeIn()
        {
            _action = new FadeIn(Duration);
            return (FadeIn)_action;
        }

        public FadeOut InitFadeOut()
        {
            _action = new FadeOut(Duration);
            return (FadeOut)_action;
        }

        public FadeTo InitFadeTo()
        {
            _action = new FadeTo(Duration, Opacity);
            return (FadeTo)_action;
        }

        public Hide InitHide()
        {
            _action = new Hide();
            return (Hide)_action;
        }

        public JumpBy InitJumpBy()
        {
            _action = new JumpBy(Duration, Position, Height, Jumps);
            return (JumpBy)_action;
        }

        public JumpTo InitJumpTo()
        {
            _action = new JumpTo(Duration, Position, Height, Jumps);
            return (JumpTo)_action;
        }

        public MoveBy InitMoveBy()
        {
            _action = new MoveBy(Duration, Position);
            return (MoveBy)_action;
        }

        public MoveTo InitMoveTo()
        {
            _action = new MoveTo(Duration, Position);
            return (MoveTo)_action;
        }

        public Place InitPlace()
        {
            _action = new Place(Position);
            return (Place)_action;
        }

        public Repeat InitRepeat()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new Repeat(GetFiniteActionSet()[0], NumberOfTimes);
                return (Repeat)_action;
            }

            return null;
        }

        public Sequence InitSequence()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                var actionSet = GetFiniteActionSet();
                _action = new Sequence(actionSet);
                return (Sequence)_action;
            }

            return null;
        }

        public Parallel InitParallel()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                var actionSet = GetFiniteActionSet();
                _action = new Parallel(actionSet);
                return (Parallel)_action;
            }

            return null;
        }

        public RepeatForever InitRepeatForever()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new RepeatForever(GetFiniteActionSet()[0]);
                return (RepeatForever)_action;
            }

            return null;
        }

        public ReverseTime InitReverseTime()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new ReverseTime(GetFiniteActionSet()[0]);
                return (ReverseTime)_action;
            }

            return null;
        }

        public RotateAroundBy InitRotateAroundBy()
        {
            _action = new RotateAroundBy(Duration, Point, DeltaXYZ.X, DeltaXYZ.Y, DeltaXYZ.Z, TransformSpace);
            return (RotateAroundBy)_action;
        }

        public RotateBy InitRotateBy()
        {
            _action = new RotateBy(Duration, DeltaXYZ.X, DeltaXYZ.Y, DeltaXYZ.Z);
            return (RotateBy)_action;
        }

        public RotateTo InitRotateTo()
        {
            _action = new RotateTo(Duration, DeltaXYZ.X, DeltaXYZ.Y, DeltaXYZ.Z);
            return (RotateTo)_action;
        }

        public ScaleBy InitScaleBy()
        {
            _action = new ScaleBy(Duration, Scale.X, Scale.Y, Scale.Z);
            return (ScaleBy)_action;
        }

        public ScaleTo InitScaleTo()
        {
            _action = new ScaleTo(Duration, Scale.X, Scale.Y, Scale.Z);
            return (ScaleTo)_action;
        }

        public Show InitShow()
        {
            _action = new Show();
            return (Show)_action;
        }

        public Spawn InitSpawn()
        {
            if (GetFiniteActionSet().Length > 0)
            {
                CreateFiniteActionsFromSet();
                _action = new Spawn(GetFiniteActionSet());
                return (Spawn)_action;
            }

            return null;
        }

        public TintBy InitTintBy()
        {
            _action = new TintBy(Duration, DeltaRGBA.R, DeltaRGBA.G, DeltaRGBA.B, DeltaRGBA.A);
            return (TintBy)_action;
        }

        public TintTo InitTintTo()
        {
            _action = new TintTo(Duration, DeltaRGBA.R, DeltaRGBA.G, DeltaRGBA.B, DeltaRGBA.A);
            return (TintTo)_action;
        }

        public ToggleVisibility InitToggleVisibility()
        {
            _action = new ToggleVisibility();
            return (ToggleVisibility)_action;
        }

        public static string GetActionTypeString(ActionTypes actionType)
        {
            string actionTypeString = string.Empty;

            switch(actionType)
            {
                //case ActionTypes.Tween:
                //    actionTypeString = "Tween";
                //    break;
                case ActionTypes.BezierBy:
                    actionTypeString = "Bezier By";
                    break;
                case ActionTypes.BezierTo:
                    actionTypeString = "Bezier To";
                    break;
                case ActionTypes.Blink:
                    actionTypeString = "Blink";
                    break;
                case ActionTypes.DelayTime:
                    actionTypeString = "Delay Time";
                    break;
                case ActionTypes.EaseBackIn:
                    actionTypeString = "Ease Back In";
                    break;
                case ActionTypes.EaseBackInOut:
                    actionTypeString = "Ease Back In Out";
                    break;
                case ActionTypes.EaseBackOut:
                    actionTypeString = "Ease Back Out";
                    break;
                case ActionTypes.EaseBounceIn:
                    actionTypeString = "Ease Bounce In";
                    break;
                case ActionTypes.EaseBounceInOut:
                    actionTypeString = "Ease Bounce In Out";
                    break;
                case ActionTypes.EaseBounceOut:
                    actionTypeString = "Ease Bounce Out";
                    break;
                case ActionTypes.EaseElastic:
                    actionTypeString = "Ease Elastic";
                    break;
                case ActionTypes.EaseElasticIn:
                    actionTypeString = "Ease Elastic In";
                    break;
                case ActionTypes.EaseElasticInOut:
                    actionTypeString = "Ease Elastic In Out";
                    break;
                case ActionTypes.EaseElasticOut:
                    actionTypeString = "Ease Elastic Out";
                    break;
                case ActionTypes.EaseExponentialIn:
                    actionTypeString = "Ease Exponential In";
                    break;
                case ActionTypes.EaseExponentialInOut:
                    actionTypeString = "Ease Exponential In Out";
                    break;
                case ActionTypes.EaseExponentialOut:
                    actionTypeString = "Ease Exponential Out";
                    break;
                case ActionTypes.EaseIn:
                    actionTypeString = "Ease In";
                    break;
                case ActionTypes.EaseInOut:
                    actionTypeString = "Ease In Out";
                    break;
                case ActionTypes.EaseOut:
                    actionTypeString = "Ease Out";
                    break;
                case ActionTypes.EaseSineIn:
                    actionTypeString = "Ease Sine In";
                    break;
                case ActionTypes.EaseSineInOut:
                    actionTypeString = "Ease Sine In Out";
                    break;
                case ActionTypes.EaseSineOut:
                    actionTypeString = "Ease Sine Out";
                    break;
                case ActionTypes.FadeIn:
                    actionTypeString = "Fade In";
                    break;
                case ActionTypes.FadeOut:
                    actionTypeString = "Fade Out";
                    break;
                case ActionTypes.FadeTo:
                    actionTypeString = "Fade To";
                    break;
                case ActionTypes.Hide:
                    actionTypeString = "Hide";
                    break;
                case ActionTypes.JumpBy:
                    actionTypeString = "Jump By";
                    break;
                case ActionTypes.JumpTo:
                    actionTypeString = "Jump To";
                    break;
                case ActionTypes.MoveBy:
                    actionTypeString = "Move By";
                    break;
                case ActionTypes.MoveTo:
                    actionTypeString = "Move To";
                    break;
                case ActionTypes.Place:
                    actionTypeString = "Place";
                    break;
                case ActionTypes.Repeat:
                    actionTypeString = "Repeat";
                    break;
                case ActionTypes.RepeatForever:
                    actionTypeString = "Repeat Forever";
                    break;
                case ActionTypes.RotateAroundBy:
                    actionTypeString = "Rotate Around By";
                    break;
                case ActionTypes.RotateBy:
                    actionTypeString = "Rotate By";
                    break;
                case ActionTypes.RotateTo:
                    actionTypeString = "Rotate To";
                    break;
                case ActionTypes.ScaleBy:
                    actionTypeString = "Scale By";
                    break;
                case ActionTypes.ScaleTo:
                    actionTypeString = "Scale To";
                    break;
                case ActionTypes.Show:
                    actionTypeString = "Show";
                    break;
                case ActionTypes.Spawn:
                    actionTypeString = "Spawn";
                    break;
                case ActionTypes.TintBy:
                    actionTypeString = "Tint By";
                    break;
                case ActionTypes.TintTo:
                    actionTypeString = "Tint To";
                    break;
                case ActionTypes.ToggleVisibility:
                    actionTypeString = "Toggle Visibility";
                    break;
            }

            return actionTypeString;
        }

        public static List<PulsarActionType> GetActionTypeList()
        {
            List<PulsarActionType> typeList = new List<PulsarActionType>();

            PulsarActionType actionType;

            for(int typeIndex = 0; typeIndex < (int)ActionTypes.LastItem; typeIndex++)
            {
                actionType = new PulsarActionType
                {
                    ActionType = (ActionTypes)typeIndex,
                    TypeString = GetActionTypeString((ActionTypes)typeIndex)
                };
                typeList.Add(actionType);
            }

            return typeList;
        }

        public FiniteTimeAction Initialise()
        {
            FiniteTimeAction finiteTimeAction = null;

            switch(PulsarActionType)
            {
                case ActionTypes.Amplitude:
                    //TODO:
                    //finiteTimeAction = InitAmplitude();
                    break;
                case ActionTypes.BezierBy:
                    finiteTimeAction = InitBezierBy();
                    break;
                case ActionTypes.BezierTo:
                    finiteTimeAction = InitBezierTo();
                    break;
                case ActionTypes.Blink:
                    finiteTimeAction = InitBlink();
                    break;
                case ActionTypes.DelayTime:
                    finiteTimeAction = InitDelayTime();
                    break;
                case ActionTypes.EaseBackIn:
                    finiteTimeAction = InitEaseBackIn();
                    break;
                case ActionTypes.EaseBackInOut:
                    finiteTimeAction = InitEaseBackInOut();
                    break;
                case ActionTypes.EaseBackOut:
                    finiteTimeAction = InitEaseBackOut();
                    break;
                case ActionTypes.EaseBounceIn:
                    finiteTimeAction = InitEaseBounceIn();
                    break;
                case ActionTypes.EaseBounceInOut:
                    finiteTimeAction = InitEaseBounceInOut();
                    break;
                case ActionTypes.EaseBounceOut:
                    finiteTimeAction = InitEaseBounceOut();
                    break;
                case ActionTypes.EaseElasticIn:
                    finiteTimeAction = InitEaseElasticIn();
                    break;
                case ActionTypes.EaseElasticInOut:
                    finiteTimeAction = InitEaseElasticInOut();
                    break;
                case ActionTypes.EaseElasticOut:
                    finiteTimeAction = InitEaseElasticOut();
                    break;
                case ActionTypes.EaseExponentialIn:
                    finiteTimeAction = InitEaseExponentialIn();
                    break;
                case ActionTypes.EaseExponentialInOut:
                    finiteTimeAction = InitEaseExponentialInOut();
                    break;
                case ActionTypes.EaseExponentialOut:
                    finiteTimeAction = InitEaseExponentialOut();
                    break;
                case ActionTypes.EaseIn:
                    finiteTimeAction = InitEaseIn();
                    break;
                case ActionTypes.EaseInOut:
                    finiteTimeAction = InitEaseInOut();
                    break;
                case ActionTypes.EaseOut:
                    finiteTimeAction = InitEaseOut();
                    break;
                case ActionTypes.EaseSineIn:
                    finiteTimeAction = InitSineIn();
                    break;
                case ActionTypes.EaseSineInOut:
                    finiteTimeAction = InitSineInOut();
                    break;
                case ActionTypes.EaseSineOut:
                    finiteTimeAction = InitSineOut();
                    break;
                case ActionTypes.FadeIn:
                    finiteTimeAction = InitFadeIn();
                    break;
                case ActionTypes.FadeOut:
                    finiteTimeAction = InitFadeOut();
                    break;
                case ActionTypes.FadeTo:
                    finiteTimeAction = InitFadeTo();
                    break;
                case ActionTypes.Hide:
                    finiteTimeAction = InitHide();
                    break;
                case ActionTypes.JumpBy:
                    finiteTimeAction = InitJumpBy();
                    break;
                case ActionTypes.JumpTo:
                    finiteTimeAction = InitJumpTo();
                    break;
                case ActionTypes.MoveBy:
                    finiteTimeAction = InitMoveBy();
                    break;
                case ActionTypes.MoveTo:
                    finiteTimeAction = InitMoveTo();
                    break;
                case ActionTypes.Parallel:
                    finiteTimeAction = InitParallel();
                    break;
                case ActionTypes.Place:
                    finiteTimeAction = InitPlace();
                    break;
                case ActionTypes.Repeat:
                    finiteTimeAction = InitRepeat();
                    break;
                case ActionTypes.RepeatForever:
                    finiteTimeAction = InitRepeatForever();
                    break;
                case ActionTypes.RotateAroundBy:
                    finiteTimeAction = InitRotateAroundBy();
                    break;
                case ActionTypes.RotateBy:
                    finiteTimeAction = InitRotateBy();
                    break;
                case ActionTypes.RotateTo:
                    finiteTimeAction = InitRotateTo();
                    break;
                case ActionTypes.ScaleBy:
                    finiteTimeAction = InitScaleBy();
                    break;
                case ActionTypes.ScaleTo:
                    finiteTimeAction = InitScaleTo();
                    break;
                case ActionTypes.Sequence:
                    finiteTimeAction = InitSequence();
                    break;
                case ActionTypes.Show:
                    finiteTimeAction = InitShow();
                    break;
                case ActionTypes.Spawn:
                    finiteTimeAction = InitSpawn();
                    break;
                case ActionTypes.TargettedAction:
                    //TODO:
                    //finiteTimeAction = InitTargettedAction();
                    break;
                case ActionTypes.TintBy:
                    finiteTimeAction = InitTintBy();
                    break;
                case ActionTypes.TintTo:
                    finiteTimeAction = InitTintTo();
                    break;
                case ActionTypes.ToggleVisibility:
                    finiteTimeAction = InitToggleVisibility();
                    break;
                //case ActionTypes.Tween:
                //    finiteTimeAction = InitActionTween();
                //    break;
            }

            return finiteTimeAction;
        }

        public void RemoveFromActionSet(string tag)
        {
            var action = _actionSet.Find(act => act.ID == tag);
            if(action != null)
            {
                _actionSet.Remove(action);
            }
        }
    }

    public class PulsarActionType
    {
        public ActionTypes ActionType { get; set; }
        public string TypeString { get; set; }
    }
}
