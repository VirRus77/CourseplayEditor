using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Core.Tools.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Courseplay.Tests
{
    [TestClass]
    public class CaptureCursorTests
    {
        [TestMethod]
        public void CaptureCursor()
        {
            var cursor = Cursors.Arrow;
            //var names = Enum.GetNames(typeof(Cursors));
            //names.ForEach(cursorName=>SaveCursor(cursorName));
        }

        private void SaveCursor(string cursorName)
        {
            using var cursor = new Cursor(cursorName);

        }
    }
}
