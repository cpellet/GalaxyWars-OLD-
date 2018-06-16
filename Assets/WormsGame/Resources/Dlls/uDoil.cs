using System.Collections.Generic;
using UnityEngine;


namespace uDoil { 
    public static class uInputExtensions
    {
        private static Dictionary<string, bool> ButtonDownTimers { get; set; } = new Dictionary<string, bool>();

        public static bool GetButtonDown(this int controllerId, string button)
        {
            bool value = false;

            if (controllerId < 0 || controllerId > 3)
            {
                Debug.LogError($"Controller id {controllerId} is not allowed, min 0 max 3");
                return false;
            }

            if (Input.GetJoystickNames().Length == 0)
            {
                Debug.LogError("No controllers found");
                return false;
            }

            if(Input.GetJoystickNames().Length < controllerId + 1)
            {
                return false;
            }

            if (Input.GetJoystickNames()[controllerId] == "Wireless Controller")
            {
                switch (button)
                {
                    case "ACTION_LEFT":
                        value = Input.GetButtonDown($"P{controllerId}_00");
                        break;
                    case "ACTION_UP":
                        value = Input.GetButtonDown($"P{controllerId}_03");
                        break;
                    case "ACTION_RIGHT":
                        value = Input.GetButtonDown($"P{controllerId}_02");
                        break;
                    case "ACTION_DOWN":
                        value = Input.GetButtonDown($"P{controllerId}_01");
                        break;
                    case "R_BUTTON":
                        value = Input.GetButtonDown($"P{controllerId}_05");
                        break;
                    case "R_STICK":
                        value = Input.GetButtonDown($"P{controllerId}_11");
                        break;
                    case "R_SELECT":
                        value = Input.GetButtonDown($"P{controllerId}_09");
                        break;
                    case "L_BUTTON":
                        value = Input.GetButtonDown($"P{controllerId}_04");
                        break;
                    case "L_STICK":
                        value = Input.GetButtonDown($"P{controllerId}_10");
                        break;
                    case "L_SELECT":
                        value = Input.GetButtonDown($"P{controllerId}_08");
                        break;
                    case "L_TRIGGER":
                        if (ButtonDownTimers.ContainsKey($"P{controllerId}_04_AXIS"))
                        {
                            if ((Input.GetAxisRaw($"P{controllerId}_04_AXIS") + 1) /2 >= 0.3f && ButtonDownTimers[$"P{controllerId}_04_AXIS"] == false)
                            {
                                value = true;
                                ButtonDownTimers[$"P{controllerId}_04_AXIS"] = true;
                            }
                            else if ((Input.GetAxisRaw($"P{controllerId}_04_AXIS") + 1) /2 < 0.3f)
                            {
                                ButtonDownTimers[$"P{controllerId}_04_AXIS"] = false;
                            }
                        }
                        else
                        {
                            ButtonDownTimers.Add($"P{controllerId}_04_AXIS", false);
                        }
                        break;
                    case "R_TRIGGER":
                        if (ButtonDownTimers.ContainsKey($"P{controllerId}_05_AXIS"))
                        {
                            if ((Input.GetAxisRaw($"P{controllerId}_05_AXIS") + 1) /2 >= 0.3f && ButtonDownTimers[$"P{controllerId}_05_AXIS"] == false)
                            {
                                value = true;
                                ButtonDownTimers[$"P{controllerId}_05_AXIS"] = true;
                            }
                            else if ((Input.GetAxisRaw($"P{controllerId}_05_AXIS") + 1) /2 < 0.3f)
                            {
                                ButtonDownTimers[$"P{controllerId}_05_AXIS"] = false;
                            }
                        }
                        else
                        {
                            ButtonDownTimers.Add($"P{controllerId}_05_AXIS", false);
                        }
                        break;
                    default:
                        Debug.LogWarning($"UNKNOWN KEY {button}");
                        break;
                }
            }
            else if (Input.GetJoystickNames()[controllerId] == "Controller (Xbox One For Windows)" || Input.GetJoystickNames()[controllerId] == "Controller (Xbox 360 Wireless Receiver for Windows)")
            {
                switch (button)
                {
                    case "ACTION_LEFT":
                        value = Input.GetButtonDown($"P{controllerId}_02");
                        break;
                    case "ACTION_UP":
                        value = Input.GetButtonDown($"P{controllerId}_03");
                        break;
                    case "ACTION_RIGHT":
                        value = Input.GetButtonDown($"P{controllerId}_01");
                        break;
                    case "ACTION_DOWN":
                        value = Input.GetButtonDown($"P{controllerId}_00");
                        break;
                    case "R_BUTTON":
                        value = Input.GetButtonDown($"P{controllerId}_05");
                        break;
                    case "R_STICK":
                        value = Input.GetButtonDown($"P{controllerId}_09");
                        break;
                    case "R_SELECT":
                        value = Input.GetButtonDown($"P{controllerId}_07");
                        break;
                    case "L_BUTTON":
                        value = Input.GetButtonDown($"P{controllerId}_04");
                        break;
                    case "L_STICK":
                        value = Input.GetButtonDown($"P{controllerId}_08");
                        break;
                    case "L_SELECT":
                        value = Input.GetButtonDown($"P{controllerId}_06");
                        break;
                    case "L_TRIGGER":
                        if (ButtonDownTimers.ContainsKey($"P{controllerId}_09_AXIS"))
                        {
                            if (Input.GetAxisRaw($"P{controllerId}_09_AXIS") >= 0.3f && ButtonDownTimers[$"P{controllerId}_09_AXIS"] == false)
                            {
                                value = true;
                                ButtonDownTimers[$"P{controllerId}_09_AXIS"] = true;
                            }
                            else if (Input.GetAxisRaw($"P{controllerId}_09_AXIS") < 0.3f)
                            {
                                ButtonDownTimers[$"P{controllerId}_09_AXIS"] = false;
                            }
                        }
                        else
                        {
                            ButtonDownTimers.Add($"P{controllerId}_09_AXIS", false);
                        }
                        break;
                    case "R_TRIGGER":                        
                        if(ButtonDownTimers.ContainsKey($"P{controllerId}_10_AXIS"))
                        {
                            if (Input.GetAxisRaw($"P{controllerId}_10_AXIS") >= 0.3f && ButtonDownTimers[$"P{controllerId}_10_AXIS"] == false)
                            {
                                value = true;
                                ButtonDownTimers[$"P{controllerId}_10_AXIS"] = true;
                            }
                            else if(Input.GetAxisRaw($"P{controllerId}_10_AXIS") < 0.3f)
                            {
                                ButtonDownTimers[$"P{controllerId}_10_AXIS"] = false;
                            }
                        }
                        else
                        {
                            ButtonDownTimers.Add($"P{controllerId}_10_AXIS", false);
                        }
                        break;
                    default:
                        Debug.LogWarning($"UNKNOWN KEY {button}");
                        break;
                }
            }
            return value;
        }

        public static bool GetButton(this int controllerId, string button)
        {
            bool value = false;

            if (controllerId < 0 || controllerId > 3)
            {
                Debug.LogError($"Controller id {controllerId} is not allowed, min 0 max 3");
                return false;
            }

            if(Input.GetJoystickNames().Length == 0)
            {
                Debug.LogError("No controllers found");
                return false;
            }

            if (Input.GetJoystickNames().Length < controllerId + 1)
            {
                return false;
            }

            if (Input.GetJoystickNames()[controllerId] == "Wireless Controller")
            {
                switch (button)
                {
                    case "ACTION_LEFT":
                        value = Input.GetButton($"P{controllerId}_00");
                        break;
                    case "ACTION_UP":
                        value = Input.GetButton($"P{controllerId}_03");
                        break;
                    case "ACTION_RIGHT":
                        value = Input.GetButton($"P{controllerId}_02");
                        break;
                    case "ACTION_DOWN":
                        value = Input.GetButton($"P{controllerId}_01");
                        break;
                    case "R_BUTTON":
                        value = Input.GetButton($"P{controllerId}_05");
                        break;
                    case "R_STICK":
                        value = Input.GetButton($"P{controllerId}_11");
                        break;
                    case "R_SELECT":
                        value = Input.GetButton($"P{controllerId}_09");
                        break;
                    case "R_TRIGGER":
                        value = (Input.GetAxisRaw($"P{controllerId}_05_AXIS") + 1) /2 >= 0.4f;
                        break;
                    case "L_BUTTON":
                        value = Input.GetButton($"P{controllerId}_04");
                        break;
                    case "L_STICK":
                        value = Input.GetButton($"P{controllerId}_10");
                        break;
                    case "L_SELECT":
                        value = Input.GetButton($"P{controllerId}_08");
                        break;
                    case "L_TRIGGER":
                        value = (Input.GetAxisRaw($"P{controllerId}_04_AXIS") + 1) /2 >= 0.4f;
                        break;
                    case "DPAD_LEFT":
                        value = Input.GetAxisRaw($"P{controllerId}_07_AXIS") <= -0.5f;
                        break;
                    case "DPAD_UP":
                        value = Input.GetAxisRaw($"P{controllerId}_08_AXIS") >= 0.5f;
                        break;
                    case "DPAD_RIGHT":
                        value = Input.GetAxisRaw($"P{controllerId}_07_AXIS") >= 0.5f;
                        break;
                    case "DPAD_DOWN":
                        value = Input.GetAxisRaw($"P{controllerId}_08_AXIS") <= -0.5f;
                        break;
                    default:
                        Debug.LogWarning($"UNKNOWN KEY {button}");
                        break;
                }
            }
            else if (Input.GetJoystickNames()[controllerId] == "Controller (Xbox One For Windows)" || Input.GetJoystickNames()[controllerId] == "Controller (Xbox 360 Wireless Receiver for Windows)")
            {
                switch (button)
                {
                    case "ACTION_LEFT":
                        value = Input.GetButton($"P{controllerId}_02");
                        break;
                    case "ACTION_UP":
                        value = Input.GetButton($"P{controllerId}_03");
                        break;
                    case "ACTION_RIGHT":
                        value = Input.GetButton($"P{controllerId}_01");
                        break;
                    case "ACTION_DOWN":
                        value = Input.GetButton($"P{controllerId}_00");
                        break;
                    case "R_BUTTON":
                        value = Input.GetButton($"P{controllerId}_05");
                        break;
                    case "R_STICK":
                        value = Input.GetButton($"P{controllerId}_09");
                        break;
                    case "R_SELECT":
                        value = Input.GetButton($"P{controllerId}_07");
                        break;
                    case "R_TRIGGER":
                        value = Input.GetAxisRaw($"P{controllerId}_10_AXIS") >= 0.4f;
                        break;
                    case "L_BUTTON":
                        value = Input.GetButton($"P{controllerId}_04");
                        break;
                    case "L_STICK":
                        value = Input.GetButton($"P{controllerId}_08");
                        break;
                    case "L_SELECT":
                        value = Input.GetButton($"P{controllerId}_06");
                        break;
                    case "L_TRIGGER":
                        value = Input.GetAxisRaw($"P{controllerId}_09_AXIS") >= 0.4f;
                        break;
                    case "DPAD_LEFT":
                        value = Input.GetAxisRaw($"P{controllerId}_06_AXIS") <= -0.5f;
                        break;
                    case "DPAD_UP":
                        value = Input.GetAxisRaw($"P{controllerId}_07_AXIS") >= 0.5f;
                        break;
                    case "DPAD_RIGHT":
                        value = Input.GetAxisRaw($"P{controllerId}_06_AXIS") >= 0.5f;
                        break;
                    case "DPAD_DOWN":
                        value = Input.GetAxisRaw($"P{controllerId}_07_AXIS") <= -0.5f;
                        break;
                    default:
                        Debug.LogWarning($"UNKNOWN KEY {button}");
                        break;
                }
            }
            return value;
        }

        public static float GetAxis(this int controllerId, string button)
        {
            float value = 0;

            if (controllerId < 0 || controllerId > 3)
            {
                Debug.LogError($"Controller id {controllerId} is not allowed, min 0 max 3");
                return 0;
            }

            if (Input.GetJoystickNames().Length == 0)
            {
                Debug.LogError("No controllers found");
                return 0;
            }

            if (Input.GetJoystickNames().Length < controllerId + 1)
            {
                return 0;
            }

            if (Input.GetJoystickNames()[controllerId] == "Wireless Controller")
            {
                switch (button)
                {
                    case "L_TRIGGER":
                        value = (Input.GetAxisRaw($"P{controllerId}_04_AXIS") + 1) / 2;
                        break;
                    case "L_HORIZONTAL":
                        value = Input.GetAxisRaw($"P{controllerId}_01_AXIS");
                        break;
                    case "L_VERTICAL":
                        value = Input.GetAxisRaw($"P{controllerId}_02_AXIS");
                        break;
                    case "R_HORIZONTAL":
                        value = Input.GetAxisRaw($"P{controllerId}_03_AXIS");
                        break;
                    case "R_VERTICAL":
                        value = Input.GetAxisRaw($"P{controllerId}_06_AXIS");
                        break;
                    case "R_TRIGGER":
                        value = (Input.GetAxisRaw($"P{controllerId}_05_AXIS") + 1) / 2;
                        break;
                    default:
                        Debug.LogWarning($"UNKNOWN KEY {button}");
                        break;
                }
            }
            else if (Input.GetJoystickNames()[controllerId] == "Controller (Xbox One For Windows)" || Input.GetJoystickNames()[controllerId] == "Controller (Xbox 360 Wireless Receiver for Windows)")
            {
                switch (button)
                {
                    case "L_TRIGGER":
                        value = Input.GetAxisRaw($"P{controllerId}_09_AXIS");
                        break;
                    case "L_HORIZONTAL":
                        value = Input.GetAxisRaw($"P{controllerId}_01_AXIS");
                        break;
                    case "L_VERTICAL":
                        value = Input.GetAxisRaw($"P{controllerId}_02_AXIS");
                        break;
                    case "R_HORIZONTAL":
                        value = Input.GetAxisRaw($"P{controllerId}_04_AXIS");
                        break;
                    case "R_VERTICAL":
                        value = Input.GetAxisRaw($"P{controllerId}_05_AXIS");
                        break;
                    case "R_TRIGGER":
                        value = Input.GetAxisRaw($"P{controllerId}_10_AXIS");
                        break;
                    default:
                        Debug.LogWarning($"UNKNOWN KEY {button}");
                        break;
                }
            }
            return value;
        }
    }
}