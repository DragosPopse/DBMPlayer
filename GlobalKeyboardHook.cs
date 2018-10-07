using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace DBMPlayer
{
    public class GlobalKeyboardHook 
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        private Dictionary<Keys, Action> _callbacks = new Dictionary<Keys, Action>();
        private Dictionary<string, Keys> _callbackKeys = new Dictionary<string, Keys>();
        private Keys _keyPressed = Keys.None;
        

        public GlobalKeyboardHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }


        ~GlobalKeyboardHook()
        {
            UnhookWindowsHookEx(_hookID);
        }


        public void Update()
        {
            if (_keyPressed != Keys.None && _callbacks.ContainsKey(_keyPressed))
            {
                _callbacks[_keyPressed]();
            }
            _keyPressed = Keys.None;
        }


        public void AddCallback(string id, Keys key, Action action)
        {
            _callbacks.Add(key, action);
            _callbackKeys.Add(id, key);
        }


        public void RemoveCallback(string id)
        {
            _callbacks.Remove(_callbackKeys[id]);
            _callbackKeys.Remove(id);
        }


        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }


        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);


        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                if (GetAsyncKeyState(Keys.ControlKey) != 0)
                {
                    _keyPressed = key;
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// Determines whether a key is up or down at the time the function is called, and whether the 
        /// key was pressed after a previous call to GetAsyncKeyState.
        /// </summary>
        /// <param name="vKey">The virtual-key code</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
    }
}
